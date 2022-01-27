import { Stranica } from "./stranica.js";
import {Drzava} from "./Drzava.js";

let listaDrzava = []

fetch("https://localhost:5001/Drzava/PrikaziDrzave")
.then(p=> {

    p.json().then(drzave => {
        drzave.forEach(drzava => {
            let d = new Drzava(drzava.id, drzava.naziv);
            listaDrzava.push(d);

        });

        let s = new Stranica(listaDrzava);
        s.crtaj(document.body);
    })
})

