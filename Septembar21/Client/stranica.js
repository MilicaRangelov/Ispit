import { Mesto } from "./Mesto.js";
import { Smestaj } from "./Smestaj.js";

export class Stranica{

    constructor(listaDrzava){
        this.listaDrzava = listaDrzava;
        this.kontejner = null;
    }

    crtaj(host){
        this.kontejner = host;
        let div = document.createElement("div");
        div.className = "divGlavni";
        this.kontejner.appendChild(div);

        let div1 =  document.createElement("div");
        div1.className = "divIzbor";
        div.appendChild(div1);

        this.crtaIzbor();

        div1= document.createElement("div");
        div1.className = "divPrikaz";
        div.appendChild(div1);
        this.crtajPrikaz();
    }

    crtaIzbor(){

        let div = this.kontejner.querySelector(".divIzbor");

        let pom = document.createElement("div");
        pom.className = "divDrzava";
        div.appendChild(pom);

        this.crtajDrzave(pom);

        pom = document.createElement("div");
        pom.className = "divRezervacija";
        div.appendChild(pom);

        this.crtaRezervaciju(pom);

        pom = document.createElement("div");
        pom.className = "divPretrazi";
        div.appendChild(pom);

        let btn = document.createElement("button");
        btn.innerHTML = "Pretrazi";
        btn.onclick = (el) => this.pretrazi();
        pom.appendChild(btn);

    }

    crtajDrzave(host){

        let l = document.createElement("label");
        l.innerHTML = "Drzava: ";
        host.appendChild(l);
        this.listaDrzava.forEach(d =>{
            let div = document.createElement("div");
            div.className = "divDiv";
            host.appendChild(div);

            let rb = document.createElement("input");
            rb.type = "radio";
            rb.name = "drzava";
            rb.value = d.id;
            rb.onclick = (ev) =>this.rbPromenjeno();
            div.appendChild(rb);

            let lb = document.createElement("label");
            lb.innerHTML =d.naziv;
            div.appendChild(lb);
        })

    }

    crtaRezervaciju(host){

        let list = ["Mesto", "DatumOd" , "DatumDo" , "Kapacitet"];
        
        list.forEach(el => {

            let div = document.createElement("div");
            div.className = "divDiv";
            host.appendChild(div);

            let l = document.createElement("label");
            l.innerHTML = el;
            div.appendChild(l);

            if(el === "Mesto"){
                let se = document.createElement("select");
                div.appendChild(se);

            }
            else{
                let inp = document.createElement("input");
                inp.type = "text";
                inp.className = "inp" + el;
                div.appendChild(inp);
            }
        });
    }

    crtajPrikaz(){

        let div = this.kontejner.querySelector(".divPrikaz");
        let p = document.createElement("p");
        p.innerHTML = "Dostupni smestaj";
        div.appendChild(p);
    }
    rbPromenjeno(){
        let rb = document.querySelector( 'input[name="drzava"]:checked'); 
        console.log(rb);
        if(rb != null)
            this.prikaziMesta(rb.value);
    }

    prikaziMesta(id){

        let se = this.kontejner.querySelector("select");
        this.obrisiDecu(se);

        fetch("https://localhost:5001/Mesto/PrikaziMestaDrzave/" + id, {
            method:"GET"
        })
        .then(p=> {
            if(p.ok){
                p.json().then(mesta =>{
                    mesta.forEach(mesto =>{
                        let m = new Mesto(mesto.id, mesto.naziv);
                        let op = document.createElement("option");
                        op.value = m.id;
                        op.innerHTML = m.naziv;
                        se.appendChild(op);
                    })
                })
            }
        })
    }

    obrisiDecu(host){
        while(host.lastChild){
            host.removeChild(host.lastChild);
        }
    }

    pretrazi(){

        let se = document.querySelector("select");
        let opt = se.options[se.selectedIndex].value;
        console.log(opt);

        let iv = this.kontejner.querySelector(".divPrikaz");
        console.log(iv);
        this.obrisiDecu(iv);

        fetch("https://localhost:5001/SmestajniObjekat/PrikaziObjekteMesta/" + opt, {
            method:"GET"
        })
        .then(p=> {
            if(p.ok){

                p.json().then(smestaji =>{

                    smestaji.forEach(smestaj => {
                        let s = new Smestaj(smestaj.id, smestaj.naziv);
                        s.crtaj(iv);
                    });
                })
            }
        })

    }

}