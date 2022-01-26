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
    public class ProizvodController : ControllerBase
    {

        public ProdavnicaContext Context { get; set; }


        public ProizvodController(ProdavnicaContext Context)
        {
            this.Context  = Context;
        }


        [Route("PrikaziProizvodeOdredjenogTipa/{idProdavnice}/{idTipa}/{cenaOd}/{cenaDo}")]
        [HttpGet]

        public async Task<ActionResult> PrikaziTipove(int idProdavnice, int idTipa, int cenaOd, int cenaDo){

            if(idProdavnice <= 0 || idTipa <=0 ){
                return BadRequest("Pogresan id");
            }
            try{

                var prodavnica = await Context.Prodavnica.Where(p=>p.ID == idProdavnice).FirstOrDefaultAsync();
                var tip = await Context.Tip.Where(p=>p.ID == idTipa).FirstOrDefaultAsync();

                if(prodavnica!= null && tip != null){

                    return Ok(await Context.Proizvod.Where(p=>p.Prodavnica == prodavnica && p.Tip == tip && p.Cena >= cenaOd && p.Cena <= cenaDo )
                    .Select(p => new {
                        Id = p.ID,
                        Naziv = p.Naziv,
                        Cena = p.Cena,
                        Kolicina = p.Kolicina
                    }).ToListAsync());
                }
                else{
                    return BadRequest("Nema trazenih podataka");
                }

            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
    
        }

        [Route("DodajProizvod/{naziv}/{cena}/{kolicina}/{idProdavnice}/{idTipa}")]
        [HttpPost]

        public async Task<ActionResult> DodajTip(string naziv, int cena, int kolicina, int idProdavnice, int idTipa){

            if(string.IsNullOrEmpty(naziv) || naziv.Length > 20){
                return BadRequest("Pogresan naziv");
            }
              if(idProdavnice <= 0 || idTipa <=0 ){
                return BadRequest("Pogresan id");
            }

            try{

                var prodavnica = await Context.Prodavnica.Where(p=>p.ID == idProdavnice).FirstOrDefaultAsync();
                var tip = await Context.Tip.Where(p=>p.ID == idTipa).FirstOrDefaultAsync();

                if(prodavnica != null && tip != null){
                    Proizvod p = new Proizvod();
                    p.Naziv = naziv;
                    p.Cena =  cena;
                    p.Kolicina = kolicina;
                    p.Prodavnica = prodavnica;
                    p.Tip=tip;
                    Context.Proizvod.Add(p);

                    await Context.SaveChangesAsync();

                    return Ok("Proizvod uspesno dodata");
                }
                else{
                    return BadRequest("Ne mozete dodati proizvod");
                }

            }
            catch(Exception ex){

                return BadRequest(ex.Message);
            }


        }

        [Route("IzmeniProizvod/{idProizvoda}/{cena}/{kolicina}")]
        [HttpPut]

        public async Task<ActionResult> IzmeniProizvod(int idProizvoda,int cena, int kolicina){

            if(idProizvoda <= 0)
                return BadRequest("Pogresan id proizvoda");

            try{
                var prodavnica = await Context.Prodavnica.Where(p=>p.ID == 1).FirstOrDefaultAsync();
                if(prodavnica != null){

                    var proizvod = await Context.Proizvod.Where(p=> p.ID == idProizvoda && p.Prodavnica == prodavnica).FirstOrDefaultAsync();

                    proizvod.Cena = cena;
                    proizvod.Kolicina = kolicina;

                    Context.Proizvod.Update(proizvod);

                    await Context.SaveChangesAsync();
                    return Ok(await Context.Proizvod.Where(p=> p.ID == idProizvoda)
                    .Select(p=> new{
                        id = p.ID,
                        Naziv = p.Naziv,
                        Kolicina = p.Kolicina,
                        Cena = p.Cena
                    }).FirstOrDefaultAsync());
                }
                else{
                    return BadRequest("Ne moze se izmeniti");
                }

            }   
            catch(Exception ex){
                return BadRequest(ex.Message);
            } 
        }

      
    }
}
