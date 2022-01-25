import {Klub} from "./Klub.js";
import {VideoKlub} from "./VideoKlub.js"

let listKlubova = [];

fetch("https://localhost:5001/VideoKlub/VideotekePrikaz")
.then(el => {

    el.json().then(klubovi => {
        klubovi.forEach(k => {
            
            var kl = new VideoKlub(k.id,k.naziv);
            listKlubova.push(kl);
        });
        console.log(listKlubova);

        var kk = new Klub(listKlubova);
        kk.crtaj(document.body);
    })

})

