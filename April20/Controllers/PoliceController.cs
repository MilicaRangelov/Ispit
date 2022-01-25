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
    public class PoliceController : ControllerBase
    {

        private VideotekaContext Context { get; set; }
        public PoliceController(VideotekaContext context)
        {
            Context = context;
        }

        [Route("PrikazPolica")]
        [HttpGet]

        public async Task<ActionResult> PrikazPolica(){

            return Ok(await Context.Police.Select(p => new {
                ID = p.ID,
                Naslov = p.Vrsta,
                Velicina = p.Velicina
            }).ToListAsync());

        }


       [Route("PrikaziPoliceVideoteke/{idKluba}")]
       [HttpGet]

       public async Task<ActionResult> PrikaziPoliceVideoteke(int idKluba){

           if(idKluba <= 0){

               return BadRequest("Pogresan id video kluba");
           }

           try{

               return Ok(
                   await Context.DVDS.Where(p=>p.Klubovi.ID == idKluba)
                   .Select(p => new{
                       ID =p.ID,
                       Naziv = p.Police.Vrsta,
                       Velicina = p.Police.Velicina
                   })
                   .ToListAsync()
        
               );

           } 
           catch(Exception ex){

               return BadRequest(ex.Message);

           }


       }

       [Route("PrikazibrojDvd/{idPolice}/{idKluba}")]
       [HttpGet]

        public async Task<ActionResult> PrikazibrojDvd(int idPolice, int idKluba){

            if(idPolice < 0 || idKluba < 0)
                return BadRequest("Pogresan id");

            try{

                var police = await Context.Police.Where(p => p.ID == idPolice).FirstOrDefaultAsync();
                var klub =  await Context.VideoKlubovi.Where(p=>p.ID == idKluba).FirstOrDefaultAsync();

                if(police != null && klub != null){
                    
                  return Ok(await Context.DVDS.Where(q=> q.Police.ID == police.ID && q.Klubovi.ID == klub.ID)
                  .Select( p=> new{
                      Zauzeto = p.Broj,
                      Polica = p.Police.Vrsta,
                      Velicina = p.Police.Velicina,
                      Klub = p.Klubovi.Naziv

                  }).FirstOrDefaultAsync());


                }
                else{
                    return BadRequest("Pogresni podaci");
                }

            }
            catch(Exception ex){

                return BadRequest(ex.Message);
            }
        }



       [Route("DodajVrstuPolice/{vrsta}/{broj}")]
       [HttpPost]

       public async Task<ActionResult> DodajVrstuPolice(string vrsta, int broj){

           if(string.IsNullOrEmpty(vrsta))
                return BadRequest("Unesite vrstu police");
            if(broj <= 0)
                return BadRequest("Veca velicina police je potrebna");

            try{

                var police = await Context.Police.Where(p=>p.Vrsta == vrsta && p.Velicina == broj).FirstOrDefaultAsync();

                    if(police == null){
                    Police p = new Police();
                    p.Vrsta = vrsta;
                    p.Velicina = broj;
                    
                    Context.Police.Add(p);
                    await Context.SaveChangesAsync();

                    return Ok("Uspesno dodata polica");
                }
                else{
                    return BadRequest("Vec postoji polica");
                }

            }
            catch(Exception  ex){

                return BadRequest(ex.Message);
            }    


       }


       [Route("DodajPoliceKluba/{idKluba}/{idPolice}")]
       [HttpPost]

       public async Task<ActionResult> DodajPoliceKluba(int idKluba, int idPolice){
            if(idPolice <= 0 || idKluba<=0)
                return BadRequest("Pogresan broj");

            try{

                var police = await Context.Police.Where(p=> p.ID == idPolice).FirstOrDefaultAsync();
                var klub = await Context.VideoKlubovi.Where(p=>p.ID == idKluba).FirstOrDefaultAsync();

                if(police != null && klub != null){

                    DVD d = new DVD();
                    d.Broj = 0;
                    d.Police = police;
                    d.Klubovi = klub;

                    Context.DVDS.Add(d);
                    await Context.SaveChangesAsync();

                    return Ok("Uspesno dodato");
                }
                else{
                    return BadRequest("Pogresne vrednosti");
                }

            }   
            catch(Exception ex){
                return BadRequest(ex.Message);
            } 


       }

       [Route("IzmeniDvdNaPolicu/{idKluba}/{idPolice}/{broj}")]
       [HttpPut]

       public async Task<ActionResult> IzmeniDvdNaPolicu(int idKluba,int idPolice, int broj){

           if(idPolice <= 0 || broj <0 || idKluba<=0)
                return BadRequest("Pogresan broj");

            try{

                VideoKlub vk = await Context.VideoKlubovi.Where(p =>p.ID == idKluba).FirstOrDefaultAsync();
                Police polica = await Context.Police.Where(p=>p.ID == idPolice).FirstOrDefaultAsync();

                if(vk != null && polica != null){
                    DVD pk = await Context.DVDS.Where(p => p.Klubovi.ID == idKluba && p.Police.ID == idPolice).FirstOrDefaultAsync();
                    if(pk.Broj + broj > polica.Velicina){
                        return BadRequest("Nema dovoljno mesta");   
                    }
                        pk.Broj += broj;
                        await Context.SaveChangesAsync();
                }

                return Ok("Uspesno dodati diskovi police");

            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
       } 

        [Route("ObrisiPoilcu/{id}")]
        [HttpDelete]

        public async Task<ActionResult> ObrisiPolicu(int id){

            if(id < 0){
                return BadRequest("Pogresan id police");
            }

            try{

                var police = await Context.Police.Where(p=> p.ID == id).FirstOrDefaultAsync();
                if(police != null){

                    Context.Police.Remove(police);
                    await Context.SaveChangesAsync();
                    return Ok("Usoesno obrisana polica");
                }
                else{
                    return BadRequest("Ne postoji polica sa zadatim id-jem");
                }


            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

    }
       
}
