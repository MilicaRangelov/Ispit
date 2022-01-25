import { Polica } from "./Polica.js";

export class Klub {

    constructor(listaKlubova){
        this.listaKlubova = listaKlubova;
        this.listaPolica = [];
        this.kontejner = null;
    }

    crtaj(host){

        this.listaKlubova.forEach(k => {
            this.crtajKlub(host,k);
        });

        
    }

    crtajKlub(host,klub){

        var glavni =document.createElement("div");
        glavni.className = "Glavni";
        host.appendChild(glavni);

        var naziv = document.createElement("h1");
        naziv.innerHTML = "Video klub: " + klub.naziv;
        glavni.appendChild(naziv);

        var forma = document.createElement("div");
        forma.className = "Forma";
        glavni.appendChild(forma);

        var dodaj = document.createElement("div");
        dodaj.className = "DodajDvd";
        forma.appendChild(dodaj);

        var prikaz = document.createElement("div");
        prikaz.className = "Prikaz";
        forma.appendChild(prikaz);

        this.crtajPrikaz(prikaz,klub);

        this.crtajIzbor(dodaj,klub);

    }

    crtajIzbor(host,klub){

        let div = document.createElement("div");
        div.className = "divv";
        host.appendChild(div);

        let div2 = document.createElement("div");
        div2.className= "Unos";
        host.appendChild(div2);

        let p = document.createElement("p");
        p.innerHTML = "Dodaj dvd:";
        div2.appendChild(p);

        let box = document.createElement("input");
        box.type = "number";
        box.className = "numPick" + klub.id;
        div2.appendChild(box);

        let btn = document.createElement("button");
        btn.className = "Dodaj";
        btn.innerHTML = "Dodaj dvd";
        btn.onclick = (ev) => this.dodajNaPolicu(klub);
        div2.appendChild(btn);

        klub.crtajPolice(div);

    }

    crtajPrikaz(host,klub){

        let div = document.createElement("div");
        div.classList.add("divVrsta");
        div.classList.add("div" + klub.id);
        host.appendChild(div);

        div = document.createElement("div");
        div.classList.add("divPolice");
        div.classList.add("divPolice" + klub.id);
        host.appendChild(div);
    }

    dodajNaPolicu(klub){

        let rb = document.querySelector(`input[name=${klub.naziv}]:checked`).value;
        console.log(rb);
        let num = document.querySelector(".numPick" + klub.id).value;
        console.log(num);

        klub.dodaj(num,rb);
    }

}