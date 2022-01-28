using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace OktobarII21.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KorpaController : ControllerBase
    {

        public ProdavnicaContext Context { get; set; }


        public KorpaController(ProdavnicaContext Context)
        {
            this.Context  = Context;
        }


        [Route("PrikaziKorpu/{idKorpe}")]
        [HttpGet]

        public async Task<ActionResult> PrikaziKorpu(int idKorpe){

            if(idKorpe <= 0){
                return BadRequest("Pogresan id korpe");
            }

            try{

                return Ok(await Context.Spoj.Where(p=>p.Korpa.ID == idKorpe)
                .Select(p=> new{

                    ID = p.ID,
                    Kolicina = p.Kolicina,
                    Proizvod = p.Proizvod.Naziv,

                }).ToListAsync());

            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [Route("DodajProizvodUKorpu/{kolicina}/{idProizoda}/{idKorpe}")]
        [HttpPost]

        public async Task<ActionResult> DodajProizvodUKorpu(int kolicina, int idProizvoda, int idKorpe){

            if(kolicina < 0){
                return BadRequest("Uneli ste pogresnu kolicinu");
            }

            if(idProizvoda <= 0 || idKorpe <= 0){
                return BadRequest("Pogresan id");
            }

            try{
                var proizvod = await Context.Proizvod.Where(p=> p.ID == idProizvoda).FirstOrDefaultAsync();
                var korpa = await Context.Korpa.Where(p=>p.ID == idKorpe).FirstOrDefaultAsync();
                

                if(proizvod != null && korpa !=null && proizvod.Kolicina >= kolicina){
                   
                   var spoj = await Context.Spoj.Where(p=>p.Proizvod == proizvod && p.Korpa == korpa).FirstOrDefaultAsync();
                   if(spoj != null){
                       spoj.Kolicina += kolicina;

                       Context.Spoj.Update(spoj);
                   }
                   else{

                       Spoj s = new Spoj();
                       s.Kolicina = kolicina;
                       s.Proizvod = proizvod;
                       s.Korpa = korpa;

                       Context.Spoj.Add(s);
                   }

                   await Context.SaveChangesAsync();

                   return Ok("Uspesno dodat proizvod u korpu");

                }
                else{
                    return BadRequest("NIje bilo moguce dodati proizvod u korpu");
                }
            }
            catch(Exception ex){

                return BadRequest(ex.Message);
            }


        }

        [Route("DodajKorpu")]
        [HttpPost]

        public async Task<ActionResult> DodajKorpu(){

            Korpa k = new Korpa();
            k.Ukupno = 2000;
            Context.Korpa.Add(k);
            await Context.SaveChangesAsync();

            return Ok("Uspesno dodata korpa");
        }

      
    }
}
