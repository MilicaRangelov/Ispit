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
    public class SmestajniObjekatController : ControllerBase
    {
       
       public AplikacijaContext Context { get; set; }

        public SmestajniObjekatController(AplikacijaContext cont)
        {
           Context = cont;
        }

        [Route("PrikaziObjekteMesta/{idMesta}")]
        [HttpGet]

        public async Task<ActionResult> PrikaziObjekteMesta(int idMesta){

            if(idMesta <= 0){
                return BadRequest("Pogresan id");
            }

            try{

                    return Ok(await Context.SmestajniObjekat.Where(p=>p.Mesto.ID == idMesta).Select(p=> new{
                                Id = p.ID,
                                Naziv = p.Naziv
                            }).ToListAsync());
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
      
        }

        [Route("DodajObjekatMesta/{naziv}/{idMesta}")]
        [HttpPost]

        public async Task<ActionResult> DodajObjekatMesta(string naziv,int idMesta){

            if(string.IsNullOrEmpty(naziv) || naziv.Length > 20)
                return BadRequest("Pogresan naziv");

            if(idMesta <=0){
                return BadRequest("Pogresan id");
            }    

            try{
                var mesto = await Context.Mesto.Where(p=>p.ID == idMesta).FirstOrDefaultAsync();
                if(mesto != null){
                    SmestajniObjekat m = new SmestajniObjekat();
                    m.Naziv = naziv;
                    m.Mesto = mesto;
                    Context.SmestajniObjekat.Add(m);
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

    }
}
