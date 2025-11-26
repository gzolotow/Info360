document.addEventListener("DOMContentLoaded", () => {

    // LISTAS DE OBJETOS
    const reciclables = ["bollo.png", "botella.png", "lata.png", "vaso.png"];
    const noReciclables = ["pizza.png", "taper.png", "bolsa.png", "manzana.png", "vasito.png"];

    // ELEMENTOS
    const heroe = document.getElementById("heroe");
    const tacho = document.getElementById("tacho");
    const progresoBarra = document.getElementById("progreso");
    const overlay = document.getElementById("overlayCompletado");

    const sonidoOK = document.getElementById("sonidoOK");
    const sonidoError = document.getElementById("sonidoError");

    let progreso = 0;

    // ---------------------------
    // MOVIMIENTO SUAVE CON MOUSE
    // ---------------------------
    document.addEventListener("mousemove", (e) => {
        const x = e.clientX;

        heroe.style.left = (x - 70) + "px";
        tacho.style.left = (x - 20) + "px";
    });

    // ---------------------------
    // CREAR OBJETOS
    // ---------------------------
    function crearObjeto() {
        const objeto = document.createElement("img");
        objeto.classList.add("objeto");

        const esReciclable = Math.random() < 0.5;
        const lista = esReciclable ? reciclables : noReciclables;

        objeto.src = "img/" + lista[Math.floor(Math.random() * lista.length)];
        objeto.style.left = Math.random() * 85 + "%";

        document.body.appendChild(objeto);

        let y = -80;

        function caer() {
            y += 3;
            objeto.style.top = y + "px";

            if (y >= window.innerHeight - 180) {
                revisarChoque(objeto, esReciclable);
                return;
            }

            requestAnimationFrame(caer);
        }

        caer();
    }

    // ---------------------------
    // COLISIÓN Y PUNTOS
    // ---------------------------
    function revisarChoque(obj, esReciclable) {
        const objRect = obj.getBoundingClientRect();
        const tRect = tacho.getBoundingClientRect();

        const choca =
            objRect.left < tRect.right &&
            objRect.right > tRect.left &&
            objRect.bottom > tRect.top;

        if (choca) {
            if (esReciclable) {
                sumarPunto(obj);
            } else {
                restarPunto(obj);
            }
        } else {
            obj.remove();
        }
    }

    // ---------------------------
    // ANIMACIONES DE ACIERTO
    // ---------------------------
    function sumarPunto(obj) {
        sonidoOK.volume = 1.0;
        sonidoOK.currentTime = 0;
        sonidoOK.play();

        obj.classList.add("entrarTacho");
        setTimeout(() => obj.remove(), 500);

        progreso += 10;
        progresoBarra.style.width = progreso + "%";

        if (progreso >= 100) {
            overlay.classList.remove("hidden");
        }
    }

    // ---------------------------
    // ANIMACIONES DE ERROR
    // ---------------------------
    function restarPunto(obj) {
        sonidoError.volume = 1.0;
        sonidoError.currentTime = 0;
        sonidoError.play();

        obj.classList.add("explotar");
        setTimeout(() => obj.remove(), 300);

        progreso -= 5;
        if (progreso < 0) progreso = 0;
        progresoBarra.style.width = progreso + "%";
    }

    // CREAR OBJETOS CADA X TIEMPO
    setInterval(crearObjeto, 1200);

});
