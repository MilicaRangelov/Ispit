using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;

namespace Septembar21.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RezervacijaController : ControllerBase
    {
       
       public AplikacijaContext Context { get; set; }

        public RezervacijaController(AplikacijaContext cont)
        {
           Context = cont;
        }

        [Route("PrikaziRezervacije")]
        [HttpGet]

        public async Task<ActionResult> PrikaziRezervacije(){

            try{

                    return Ok(await Context.Rezervacija.Select(p=> new{
                                Id = p.ID,
                                DatumOd = p.DatumOd.ToShortDateString(),
                                DatumDo = p.DatumDo.ToShortDateString(),
                                Kapacitet = p.Kapacitet
                            }).ToListAsync());
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
      
        }

        [Route("DodajRezervacije/{datumOd}/{datumDo}/{kapacitet}")]
        [HttpPost]

        public async Task<ActionResult> DodajMestoUDrzavi(DateTime datumOd, DateTime datumDo, int kapacitet){

        
            try{
                
                Rezervacija r = new Rezervacija();
                r.DatumOd = datumOd;
                r.DatumDo = datumDo;
                r.Kapacitet = kapacitet;
                Context.Rezervacija.Add(r);
                await Context.SaveChangesAsync();

                return Ok("Uspesna rezervacija");
                
             
            }   
            catch(Exception ex){
                return BadRequest(ex.Message);
            } 
        }

    }
}
