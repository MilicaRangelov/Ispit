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
    public class MestoController : ControllerBase
    {
       
       public AplikacijaContext Context { get; set; }

        public MestoController(AplikacijaContext cont)
        {
           Context = cont;
        }

        [Route("PrikaziMestaDrzave/{idDrzave}")]
        [HttpGet]

        public async Task<ActionResult> PrikaziMestaDrzave(int idDrzave){

            if(idDrzave <= 0){
                return BadRequest("Pogresan id");
            }

            try{

                    return Ok(await Context.Mesto.Where(p=>p.Drzava.ID == idDrzave).Select(p=> new{
                                Id = p.ID,
                                Naziv = p.Naziv
                            }).ToListAsync());
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
      
        }

        [Route("DodajMestoUDrzavi/{naziv}/{idDrzave}")]
        [HttpPost]

        public async Task<ActionResult> DodajMestoUDrzavi(string naziv,int idDrzave){

            if(string.IsNullOrEmpty(naziv) || naziv.Length > 20)
                return BadRequest("Pogresan naziv");

            if(idDrzave <=0){
                return BadRequest("Pogresan id");
            }    

            try{
                var drzava = await Context.Drzava.Where(p=>p.ID == idDrzave).FirstOrDefaultAsync();
                if(drzava != null){
                    Mesto m = new Mesto();
                    m.Naziv = naziv;
                    m.Drzava = drzava;
                    Context.Mesto.Add(m);
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
