using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace April20.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideoKlubController : ControllerBase
    {

        private VideotekaContext Context { get; set; }
        public VideoKlubController(VideotekaContext context)
        {
            Context = context;
        }

        [Route("VideotekePrikaz")]
        [HttpGet]
        public async Task<ActionResult> VideotekePrikaz(){

            return Ok(await Context.VideoKlubovi.Select(p=> new{
                Id = p.ID,
                Naziv = p.Naziv
            }).ToListAsync());

        }

        [Route("PrikaziKlub/{id}")]
        [HttpGet]
        public async Task<ActionResult> PrikaziKlub(int id){

            if(id <= 0){
                return BadRequest("Id kluba mora biti veci od nule");
            }

            try{

                return Ok(await Context.VideoKlubovi.Where(p=> p .ID == id)
                .Select(p => new {
                    Id = p.ID,
                    Naziv = p.Naziv
                })
                .FirstOrDefaultAsync());

            }
            catch(Exception ex){

                return BadRequest(ex.Message);
            }
        }

        [Route("DodajVideoKlub/{naziv}")]
        [HttpPost]

        public async Task<ActionResult> DodajVideoKlub(string naziv){

            if(string.IsNullOrEmpty(naziv))
                return BadRequest("Morate uneti naziv video kluba");

            try{

                VideoKlub klub = new VideoKlub();
                klub.Naziv = naziv;
                Context.VideoKlubovi.Add(klub);
                await Context.SaveChangesAsync();

                return Ok("Uspesno dodat video klub");

            }
            catch(Exception ex){

                return BadRequest(ex.Message);
            }    

        }



  
    }
}
