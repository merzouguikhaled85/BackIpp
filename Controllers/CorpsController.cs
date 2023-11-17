using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Hub;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorpsController : ControllerBase
    {
        private IHubContext<MessageHub, IMessageHubClient> messageHub;
        private readonly ApplicationDbContext context;

        public CorpsController(IHubContext<MessageHub, IMessageHubClient> _messageHub, ApplicationDbContext _context)
        {
            messageHub = _messageHub;
            context = _context;

        }


        /*--------------------------------------------Liste des corps --------------------------------------------*/
      [HttpGet]
        [Route("listCorps")]
        public async Task<IActionResult> Getall()

        {
            try
            {
                var corps = await (from a in context.Corps



                                   orderby a.LIBELLE_CORP ascending

                                   select new
                                   {

                                       Code_corps = a.CODE_CORPS,
                                       Lib_Corps = a.LIBELLE_CORP,

                                       Abrege_Corps = a.ABREGE_CORP != null ? a.ABREGE_CORP : "null",

                                       Armee = a.ARMEE != null ? a.ARMEE : "null",






                                   }).ToListAsync();

                if (corps == null)
                {
                    return NotFound(); 
                }

                return Ok(corps); // Return the patient if found
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }




    }
}
