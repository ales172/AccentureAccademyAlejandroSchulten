
let i = 1;
function agregaAutor() {
    let tbl = document.getElementById("tablaCrea");
    let tblBody = document.getElementById("tbCreaAutor");
    let hilera = document.createElement("tr")
    hilera.setAttribute("id",(i));
    let celda = document.createElement("td");
    celda.innerHTML = '<tr><td class="Autores"><input  id="Nombre" type="text" name="Autores['+i+']" placeholder="Nombre autor" value="" /></td ></tr>';
    i++;
    hilera.appendChild(celda);
    tblBody.appendChild(hilera);
    tbl.appendChild(tblBody);
}




let btnOtroAutor = document.querySelector("#otroAutor");
btnOtroAutor.addEventListener("click", e => {
    e.preventDefault();
    agregaAutor();
});


let main = document.querySelector("main");

// btnMenu = document.querySelector("#btnMenu");
//let nav = document.querySelector("#navMenu");
//btnMenu.addEventListener("click", () => nav.classList.toggle("abierto"))

function direccion(url) {
    let xhr = new XMLHttpRequest;
    xhr.open("GET", url);
    xhr.addEventListener("readystatechange", () => {
        if (xhr.readyState == 4 && xhr.status == 200) {
            main.innerHTML = xhr.response;
            nav.classList.toggle("abierto")

        }
    })
    xhr.send();
}

let links = document.querySelectorAll(".link");
links.forEach(link => {
    link.addEventListener("click", e => {
        e.preventDefault();
        direccion(`${e.target.dataset.archivo}.html`)
    })
})*/
/*
usuarios.forEach(usuario => {
    let div = document.createElement("div")
    let h2 = document.createElement("h2")
    h2.innerText = usuario.name
    let p = document.createElement("p")
    p.innerText = usuario.company.catchPhrase
    let a = document.createElement("a")
    a.href = "http://" + usuario.website
    a.innerText = "ver mas..."
    div.appendChild(h2)
    div.appendChild(p)
    div.appendChild(a)
    div.className = "card"
    console.log(div)
    s.className = "sectionTest"
    s.appendChild(div)
    m.appendChild(s);
});*/
//document.body.appendChild(s);
//m.appendChild(s);