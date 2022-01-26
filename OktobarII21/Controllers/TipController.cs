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
    public class TipController : ControllerBase
    {

        public ProdavnicaContext Context { get; set; }


        public TipController(ProdavnicaContext Context)
        {
            this.Context  = Context;
        }


        [Route("PrikaziTipove")]
        [HttpGet]

        public async Task<ActionResult> PrikaziTipove(){

            return Ok(await Context.Tip.Select(p => new{
                Id = p.ID,
                Naziv = p.Naziv
            }).ToListAsync());
        }

        [Route("DodajTip/{naziv}")]
        [HttpPost]

        public async Task<ActionResult> DodajTip(string naziv){

            if(string.IsNullOrEmpty(naziv) || naziv.Length > 20){
                return BadRequest("Pogresan naziv");
            }

            try{
                Tip p = new Tip();
                p.Naziv = naziv;
                Context.Tip.Add(p);

                await Context.SaveChangesAsync();

                return Ok("Tip uspesno dodata");

            }
            catch(Exception ex){

                return BadRequest(ex.Message);
            }


        }

      
    }
}
