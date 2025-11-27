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

document.addEventListener('DOMContentLoaded', () => {
    const personaje = document.getElementById('personaje');
    const barraProgreso = document.getElementById('barra-progreso');
    const overlay = document.getElementById('overlayCompletado');
    let posX = window.innerWidth / 2;
    let progreso = 0;
    const objetivo = 100;

    // ---------------------------
    // MOVIMIENTO SUAVE CON MOUSE
    // ---------------------------
    document.addEventListener("mousemove", (e) => {
        const x = e.clientX;

        heroe.style.left = (x - 70) + "px";
        tacho.style.left = (x - 20) + "px";
    // Movimiento del personaje
    document.addEventListener('mousemove', (e) => {
        posX = e.clientX - 40;
        personaje.style.left = `${posX}px`;
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
    // Actualizar barra
    function actualizarBarra() {
        barraProgreso.style.width = `${progreso}%`;
        if (progreso >= objetivo) {
            mostrarOverlay();
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
    // Mostrar overlay
    function mostrarOverlay() {
        overlay.classList.remove('hidden');
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
    // Crear residuos con velocidad y rotación aleatoria
    function crearResiduo() {
        const residuos = ['banana', 'bollo', 'botella', 'lata', 'pizza', 'taper', 'vasito', 'diario'];
        const tipo = residuos[Math.floor(Math.random() * residuos.length)];
        const img = document.createElement('img');
        img.src = `img/${tipo}.png`;
        img.className = 'residuo';
        img.dataset.tipo = (tipo === 'botella' || tipo === 'bollo' || tipo === 'diario') ? 'verde' : 'negro';
        img.style.left = `${Math.random() * (window.innerWidth - 50)}px`;

        // Rotación aleatoria
        const rotacion = Math.floor(Math.random() * 360);
        img.style.transform = `rotate(${rotacion}deg)`;

        document.getElementById('contenedor-residuos').appendChild(img);

        let posY = -50;
        const velocidad = Math.random() * 3 + 3; // Entre 3 y 6 px por ciclo
        const intervalo = setInterval(() => {
            posY += velocidad;
            img.style.top = `${posY}px`;

            const personajeRect = personaje.getBoundingClientRect();
            const residuoRect = img.getBoundingClientRect();
            if (
                residuoRect.bottom >= personajeRect.top &&
                residuoRect.left < personajeRect.right &&
                residuoRect.right > personajeRect.left
            ) {
                if (img.dataset.tipo === 'verde') {
                    progreso += 10;
                    if (progreso > 100) progreso = 100;
                    barraProgreso.classList.remove('rojo');
                } else {
                    progreso -= 10;
                    if (progreso < 0) progreso = 0;
                    barraProgreso.classList.add('rojo');
                    setTimeout(() => barraProgreso.classList.remove('rojo'), 500);
                }
                actualizarBarra();
                img.remove();
                clearInterval(intervalo);
            }

        progreso -= 5;
        if (progreso < 0) progreso = 0;
        progresoBarra.style.width = progreso + "%";
            if (posY > window.innerHeight) {
                img.remove();
                clearInterval(intervalo);
            }
        }, 50);
    }

    // CREAR OBJETOS CADA X TIEMPO
    setInterval(crearObjeto, 1200);

    setInterval(crearResiduo, 2000);
});