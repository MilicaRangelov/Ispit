
export class Soba{

    constructor(id, brSobe){
        this.id = id;
        this.brSobe = brSobe;
        this.br = 0;
    }

    crtaj(host, id){

        let dod = document.querySelector(".inpDatumOd").value;
        console.log(dod);
        let ddo = document.querySelector(".inpDatumDo").value
        console.log(ddo);

        let kap = document.querySelector(".inpKapacitet").value;
        console.log(kap);

        fetch("https://localhost:5001/Soba/PrikaziDostupneRezervacije/" + dod + "/" + ddo + "/" + kap + "/" + id,{
            method:"GET"
        })
        .then(p => {
            if(p.ok){

                p.json().then(dostupno =>{
                    dostupno.forEach(dost => {
                        
                        let div = document.createElement("div");
                        div.className = "divDostupno";
                        host.appendChild(div);

                        let l = document.createElement("label");
                        l.innerHTML = dost.brSobe;
                        div.appendChild(l);

                        l=document.createElement("label");
                        l.innerHTML = dost.datumOdd + " - " + dost.datumDoo;
                        div.appendChild(l);

                        l=document.createElement("label");
                        l.innerHTML = dost.kapacitet;
                        div.appendChild(l);

                        
                        l=document.createElement("label");
                        l.innerHTML = dost.zauzeto;
                        div.appendChild(l);
                        console.log(dost.zauzeto);
                        if(dost.zauzeto === "slobodno"){
                            this.br = this.br+1;
                        }
                    });
                })
            }
        })
    }

    broj(){
        return this.br;
    }
}