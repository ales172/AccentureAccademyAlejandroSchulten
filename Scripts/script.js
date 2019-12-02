
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




let links = document.querySelectorAll(".link");
links.forEach(link => {
    link.addEventListener("click", e => {
        e.preventDefault();
        direccion(`${e.target.dataset.archivo}.html`)
    })
});