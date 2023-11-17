using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

using WebApplication1.Data;
using WebApplication1.Dto;
using WebApplication1.Hub;
using WebApplication1.Models;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using ZXing;
using ZXing.QrCode;
using Microsoft.VisualBasic;
using AutoMapper;
using Microsoft.Extensions.Logging;
using WebApplication1.Helpers;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IHubContext<MessageHub, IMessageHubClient> messageHub;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PatientsController(IHubContext<MessageHub, IMessageHubClient> _messageHub, ApplicationDbContext _context, IMapper _mapper)
        {
            messageHub = _messageHub;
            context = _context;
            mapper = _mapper;

        }



        /*-----------------------------------------   Création d'un nouveau patient   -------------------------------------------*/

        [HttpPost("CreatePatient")]
        public async Task<IActionResult> CreatePatient([FromBody] Patients patient)
        {
            try
            {
               

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

               

                context.patients?.Add(patient);
                await context.SaveChangesAsync();
                return Ok(patient);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /*------------------------------------------- insertion militaire------------------------------------------*/

        [HttpPost("CreatePatientMilitaire")]
        public async Task<IActionResult> CreatePatientMilitaire([FromBody] PatientMilitaireDto patientMilitaireDto)
        {
            try
            {

               

                if (patientMilitaireDto == null)
                {
                    return BadRequest();
                }
                // Validation, if needed
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
               
                // Utilisez AutoMapper pour convertir PatientMilitaireDto en Patients
                var patientMilitaire = mapper.Map<Patients>(patientMilitaireDto);
                patientMilitaire.Ipp = CreateIpp();
                //patientMilitaire.CreatedAt = DateTime.Now.ToString();

                // Ajoutez les détails de patient militaire associés au patient maître
               /* if (patient2.patientMiltaires != null && patient2.patientMiltaires.Any())
                {
                    foreach (var patientMilitaire in patient2.patientMiltaires)
                    {
                        context.patientMiltaires.Add(patientMilitaire);
                    }

                }*/


                // Ajoutez le patient maître à la base de données
                context.patients.Add(patientMilitaire);
                await context.SaveChangesAsync();

                return Ok(patientMilitaire);

            }



            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

           

           
        }

        /*-------------------------------------------- insertion porteur carte des soins ---------------------------------------------*/

        [HttpPost("CreatePatientPorteurCarteSoins")]
        public async Task<IActionResult> CreatePatientPorteurCarteSoins([FromBody] PatientporteurCarteSoinsDto patient)
        {
            try
            {

                if (patient == null)
                {
                    return BadRequest();
                }

                // Validation, if needed
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                // Utilisez AutoMapper pour convertir PatientMilitaireDto en Patients
                var patientporteurCarteSoins = mapper.Map<Patients>(patient);

                patientporteurCarteSoins.Ipp = CreateIpp();
                //patientporteurCarteSoins.CreatedAt = DateTime.Now.ToString();


                // Ajoutez les détails de patient porteur carte des soins associés au patient maître
                /* if (patient.porteurCarteSoins != null && patient.porteurCarteSoins.Any())
                 {
                     foreach (var patientPorteurCarteSoins in patient.porteurCarteSoins)
                     {
                         context.porteurCarteSoins.Add(patientPorteurCarteSoins);
                     }
                 }*/

                // Ajoutez le patient maître à la base de données
                context.patients.Add(patientporteurCarteSoins);
                await context.SaveChangesAsync();

                return Ok(patientporteurCarteSoins);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }






        /*-------------------------------------------- insertion patient cnam ---------------------------------------------*/

        [HttpPost("CreatePatientCnam")]
        public async Task<IActionResult> CreatePatientCnam([FromBody] PatientCnamDto patientCnamDto)
        {
            try
            {

                if (patientCnamDto == null)
                {
                    return BadRequest();
                }

                // Validation, if needed
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                // Utilisez AutoMapper pour convertir PatientMilitaireDto en Patients
                var patientCnam = mapper.Map<Patients>(patientCnamDto);

                patientCnam.Ipp = CreateIpp();
                //patientporteurCarteSoins.CreatedAt = DateTime.Now.ToString();


                // Ajoutez les détails de patient porteur carte des soins associés au patient maître
                /* if (patient.porteurCarteSoins != null && patient.porteurCarteSoins.Any())
                 {
                     foreach (var patientPorteurCarteSoins in patient.porteurCarteSoins)
                     {
                         context.porteurCarteSoins.Add(patientPorteurCarteSoins);
                     }
                 }*/

                // Ajoutez le patient maître à la base de données
                context.patients.Add(patientCnam);
                await context.SaveChangesAsync();

                return Ok(patientCnam);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }







        /*------------------------------------- Générer un Ipp -----------------------------------------------*/

        [HttpPost("CreateIpp")]
        public string CreateIpp()
        {
            bool trouve = true;
            string ipp = string.Empty;
            while (trouve)
            {
                ipp = IppGenerator.GenerateIpp();
                var ch1 = context.patients.FirstOrDefault(x => x.Ipp == ipp);


                if (ch1 == null)
                {

                    trouve = false;

                }

            }
            return ipp; // Retournez l'IPP s'il est unique.
        }





        /*------------------------------------- Générer un indexAssure  -----------------------------------------------*/

        [HttpPost("CreateIndexAssure")]
        public string CreateIndexAssure()
        {
            bool trouve = true;
            string IndexAssure = string.Empty;
            while (trouve)
            {
                IndexAssure = AssureIndexGenerator.GenerateIndex();
                var ch1 = context.patientsBhse.FirstOrDefault(x => x.INDEX_ASSURE.ToString() == IndexAssure);
               


                if (ch1 == null)
                {

                    trouve = false;

                }

            }
            return IndexAssure; // Retournez l'IndexAssure s'il est unique.
        }




        /*---------------------------------- Recherche multicritère du patient  -------------------------------------*/

       [HttpPost("SearchPatients")]
        public async Task<IActionResult> SearchPatients([FromBody] PatientDto patientDto)
        {
            try
            {
                var query = from a in context.patients
                            join b in context.Villes on a.Lieu_Naissance equals b.CODE_VILLE
                            orderby a.CreatedAt descending
                            select new
                            {
                                ipp = a.Ipp,
                                photo= a.Photo != null ? a.Photo : "null",
                                nomFr = a.Nom_Patient_Fr,
                                prenomFr = a.Prenom_Patient_Fr,
                                prenomPereFr = a.Prenom_Pere_Fr,
                                nomAr = a.Nom_Patient_Ar,
                                prenomAr = a.Prenom_Patient_Ar,
                                prenomPereAr = a.Prenom_Pere_Ar,
                                sexe = a.Sexe,
                                dateNaissance = a.Date_Naissance,
                               LibelleVille = b.LIBELLE_VILLE,
                                cin = a.Carte_Identite != null ? a.Carte_Identite : "null",
                                AdresseFr = a.Adresse_Fr,
                                Telephone1 = a.Telephone1,
                                Telephone2 = a.Telephone2 != null ? a.Telephone2 : "null",
                                DateCreation = a.CreatedAt,
                                Situation = a.Situation
                            };

                // les filtres 
                if (!string.IsNullOrEmpty(patientDto.nomFr.ToUpper()))
                {
                    query = query.Where(a => a.nomFr.ToUpper().Contains(patientDto.nomFr.ToUpper()));
                }

                if (!string.IsNullOrEmpty(patientDto.prenomFr.ToUpper()))
                {
                    query = query.Where(a => a.prenomFr.ToUpper().Contains(patientDto.prenomFr.ToUpper()));
                }

                if (!string.IsNullOrEmpty(patientDto.dateNaissance))
                {
                    query = query.Where(a => a.dateNaissance == patientDto.dateNaissance);
                }

                if (!string.IsNullOrEmpty(patientDto.sexe))
                {
                    query = query.Where(a => a.sexe == patientDto.sexe);
                }

                var patients = await query.OrderBy(a => a.ipp).ToListAsync();

                if (patients == null)
                {
                    return BadRequest(new { message = "liste vide" }); // Return a 404 response if no patients match the criteria
                }

                return Ok(patients); // Return the matching patients
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        /*--------------------------------------Détails patient------------------------------------------------------------*/

       [HttpGet("GetPatientById/{patientId}")]
        public async Task<ActionResult<Patients>> GetPatientById(string patientId)
        {
            try
            {
                var patient = await (from a in context.patients
                                     join b in context.Villes on a.Lieu_Naissance equals b.CODE_VILLE

                                     where a.Ipp == patientId
                                     orderby a.Ipp ascending

                                     select new
                                     {

                                         ipp = a.Ipp,
                                         photo= !string.IsNullOrEmpty(a.Photo) ? a.Photo : "Resources\\Images\\inconnu.jpg",
                                         nomFr = a.Nom_Patient_Fr,
                                         prenomFr = a.Prenom_Patient_Fr,
                                         prenomPereFr = a.Prenom_Pere_Fr,
                                         nomAr = a.Nom_Patient_Ar,
                                         prenomAr = a.Prenom_Patient_Ar,
                                         prenomPereAr = a.Prenom_Pere_Ar,
                                         sexe = a.Sexe,
                                         dateNaissance = a.Date_Naissance,
                                         //Lieu_Naissance=a.Lieu_Naissance.ToString(),
                                         LibelleVille = b.LIBELLE_VILLE,
                                         cin = a.Carte_Identite != null ? a.Carte_Identite : "null",
                                         AdresseFr = a.Adresse_Fr,
                                         Telephone1 = a.Telephone1,
                                         Telephone2 = a.Telephone2 != null ? a.Telephone2 : "null",
                                         DateCreation = a.CreatedAt,
                                         Situation = a.Situation





                                     }).FirstOrDefaultAsync();

                if (patient == null)
                {
                    return NotFound(); // Return a 404 response if patient is not found
                }

                return Ok(patient); // Return the patient if found
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }




        /*--------------------------------------Détails patient militaire------------------------------------------------------------*/

        [HttpGet("GetHistoPatientMilitaire/{ipp}")]
        public async Task<ActionResult> GetHistoPatientMilitaire(string ipp)
        {
            try
            {
                var patients = await (from a in context.patientMiltaires 
                                      join b in context.Categories on a.CodeCategorie equals b.CODE_CATEGORIE
                                      join c in context.Scategories on a.CodeSCategorie equals c.CODE_SCATEGORIE
                                      join d in context.Grades on a.CodeGrade equals d.CODE_GRADE
                                      where a.PatientsIpp==ipp
                                      



                                      select new
                                      {

                                          ipp = a.PatientsIpp,
                                          identifiant = a.Identifiant,
                                          categorie = b.LIBELLE_CATEGORIE,
                                          scategorie = c.LIBELLE_SCATEGORIE,
                                          grade=d.LIBELLE_GRADE,
                                          dateCreation=a.CreatedAt

                                      }).ToListAsync();

            

                if (patients == null)
                {
                    return Ok(""); // Return a 404 response if patient is not found
                }

                return Ok(patients); // Return the patient if found
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /*--------------------------------------Détails patient Porteur cate des soins------------------------------------------------------------*/

        [HttpGet("GetHistoPorteurCarteSoins/{ipp}")]
        public async Task<ActionResult> GetHistoPorteurCarteSoins(string ipp)
        {
            try
            {
                var patients = await (from a in context.porteurCarteSoins
                                      join b in context.Categories on a.CodeCategorie equals b.CODE_CATEGORIE
                                      join c in context.Scategories on a.CodeSCategorie equals c.CODE_SCATEGORIE
                                     
                                      where a.PatientsIpp == ipp




                                      select new
                                      {

                                          ipp = a.PatientsIpp,
                                          identifiant = a.Identifiant,
                                          categorie = b.LIBELLE_CATEGORIE,
                                          scategorie = c.LIBELLE_SCATEGORIE,
                                          numCarteSoin=a.NumCarteSoin,
                                          dateValidite=a.DateValidite,
                                         
                                          dateCreation = a.CreatedAt

                                      }).ToListAsync();



                if (patients == null)
                {
                    return Ok(""); // Return a 404 response if patient is not found
                }

                return Ok(patients); // Return the patient if found
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }







    }



}










 















    




