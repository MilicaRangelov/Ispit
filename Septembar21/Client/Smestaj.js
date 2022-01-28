import { Soba } from "./Soba.js";

export class Smestaj{

    constructor(id, naziv){
        this.id = id;
        this.naziv = naziv;
    }

    crtaj(host){

        let div = document.createElement("div");
        div.className = "divSmestaj";
        host.appendChild(div);

        let div1 = document.createElement("div");
        div1.className = "div" + this.naziv;
        div.appendChild(div1);

        let p  = document.createElement("p");
        p.className = "p" + this.naziv;
        p.innerHTML =this.naziv;
        div1.appendChild(p);

        div1 = document.createElement("div");
        div1.className = "divSobe";
        div.appendChild(div1);

        this.crtajSobe(div1);

    }

    obrisiDecu(host){
        while(host.lastChild){
            host.removeChild(host.lastChild);
        }
    }

    crtajSobe(host){

      this.obrisiDecu(host);
      let br = 0;

      fetch("https://localhost:5001/Soba/PrikaziSobeSmestaja/" + this.id,{
          method:"GET"
      })
      .then(p=>{

        if(p.ok){
            p.json().then(sobe =>{
                sobe.forEach(soba => {
                    let s = new Soba(soba.id, soba.brSobe);
                    s.crtaj(host,this.id);
                    let br2 = s.broj();
                    console.log(`br slobodnih soba ${br2}`);
                    br = br+br2;
                });
                console.log(`br: ${br}`);
                if(br == 0){
                    this.obrisiDecu(host);
                }
            })
        }

      })
    }
}