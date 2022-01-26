import {Pocetna} from "./Pocetna.js";
import {Prodavnica} from "./Prodavnica.js"; 

//ucitavanje prodavnica


fetch("https://localhost:5001/Prodavnicat/PrikaziProdavnice")
.then(p => {

    let listaProdanica = [];
    p.json().then(prodavnice => {

        prodavnice.forEach(prodavnica => {
            let prod = new Prodavnica(prodavnica.id,prodavnica.naziv);
            listaProdanica.push(prod);
        });
        let poc = new Pocetna(listaProdanica);
        poc.crtajSveProdavnice(document.body);

    })
})


