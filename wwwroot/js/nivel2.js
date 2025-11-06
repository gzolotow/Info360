const objetos = [
    { src: '~/img/banana.png', reciclable: false },
    { src: '~/img/bolsa.png', reciclable: true },
    { src: '~/img/botella.png', reciclable: true }, 
    { src: '~/img/pizza.png', reciclable: false },
    { src: '~/img/manzana.png', reciclable: false },
    { src: '~/img/vaso.png', reciclable: true },
    { src: '~/img/tupper.png', reciclable: false },
    { src: '~/img/diario.png', reciclable: true },
];

// Estado objetos en pantalla
const objetosEnPantalla = [];

// Áreas límites
const screenWidth = window.innerWidth;
const screenHeight = window.innerHeight;

// Función para crear objeto volante con posiciones y velocidades aleatorias
function crearObjetoVolante() {
    if (!gameActive) return;

    const index = Math.floor(Math.random() * objetos.length);
    const objData = objetos[index];

    const objetoEl = document.createElement('img');
    objetoEl.src = objData.src;
    objetoEl.classList.add('nivel2-objeto');
    objetoEl.dataset.reciclable = objData.reciclable ? 'true' : 'false';

    // Posición inicial en borde izquierdo o derecho aleatorio y altura aleatoria
    const startSide = Math.random() < 0.5 ? 'left' : 'right';
    const posY = Math.random() * (screenHeight * 0.6) + 50; // altura entre 50px y 60% pantalla

    // Posición X inicial depende de lado
    let posX = startSide === 'left' ? -50 : screenWidth + 50;

    objetoEl.style.top = posY + 'px';
    objetoEl.style.position = 'absolute';

    objetosContainer.appendChild(objetoEl);

    // Velocidad horizontal (izq a derecha o viceversa)
    const velocidadX = (Math.random() * 1.2 + 0.8) * (startSide === 'left' ? 1 : -1);
    // Velocidad vertical suave para simular vuelo (sube y baja lentamente)
    const velocidadY = (Math.random() * 0.5 + 0.1) * (Math.random() < 0.5 ? 1 : -1);

    // Guardamos estado para animar
    objetosEnPantalla.push({
        el: objetoEl,
        x: posX,
        y: posY,
        velX: velocidadX,
        velY: velocidadY,
        reciclable: objData.reciclable,
        startSide: startSide
    });
}

// Animar objetos "volando"
function animarObjetosVolando() {
    if (!gameActive) return;

    objetosEnPantalla.forEach((obj, i) => {
        obj.x += obj.velX;
        obj.y += obj.velY;

        // Oscilación vertical (si se pasa cierto límite vertical, cambia dirección Y)
        if (obj.y < 50) obj.velY = Math.abs(obj.velY);
        if (obj.y > screenHeight * 0.7) obj.velY = -Math.abs(obj.velY);

        obj.el.style.left = obj.x + 'px';
        obj.el.style.top = obj.y + 'px';

        // Detectar si está llegando al "tacho" del jugador (colisión)
        const playerRect = playerContainer.getBoundingClientRect();
        const objRect = obj.el.getBoundingClientRect();

        if (
            objRect.right > playerRect.left &&
            objRect.left < playerRect.right &&
            objRect.bottom > playerRect.top &&
            objRect.top < playerRect.bottom
        ) {
            if (obj.reciclable) {
                // Animación de "tragado"
                obj.el.style.transition = 'transform 0.5s ease, opacity 0.5s ease';
                obj.el.style.transform = 'scale(0) translateY(40px)';
                obj.el.style.opacity = '0';

                setTimeout(() => {
                    if (obj.el.parentNode) {
                        obj.el.parentNode.removeChild(obj.el);
                    }
                }, 500);

                objetosEnPantalla.splice(i, 1);
                // Acá podrías sumar puntaje o feedback
            } else {
                // No reciclables pasan sin efecto (o podrías agregar mensaje o penalización)
            }
        }

        // Si salió de pantalla horizontalmente, eliminar y sacar del array
        if ((obj.startSide === 'left' && obj.x > screenWidth + 60) ||
            (obj.startSide === 'right' && obj.x < -60)) {
            if (obj.el.parentNode) {
                obj.el.parentNode.removeChild(obj.el);
            }
            objetosEnPantalla.splice(i, 1);
        }
    });

    requestAnimationFrame(animarObjetosVolando);
}
function startGame() {
    gameActive = true;

    // Cada 0.6 segundos cae nuevo objeto volante
    const spawnInterval = setInterval(() => {
        crearObjetoVolante();
    }, 600);

    animarObjetosVolando();

    setTimeout(() => {
        gameActive = false;
        clearInterval(spawnInterval);
        alert('¡Tiempo terminado!');
    }, gameDuration);
}