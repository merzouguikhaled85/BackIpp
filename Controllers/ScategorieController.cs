using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using WebApplication1.Data;
using WebApplication1.Dto;
using WebApplication1.Hub;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScategorieController : ControllerBase
    {

        private IHubContext<MessageHub, IMessageHubClient> messageHub;
        private readonly ApplicationDbContext context;


        public ScategorieController(IHubContext<MessageHub, IMessageHubClient> _messageHub, ApplicationDbContext _context)
        {
            messageHub = _messageHub;
            context = _context;
        }



        /*------------------------------ afficher  la liste des scatégories-------------------------------------------------*/

        [HttpGet]
        [Route("listCategories")]
        public async Task<ActionResult<Categorie>> GetallCategories()
        {
            try
            {

                var Categories = await context.Categories.OrderBy(x => x.LIBELLE_CATEGORIE).ToListAsync();


                if (Categories.Count == 0)
                {
                    return BadRequest(new { message = "liste vide" });

                }


                return Ok(Categories);




            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }






        /*------------------------------ afficher  la liste des scatégories-------------------------------------------------*/

       [HttpGet]
        [Route("listScategories/{id}")]
        public async Task<ActionResult<Scategorie>> Getall(int id)
        {
            try
            {

                // var scategories = await context.Scategories.OrderBy(x => x.LIBELLE_SCATEGORIE).ToListAsync();

                var scategories = await (from a in context.Scategories
                                         join b in context.Categories on a.CODE_CATEGORIE equals b.CODE_CATEGORIE
                                         where a.CODE_CATEGORIE == id
                                         orderby b.LIBELLE_CATEGORIE ascending
                                         select new
                                         {
                                             CodeScategorie = a.CODE_SCATEGORIE,
                                             LibScategorie = a.LIBELLE_SCATEGORIE,
                                             CodeCategorie = a.CODE_CATEGORIE,
                                             LibCategorie = b.LIBELLE_CATEGORIE

                                         }).ToListAsync();
                if (scategories.Count == 0)
                {
                    return BadRequest(new { message = "liste vide" });

                }


                return Ok(scategories);




            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        /*------------------------------ afficher  la liste des Grades-------------------------------------------------*/

        [HttpGet]
        [Route("listgrades")]
        public async Task<ActionResult<Grade>> GetallGrades()
        {
            try
            {

                // var scategories = await context.Scategories.OrderBy(x => x.LIBELLE_SCATEGORIE).ToListAsync();

                var Grades = await context.Grades.OrderBy(x => x.CODE_GRADE).ToListAsync();
                if (Grades.Count == 0)
                {
                    return BadRequest(new { message = "liste vide" });

                }


                return Ok(Grades);




            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }




        /*------------------------------ afficher  la liste des villes-------------------------------------------------*/

        [HttpGet]
        [Route("listVilles")]
        public async Task<ActionResult<Ville>> GetallVilles()
        {
            try
            {

                // var scategories = await context.Scategories.OrderBy(x => x.LIBELLE_SCATEGORIE).ToListAsync();

                var Villes = await context.Villes.OrderBy(x => x.CODE_VILLE).ToListAsync();
                if (Villes.Count == 0)
                {
                    return BadRequest(new { message = "liste vide" });

                }


                return Ok(Villes);




            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        






    }
}
