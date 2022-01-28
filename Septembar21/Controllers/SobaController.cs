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
    public class SobaController : ControllerBase
    {
       
       public AplikacijaContext Context { get; set; }

        public SobaController(AplikacijaContext cont)
        {
           Context = cont;
        }

        [Route("PrikaziSobeSmestaja/{idSmestaja}")]
        [HttpGet]

        public async Task<ActionResult> PrikaziSobeSmestaja(int idSmestaja){

            if(idSmestaja <= 0){
                return BadRequest("Pogresan id");
            }

            try{

                    return Ok(await Context.Soba.Where(p=>p.SmestajniObjekat.ID == idSmestaja).Select(p=> new{
                                Id = p.ID,
                                BrojSobe = p.BrSobe
                            }).ToListAsync());
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
      
        }

        [Route("DodajSobuSmestaja/{brSobe}/{idSmestaja}")]
        [HttpPost]

        public async Task<ActionResult> DodajMestoUDrzavi(int brSobe,int idSmestaja){

           if(brSobe < 0)
                return BadRequest("Br sobe manji od 0");

            if(idSmestaja <=0){
                return BadRequest("Pogresan id");
            }    

            try{
                var smestaj = await Context.SmestajniObjekat.Where(p=>p.ID == idSmestaja).FirstOrDefaultAsync();
                if(smestaj != null){
                    Soba m = new Soba();
                    m.BrSobe = brSobe;
                    m.SmestajniObjekat = smestaj;
                    Context.Soba.Add(m);
                    await Context.SaveChangesAsync();
                    return Ok("Uspesno dodato mesto");
                }
                else{
                    return BadRequest("Ne moze se dodati mesto");
                }
            }   
            catch(Exception ex){
                return BadRequest(ex.Message);
            } 
        }


        [Route("DodajRezervacijeZaSobe/{idSobe}/{idRezervacije}/{zauzeto}")]
        [HttpPost]

        public async Task<ActionResult> DodajRezervacijeZaSobe(int idSobe, int idRezervacije, int zauzeto){

            if(idSobe <= 0 || idRezervacije <= 0 ){
                return BadRequest("Pogresan id");
            }

            try{

                var soba = await Context.Soba.Where(p=>p.ID == idSobe).FirstOrDefaultAsync();
                var rezervacija = await Context.Rezervacija.Where(p=> p.ID == idRezervacije).FirstOrDefaultAsync();

                if(soba != null && rezervacija != null){

                    Spoj s = new Spoj();
                    s.Soba = soba;
                    s.Rezervacija = rezervacija;
                    if(zauzeto == 0)
                        s.Zauzeto = true;
                    else    
                        s.Zauzeto = false;

                    Context.RezervacijaSoba.Add(s);
                    await Context.SaveChangesAsync();

                    return Ok("Uspesno");    

                }
                else{
                    return BadRequest("Greska");
                }

            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [Route("PrikaziDostupneRezervacije/{datumOd}/{datumDo}/{kapacitet}/{idSmestaja}")]
        [HttpGet]

        public async Task<ActionResult> PrikaziDostupneRezervacije(string datumOd, string datumDo, int kapacitet, int idSmestaja){

            try{
                DateTime dod = DateTime.Parse(datumOd);
                DateTime ddo = DateTime.Parse(datumDo);

                return Ok(await Context.RezervacijaSoba.Where(p => p.Rezervacija.DatumOd.CompareTo(dod) >= 0 && p.Rezervacija.DatumDo.CompareTo(ddo) <= 0 &&p.Rezervacija.Kapacitet == kapacitet && p.Soba.SmestajniObjekat.ID == idSmestaja)
                    .Select(p=> new{

                        Id = p.ID,
                        BrSobe = p.Soba.BrSobe,
                        DatumOdd = p.Rezervacija.DatumOd.ToShortDateString(),
                        DatumDoo = p.Rezervacija.DatumDo.ToShortDateString(),
                        Kapacitet = p .Rezervacija.Kapacitet,
                        Zauzeto = (p.Zauzeto)? "zauzeto" : "slobodno"

                    }).ToListAsync());

            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
    }
}
