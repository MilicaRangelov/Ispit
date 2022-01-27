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
    public class DrzavaController : ControllerBase
    {
       
       public AplikacijaContext Context { get; set; }

        public DrzavaController(AplikacijaContext cont)
        {
           Context = cont;
        }

        [Route("PrikaziDrzave")]
        [HttpGet]

        public async Task<ActionResult> PrikaziDrzave(){

            return Ok(await Context.Drzava.Select(p=> new{
                Id = p.ID,
                Naziv = p.Naziv
            }).ToListAsync());
        }

        [Route("DodajDrzavu/{naziv}")]
        [HttpPost]

        public async Task<ActionResult> DodajDrzavu(string naziv){
            if(string.IsNullOrEmpty(naziv) || naziv.Length > 20)
                return BadRequest("Pogresan naziv");

            try{

                Drzava d = new Drzava();
                d.Naziv = naziv;
                Context.Drzava.Add(d);
                await Context.SaveChangesAsync();
                return Ok("Uspesno dodata drzava");
            }   
            catch(Exception ex){
                return BadRequest(ex.Message);
            } 
        }

    }
}
