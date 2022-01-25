import {Polica} from "./Polica.js";

export class VideoKlub{

    constructor(id, naziv){
        this.id = id;
        this.naziv = naziv;
        this.listaPolica = [];
    }


    crtajPolice(host){
        fetch("https://localhost:5001/Police/PrikaziPoliceVideoteke/" + this.id ,{
            method: "GET"
        })
        .then(s => {

            if(s.ok){

                s.json().then(police =>{

                    police.forEach(polica => {

                        let p = new Polica(polica.id, polica.naziv, polica.velicina);
                        this.listaPolica.push(p);
                    });
                    console.log(this.listaPolica);
                    this.crtajIzbor(host);
                })
            }

        })

    }

    crtajIzbor(host){

        let pom = document.querySelector(".div" + this.id);
        this.obrisiDecu(pom);

        let pom2 = document.querySelector(".divPolice" + this.id);
        this.obrisiDecu(pom2);

        this.listaPolica.forEach(p=>{
            let div = document.createElement("div");
            div.className="dugmici";
            host.appendChild(div);

            let rb = document.createElement("input");
            rb.type = "radio";
            rb.name= this.naziv;
            rb.value = p.id;
            console.log(p.id);
            div.appendChild(rb);

            let lab = document.createElement("label");
            lab.innerHTML = p.naziv;
            div.appendChild(lab);

      
        
            this.crtajPrikaz(pom,p);
            this.crtajPrikazPolica(pom2,p);

        });

    }

    obrisiDecu(div){

        while (div.lastChild) {
           div.removeChild(div.lastChild);
        }
    }

    crtajPrikaz(div,el){

        let p = document.createElement("p");
        p.innerHTML = el.naziv;
        div.appendChild(p);

    }

    crtajPrikazPolica(host,p){
        
        let  klasa = p.naziv;;
        if(p.naziv.includes(" ")){
            klasa = klasa.split("").filter((el)=> el !== " ").join("");
            console.log(klasa);
        }

        let divPom = document.createElement("div");
        divPom.className = "divPomocni";
        host.appendChild(divPom);

        let div = document.createElement("div");
        div.classList.add("divP");
        div.classList.add("div" + klasa);
        divPom.appendChild(div);

        let par = document.createElement("p");
        p.className = "p" + klasa;
        par.innerHTML = "/" + p.velicina;
        divPom.appendChild(par);

        this.crtajDvd(div, par, p);

    }

    crtajDvd(div, par, p){

        fetch("https://localhost:5001/Police/PrikazibrojDvd/" + p.id + "/" + this.id ,{
            method: "GET"
        })
        .then(s => {

            if(s.ok){
                
                let broj;
                this.obrisiDecu(div);

                s.json().then(dvd=>{

                    let z = par.innerHTML;
                    console.log(z);
                    par.innerHTML = dvd.zauzeto + z;
                    console.log(`dvd.zauzeto ${dvd.zauzeto}`)
                    broj = dvd.zauzeto;
                    console.log(`Zauzeto ${broj}`);
                    console.log(div.clientWidth);
                    if(broj !== 0){
                        let wid = div.clientWidth / p.velicina;
                        console.log(`Velicina diva je ${wid}`);
                        for(let i=0; i < broj; i++){
        
                            this.crtajD(div,wid);
        
                        }

                    }

                })
              
             
            }
        })
    }

    crtajD(div,vel){

        let dete = document.createElement("div");
        dete.className = "bojaDvd";
        dete.style.width = vel+"px"; 
        div.appendChild(dete);

    }


    dodaj(num,rb){

        fetch("https://localhost:5001/Police/IzmeniDvdNaPolicu/" + this.id + "/" + rb + "/" + num,{
            method:"PUT"
        })
        .then(s=>{
            if(s.ok){
                console.log(this.listaPolica[0].id);
                console.log(rb);
                let j=0;
                while(this.listaPolica[j].id != rb)
                {
                    console.log(this.listaPolica[j].id);
                    console.log(this.listaPolica[j] + ` , j= ${j}`);
                     j=j+1;
                }
              
              console.log(this.listaPolica[j]);
              let klasa =this.listaPolica[j].naziv;

              if(klasa.includes(" ")){
                klasa = klasa.split("").filter((el)=> el !== " ").join("");
                console.log(klasa);
              }

              let div = document.querySelector(".div"+klasa);
            
              let vel = div.clientWidth / this.listaPolica[j].velicina;
              console.log(vel);
              
              for(let i=0; i<num ;i++){
                let dodaj = document.createElement("div");
                dodaj.className = "bojaDvd";
                dodaj.style.width = vel +"px";
                div.appendChild(dodaj);
              }

            }
        })

        

    }

}