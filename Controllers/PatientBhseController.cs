using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Immutable;
using System.Globalization;
using WebApplication1.Data;
using WebApplication1.Dto;
using WebApplication1.Hub;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientBhseController : ControllerBase
    {
        private readonly IHubContext<MessageHub, IMessageHubClient> messageHub;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PatientBhseController(IHubContext<MessageHub, IMessageHubClient> _messageHub, ApplicationDbContext _context, IMapper _mapper)
        {
            messageHub = _messageHub;
            context = _context;
            mapper = _mapper;

        }

        /*------------------------------------------- insertion militaire dans Bhse------------------------------------------*/

        [HttpPost("CreatePatientBhseMilitaire")]
        public async Task<IActionResult> CreatePatientBhseMilitaire([FromBody] PatientMilitaireBhseDto patientMilitaireBhseDto)
        {
            try
            {
                if (patientMilitaireBhseDto == null)
                {
                    return BadRequest();
                }
                // Validation, if needed
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                // Utilisez AutoMapper pour convertir PatientMilitaireDto en PatientBhse
               
                var patientMilitaireBhse = mapper.Map<PatientBhse>(patientMilitaireBhseDto);

                // Ajoutez le patient  à la base de données Bhse
                context.patientsBhse.Add(patientMilitaireBhse);
                await context.SaveChangesAsync();

                return Ok(patientMilitaireBhse);


            }


            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }




        }



        /*--------------------------------------Détails patient by identifiant bhse------------------------------------------------------------*/

        [HttpGet("GetPatientMilitaireById/{identifiant}")]
        public async Task<ActionResult<PatientMilitaireBhseDto>> GetPatientMilitaireById(string identifiant)
        {
            try
            {
                var patient = await (from a in context.patientMiltaires
                                     join b in context.patients on a.PatientsIpp equals b.Ipp

                                     where a.Identifiant == identifiant


                                     select new PatientMilitaireBhseDto
                                     {

                                         IDENTIFIANT = a.Identifiant,
                                         NOM = b.Nom_Patient_Fr,
                                         PRENOM = b.Prenom_Patient_Fr,
                                         SEXE = b.Sexe,
                                         MATRICULE_CORPS = "",
                                         MATRICULE_REC = a.MatriculeRec,
                                         ANNEE_REC = a.AnneeRec != null ? a.AnneeRec : "null",
                                         CARTE_IDENTITE = b.Carte_Identite != null ? b.Carte_Identite : "null",
                                         DATE_CIN = b.Date_Cin != null ? b.Date_Cin : "null",
                                         ADRESSE = b.Adresse_Fr,
                                         TELEPHONE = b.Telephone1,
                                         ARMEE = a.Armee,
                                         CODE_GRADE = a.CodeGrade,
                                         CODE_CORPS = a.CodeCorps,
                                         CODE_CATEGORIE = a.CodeCategorie,
                                         CODE_SCATEGORIE = a.CodeSCategorie,
                                         IPP = a.PatientsIpp!,
                                         DATE_NAISSANCE = Convert.ToDateTime(b.Date_Naissance)
                                        




                                     }).FirstOrDefaultAsync();

                if (patient == null)
                {
                    return NotFound(); // Return a 404 response if patient is not found
                }

              

              await CreatePatientBhseMilitaire(patient);
                return Ok(patient); // Return the patient if found


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }


}
