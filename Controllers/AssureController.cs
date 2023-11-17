using AutoMapper;
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
    public class AssureController : ControllerBase
    {
        private readonly IHubContext<MessageHub, IMessageHubClient> messageHub;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public AssureController(IHubContext<MessageHub, IMessageHubClient> _messageHub, ApplicationDbContext _context, IMapper _mapper)
        {
            messageHub = _messageHub;
            context = _context;
            mapper = _mapper;

        }


        /*--------------------------------------Détails assure by matricule------------------------------------------------------------*/

        [HttpGet("GetAssureById/{matricule}")]
        public async Task<ActionResult<Assure>> GetAssureById(string matricule)
        {
            try
            {
                var assure = await (from a in context.Assures
                                     

                                     where a.MATRICULE_ASSURE == matricule
                                    

                                     select new
                                     {

                                         matricule = a.MATRICULE_ASSURE,
                                         nom_prenom_assure=a.NOM_PRENOM_ASSURE,
                                         date_validite=a.DATE_VALIDITE.ToShortDateString(),
                                         code_scategorie=a.CODE_SCATEGORIE
                                         

                                     }).FirstOrDefaultAsync();

                if (assure == null)
                {
                    return NotFound(new { Message = "vérifier le numéro de carnet" }); // Return a 404 response if patient is not found
                }

                return Ok(assure); // Return the patient if found
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
