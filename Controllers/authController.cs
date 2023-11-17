using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Data;
using WebApplication1.Dto;
using WebApplication1.Helpers;
using WebApplication1.Hub;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class authController : ControllerBase
    {
        public static readonly List<string> ConnectedUsers = new List<string>();
        private readonly IHubContext<MessageHub, IMessageHubClient> messageHub;
        private readonly ApplicationDbContext context;
       


        public authController(IHubContext<MessageHub, IMessageHubClient> _messageHub, ApplicationDbContext _context)
        {
            messageHub = _messageHub;
            context = _context;
            

        }

        /*---------------------------------- Connecter à l'application ---------------------------------------*/

        [HttpPost("Connect")]
        public async Task<IActionResult> Connect([FromBody] UserVm user)
        {
            try
            {
                if (user != null)
                {
                    // return Ok(await _context.Users!.Where(a => a.Password == MD5Hash(user.Password!)).FirstOrDefaultAsync());
                    var res = await context.users!.Where(a => a.Login == user.Login!).FirstOrDefaultAsync();
                    if (res != null)
                    {
                        bool passwordMatches = PasswordHasher.VerifyPassword(user.Password!, res.Password!);
                        if (!passwordMatches)
                       
                        {
                            // return StatusCode(201);
                            return NotFound(new { Message = "le mot de passe est incorrect" });

                        }

                        if (res.Situation != "active")
                        {

                            return BadRequest(new { Message = "le compte est  verouillé,Contacter l'administrateur" });


                        }
                        //return Ok(res.Prenom!.ToString()+" "+res.Nom!.ToString());
                        //return Ok(res);


                        /**/
                        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("s59JXsM7wNh6rVGF"));
                        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                        var claims = new List<Claim>
                          {
                             new Claim(ClaimTypes.NameIdentifier, res.Id.ToString()),
                             new Claim(ClaimTypes.Name, res.Nom+' '+res.Prenom),
                             new Claim(ClaimTypes.Role, res.Role!),
                             new Claim(ClaimTypes.UserData, res.Situation)

                          };
                        var tokeOptions = new JwtSecurityToken(
                            issuer: "https://localhost:7058",
                            audience: "https://localhost:7058",
                            claims: claims,

                            expires: DateTime.Now.AddMinutes(120),
                            signingCredentials: signinCredentials
                        );

                       
                        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                        
                        return Ok(new AuthenticatedResponse { Token = tokenString });


                    }


                }

                return BadRequest(new { Message = "Le nom d'utilisateur ou le mot de passe est incorrect" });


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


       

    }
}
