// Arrays de imágenes
const reciclables = [
    "/img/botella.png",
    "/img/otella2.png",
    "/img/botella3.png",
    "/img/lata.png",
    "/img/papel.png",
    "/img/carton.png"
];

const noReciclables = [
    "/img/banana.png",
    "/img/pizza.png",
    "/img/aper.png",
    "/img/vaso.png",
    "/img/vasito.png"
];

// DOM
const player = document.getElementById("player");
const fallingContainer = document.getElementById("falling-container");
const progressBar = document.getElementById("progressBar");
const overlayCompletado = document.getElementById("overlayCompletado");

const soundBien = document.getElementById("soundBien");
const soundMal = document.getElementById("soundMal");
const soundError = document.getElementById("soundError");

let progreso = 0;
let juegoTerminado = false;

// Movimiento del jugador
document.addEventListener("mousemove", (e) => {
    const limite = window.innerWidth - player.offsetWidth;
    let x = e.clientX - player.offsetWidth / 2;

    if (x < 0) x = 0;
    if (x > limite) x = limite;

    player.style.left = x + "px";
});

// Crear objetos
function spawnObject() {
    if (juegoTerminado) return;

    const obj = document.createElement("img");
    obj.classList.add("falling-object");

    const tipo = Math.random() < 0.6 ? "rec" : "no";
    obj.dataset.tipo = tipo;

    obj.src = `/images/${tipo === "rec" ? reciclables.random() : noReciclables.random()}`;
    obj.style.left = Math.random() * (window.innerWidth - 100) + "px";
    obj.style.top = "-120px";

    fallingContainer.appendChild(obj);

    fall(obj);
}

// Caída
function fall(obj) {
    let y = -120;
    let speed = 3 + Math.random() * 2.5;

    const interval = setInterval(() => {
        if (juegoTerminado) {
            obj.remove();
            clearInterval(interval);
            return;
        }

        y += speed;
        obj.style.top = y + "px";

        if (checkCollision(obj, player)) {
            if (obj.dataset.tipo === "rec") recogerBien(obj);
            else recogerMal(obj);

            clearInterval(interval);
        }

        if (y > window.innerHeight) {
            obj.remove();
            clearInterval(interval);
        }

    }, 16);
}

// ✔ Buena recolección
function recogerBien(obj) {
    soundBien.volume = 1;
    soundBien.play();

    obj.classList.add("absorb");

    progreso += 4;
    updateBarra();

    setTimeout(() => obj.remove(), 300);
}

// ❌ Mala recolección
function recogerMal(obj) {
    soundMal.volume = 1;
    soundMal.play();

    obj.classList.add("explode");

    progreso -= 6;
    if (progreso < 0) progreso = 0;

    soundError.volume = 1;
    soundError.play();

    updateBarra();

    setTimeout(() => obj.remove(), 250);
}

// Barra
function updateBarra() {
    progressBar.style.width = progreso + "%";

    if (progreso >= 100) {
        terminarNivel();
    }
}

// Terminar nivel
function terminarNivel() {
    juegoTerminado = true;
    setTimeout(() => overlayCompletado.classList.remove("hidden"), 400);
}

// Helper
Array.prototype.random = function () {
    return this[Math.floor(Math.random() * this.length)];
};

// Spawn constante
setInterval(spawnObject, 850);

// DETECTOR DE COLISION
function checkCollision(a, b) {
    const A = a.getBoundingClientRect();
    const B = b.getBoundingClientRect();

    return !(
        A.bottom < B.top ||
        A.top > B.bottom ||
        A.right < B.left ||
        A.left > B.right
    );
}
