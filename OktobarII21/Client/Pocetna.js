import { Prodavnica } from "./Prodavnica.js";
import { Tip } from "./Tip.js";
import { Proizvod } from "./Proizvod.js";

export class Pocetna{

    constructor(listaProdavnica){
        this.listaProdavnica = listaProdavnica;
        this.kontejner = null;
    }

    crtajSveProdavnice(host){

        this.listaProdavnica.forEach(el => {
            this.crtaj(host,el.naziv,el);
        });
    }

    crtaj(host,naziv,el){

        this.kontejner = host;
        let h = document.createElement("h1");
        h.innerHTML = "Online prodavnica: " + naziv;
        this.kontejner.appendChild(h);

        let div = document.createElement("div");
        div.className = "divGlavni";
        this.kontejner.appendChild(div);

        let div1 = document.createElement("div");
        div1.className = "divIzaberi";
        div.appendChild(div1);

        let div2 = document.createElement("div");
        div2.className= "divPrikaz";
        div.appendChild(div2);

        this.crtajIzbor(div1,el);
        this.crtajPrikaz(div2);

    }

    crtajPrikaz(host){

        let div = document.createElement("div");
        div.className= "divTabela";
        host.appendChild(div);

        this.crtajTabelu(div);

        let div2 =  document.createElement("div");
        div2.className = "divKorpa";
        host.appendChild(div2);

        this.crtajKorpu(div2);
    }

    crtajIzbor(host,el){


        let div = document.createElement("div");
        div.className = "divPretraga";
        host.appendChild(div);

        this.crtajPretragu(div,el);

        let div2 = document.createElement("div");
        div2.className= "divIzmena";
        host.appendChild(div2);

        this.crtajIzmene(div2,el);
    }

    crtajTabelu(host){

        let tabela=document.createElement("table");
        host.appendChild(tabela);

        let th = document.createElement("thead");
        tabela.appendChild(th);

        let tb = document.createElement("tbody");
        tb.className = "tabelaBody";
        tabela.appendChild(tb);

        let tr = document.createElement("tr");
        th.appendChild(tr);

        let th1 = document.createElement("th");
        th1.innerHTML = "Naziv";
        tr.appendChild(th1);

        th1 = document.createElement("th");
        th1.innerHTML = "Cena";
        tr.appendChild(th1);

        th1 = document.createElement("th");
        th1.innerHTML = "Kolicina";
        tr.appendChild(th1);

        th1 = document.createElement("th");
        th1.innerHTML = "Izmeni";
        tr.appendChild(th1);

        th1 = document.createElement("th");
        th1.innerHTML = "Korpa";
        tr.appendChild(th1);
        

    }

    crtajKorpu(host){

        let p = document.createElement("p");
        p.innerHTML = "Korpa";
        host.appendChild(p);

        let btn =  document.createElement("button");
        btn.innerHTML = "Poruci";
        btn.className = "btnPoruci"
        host.appendChild(btn);
    }

    crtajPretragu(host,el){

        let p = document.createElement("p");
        p.innerHTML = "Pretrazi";
        host.appendChild(p);

        let div = document.createElement("div");
        div.className = "divPom";
        host.appendChild(div);
        

        let l = document.createElement("label");
        l.innerHTML = "Tip: ";
        div.appendChild(l);

        let se = document.createElement("select");
        se.className = "selectEl";
        div.appendChild(se);

        this.crtajTipove(se);

        div = document.createElement("div");
        div.className = "divPom";
        host.appendChild(div);
        

        l = document.createElement("label");
        l.innerHTML = "CenaOd: ";
        div.appendChild(l);

        let inp = document.createElement("input");
        inp.type = "number";
        inp.className = "CenaOd";
        div.appendChild(inp);

        div = document.createElement("div");
        div.className = "divPom";
        host.appendChild(div);
        

        l = document.createElement("label");
        l.innerHTML = "CenaDo: ";
        div.appendChild(l);


        inp = document.createElement("input");
        inp.type = "number";
        inp.className = "CenaDo";
        div.appendChild(inp);


        let btn = document.createElement("button");
        btn.className ="btnPrikaz";
        btn.onclick = (ev) => this.prikaziProizvode(el);
        btn.innerHTML = "Prikazi";

        host.appendChild(btn);

    }

    crtajIzmene(host,el){

        let p = document.createElement("p");
        p.className = "pIzmena";
        p.innerHTML = "Izmeni: ";
        host.appendChild(p);

        let div = document.createElement("div");
        div.className = "divPom";
        host.appendChild(div);

        let l = document.createElement("label");
        l.innerHTML = "Cena: ";
        div.appendChild(l);

        let inp = document.createElement("input");
        inp.type = "number";
        inp.className = "Cena";
        div.appendChild(inp);

        div = document.createElement("div");
        div.className = "divPom";
        host.appendChild(div);
        

        l = document.createElement("label");
        l.innerHTML = "Kolicina: ";
        div.appendChild(l);


        inp = document.createElement("input");
        inp.type = "number";
        inp.className = "Kolicina";
        div.appendChild(inp);


        let btn = document.createElement("button");
        btn.className ="btnIzmeni";
        btn.onclick = (ev) => this.fetchIzmeni();
        btn.innerHTML = "Izmeni";

        host.appendChild(btn);



    }

    crtajTipove(host){

        fetch("https://localhost:5001/Tip/PrikaziTipove")
        .then(p=>{
            p.json().then(tipovi => {

                tipovi.forEach(tip => {
                    let t = new Tip(tip.id,tip.naziv);
                    let op = document.createElement("option");
                    op.value = t.id;
                    op.innerHTML = t.naziv;
                    host.appendChild(op);
                });


            })
        })
    }

    obrisi(host){
        while(host.lastChild){
            host.removeChild(host.lastChild);
        }
    }

    prikaziProizvode(el){
        var tb = document.querySelector(".tabelaBody");
        this.obrisi(tb);

        var se = this.kontejner.querySelector("select");
        var id = se.options[se.selectedIndex].value;

        this.fetchProizvodi(tb,id,el);
    }

    fetchProizvodi(tb,id,el){
        let cenaOd = this.kontejner.querySelector(".CenaOd").value;
        let cenaDo = this.kontejner.querySelector(".CenaDo").value;

        fetch("https://localhost:5001/Proizvod/PrikaziProizvodeOdredjenogTipa/" + el.id + "/" + id + "/" + cenaOd + "/" + cenaDo ,{
            method:"GET"
        })
        .then(p => {

            if(p.ok){

                p.json().then(proizvodi => {
                    proizvodi.forEach(proizvod => {

                        let proi = new Proizvod(proizvod.id, proizvod.naziv, proizvod.cena, proizvod.kolicina);
                        let tr = document.createElement("tr");
                        tr.className = "tr" + proizvod.id;
                        tb.appendChild(tr);

                        let td = document.createElement("td");
                        td.innerHTML = proizvod.naziv;
                        td.className = "tdNaziv";
                        tr.appendChild(td);

                        td = document.createElement("td");
                        td.innerHTML = proizvod.cena;
                        td.className = "tdCena";
                        tr.appendChild(td);

                        td = document.createElement("td");
                        td.innerHTML = proizvod.kolicina;
                        td.className = "tdKolicina";
                        tr.appendChild(td);

                        td = document.createElement("td");
                        let btn = document.createElement("button");
                        btn.innerHTML = "Izmeni";
                        btn.onclick = (ev) => this.izmeniProizvod(proi);
                        td.appendChild(btn);
                        tr.appendChild(td);

                        td = document.createElement("td");
                        btn = document.createElement("button");
                        btn.innerHTML = "Kupi";
                        btn.onclick = (ev) => this.kupiProizvod(proi);
                        td.appendChild(btn);
                        tr.appendChild(td);


                        
                    });
                })
            }
        })
    }

    izmeniProizvod(proizvod){

        let p = this.kontejner.querySelector(".pIzmena");
        p.innerHTML ="Izmeni:" + proizvod.naziv;
        p.value = proizvod.id;
        console.log(p.value);

    }

    fetchIzmeni(){

        let p = this.kontejner.querySelector(".pIzmena").value;
        console.log(`p value ${p}`);
        let val = this.kontejner.querySelector(".Cena").value;
        let kol = this.kontejner.querySelector(".Kolicina").value;
        let tabela = this.kontejner.querySelector(".tabelaBody");
        let tr = tabela.querySelector(".tr" + p);
        let td1 = tr.querySelector(".tdCena");
        let td2 = tr.querySelector(".tdKolicina");
        console.log(val + kol);
        console.log(`tabela ${tabela}`);
        console.log(`tr ${tabela}`);
        console.log(`td1 ${td1}`);
        console.log(`td1 ${td2}`);


        fetch("https://localhost:5001/Proizvod/IzmeniProizvod/" + p + "/" + val + "/" + kol,{
            method:"PUT"
        })
        .then(p=>{

            if(p.ok){

                p.json().then(proizvod =>{
                    td1.innerHTML = proizvod.cena;
                    td2.innerHTML = proizvod.kolicina;
                })
            
            }
        })
    }

    kupiProizvod(proizvod){

        let div = this.kontejner.querySelector(".divKorpa");
        let p = div.querySelector("." + proizvod.naziv);

        if(p !=null){

            p.value = p.value + 1;
          
        }
        else{
            p = document.createElement("p");
            p.className = proizvod.naziv;
            p.value = 1;
            div.appendChild(p);
        }
        p.innerHTML = proizvod.naziv + " " +p.value;

    }

}