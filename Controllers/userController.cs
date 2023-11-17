using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using WebApplication1.Data;
using WebApplication1.Dto;
using WebApplication1.Helpers;
using WebApplication1.Hub;
using Microsoft.AspNetCore.Http;

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        public static readonly List<string> ConnectedUsers = new List<string>();
        private IHubContext<MessageHub, IMessageHubClient> messageHub;
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor httpContextAccessor;
        public userController(IHubContext<MessageHub, IMessageHubClient> _messageHub, ApplicationDbContext _context, IHttpContextAccessor _httpContextAccessor)
        {
            messageHub = _messageHub;
            context = _context;
            httpContextAccessor = _httpContextAccessor;
        }





        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            try
            {
                if (user is null)
                {
                    return BadRequest("user object is null");
                }

                if (!ModelState.IsValid)
                {
                    // return BadRequest("Invalid model object");

                    // Récupérer les erreurs de validation du modèle
                    var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                    return BadRequest(new { Errors = errors });
                }

              
                bool loginExists = CheckExistLogin(user.Login!);

                if (loginExists)
                {
                    return BadRequest( new {message= "A user with the same login already exists." } );
                }

                user.Password = PasswordHasher.HashPassword(user.Password);

                context.users?.Add(user);


                await context.SaveChangesAsync();
             

                return Ok(new userDto
                {
                    Nom = user.Nom,
                    Prenom = user.Prenom,
                    Login = user.Login,
                    Role = user.Role,
                    Situation = user.Situation
                });
                var users=await context.users.ToListAsync();
               await messageHub.Clients.All?.SendListUsers(users);

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut("UpdateUser/{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] User updatedUser)
        {
            try
            {
                if (updatedUser == null)
                {
                    return BadRequest("Updated user object is null");
                }

                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
                    return BadRequest(new { message = errors });
                }

                var existingUser = await context.users.FindAsync(userId);

                if (existingUser == null)
                {
                    return NotFound("User not found");
                }

                // Check if the login is being changed and if it already exists
                if (existingUser.Login != updatedUser.Login && CheckExistLogin(updatedUser.Login!))
                {
                    return BadRequest(new { message = "A user with the same login already exists." });
                }

                if(updatedUser.Role is null)
                {
                    return BadRequest(new { message = "choisir un role parmi la liste" });
                }
               

                // Update user properties
               
                
                existingUser.Nom = updatedUser.Nom;
                existingUser.Prenom = updatedUser.Prenom;
                existingUser.Login = updatedUser.Login;
                existingUser.Password = PasswordHasher.HashPassword(updatedUser.Password);
                existingUser.Role = updatedUser.Role;
                existingUser.Situation = updatedUser.Situation;

                // Save changes to the database
                await context.SaveChangesAsync();

                var users = await context.users.ToListAsync();
                await messageHub.Clients.All!.SendListUsers(users);

                return Ok(new userDto
                {
                    Id = userId,
                    Nom = existingUser.Nom,
                    Prenom = existingUser.Prenom,
                    Login = existingUser.Login,
                    Role = existingUser.Role,
                    Situation = existingUser.Situation
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //[Authorize(Roles = "admin,user")]
        [HttpPost]
        [Route("listUsers")]
        public async Task<IActionResult> Getall()
           
        {
            try
            {
                List<User> users = await context.users!.ToListAsync();
                if (users != null)
                {
                    //messageHub.Clients.All.SendOffersToUser(users);
                    string date = DateTime.Now.ToString();
                    // messageHub.Clients.All.SendData("Dernière résultat du " + date);
                    return Ok(users);
                    //messageHub.Clients.All.SendListUsers(users);
                }

                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("GetUserById/{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            try
            {
                var user = await context.users.FindAsync(userId);

                if (user == null)
                {
                    return NotFound("User not found");
                }

                var userDto = new userDto
                {
                    Id=userId,
                    Nom = user.Nom,
                    Prenom = user.Prenom,
                    Login = user.Login,
                    Role = user.Role, // Update this with the actual role value
                    Password=user.Password,
                    Situation = user.Situation
                };

                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Authorize(Roles = "admin,user")]
        [HttpPost]
        [Route("listUsersToTaches")]
        public async Task<ActionResult<userDto>> listUsersToTaches()

        {
            try
            {
                List<User> users = await context.users!.ToListAsync();
                if (users != null)
                {
                   

                    List<userDto> usersDto = users.Select(user => new userDto
                    {
                        Id=user.Id,
                        Nom = user.Nom,
                        Prenom = user.Prenom,
                        Login = user.Login,
                        Role = user.Role,
                        Situation = user.Situation
                    }).ToList();

                   messageHub.Clients.All?.SendListUsers(users);
                    return Ok(usersDto);
                 ;

                }

                return BadRequest();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /*---------------------------------- Vérifier l'existence du login  ---------------------------------------*/
        private bool CheckExistLogin(string login)
        {

            var existingUser = context.users.FirstOrDefault(u => u.Login == login);

            if (existingUser != null) return true;
            else return false;
        }


        [HttpGet("LogUserLoginInfo")]
        public IActionResult LogUserLoginInfo()
        {
            var hostname = Environment.MachineName;
            string ipAddress = httpContextAccessor.HttpContext!.Connection.RemoteIpAddress!.ToString();
            var loginTime = DateTime.Now;
            var user = Environment.UserName;
            var connectionId = httpContextAccessor.HttpContext.Connection.Id;

            ConnectedUsers.Add(connectionId);

            var userLoginInfo = new
            {
                Hostname = hostname,
                IPAddress = ipAddress,
                LoginTime = loginTime,
                User=user,
                ConnectionId= connectionId
            };

            return Ok(userLoginInfo);
        }









    }
}
