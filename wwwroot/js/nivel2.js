/* OBJETOS RECICLABLES (según tus imágenes reales) */
const reciclables = [
    "lata.png",
    "botella.png",
    "bolsa.png",
    "diario.png",
    "bollo.png"
];

/* OBJETOS NO RECICLABLES (basura real) */
const noReciclables = [
    "banana.png",
    "pizza.png",
    "taper.png",
    "vaso.png",
    "vasito.png"
];

/* LISTA COMPLETA */
const objetos = [...reciclables, ...noReciclables];

/* ELEMENTOS */
const personaje = document.getElementById("personaje");
const tacho = document.getElementById("tacho");
const juego = document.getElementById("juego-area");

/* --------------------------------------------- */
/* MOVIMIENTO DEL PERSONAJE Y TACHO */
/* --------------------------------------------- */
document.addEventListener("mousemove", (e) => {
    let x = e.clientX;
    personaje.style.left = x + "px";
    tacho.style.left = x + "px";
});

/* --------------------------------------------- */
/* CREACIÓN DE OBJETOS QUE CAEN */
/* --------------------------------------------- */
function crearObjeto() {
    const obj = document.createElement("img");

    const img = objetos[Math.floor(Math.random() * objetos.length)];
    obj.src = `/img/${img}`;
    obj.classList.add("objeto");
    obj.style.left = Math.random() * 90 + "vw";

    juego.appendChild(obj);

    /* DETECTAR COLISIÓN */
    let check = setInterval(() => {
        if (colision(obj, tacho)) {

            if (reciclables.includes(img)) {
                console.log("✔ Reciclable atrapado");
            } else {
                console.log("✘ Basura atrapada");
            }

            obj.remove();
            clearInterval(check);
        }
    }, 50);

    obj.addEventListener("animationend", () => obj.remove());
}

/* CREA UN OBJETO CADA 1.1s */
setInterval(crearObjeto, 1100);

/* --------------------------------------------- */
/* SISTEMA DE COLISIÓN */
/* --------------------------------------------- */
function colision(a, b) {
    const r1 = a.getBoundingClientRect();
    const r2 = b.getBoundingClientRect();

    return !(
        r1.top > r2.bottom ||
        r1.bottom < r2.top ||
        r1.right < r2.left ||
        r1.left > r2.right
    );
}
