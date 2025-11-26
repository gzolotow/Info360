
document.addEventListener('DOMContentLoaded', () => {
    const personaje = document.getElementById('personaje');
    const barraProgreso = document.getElementById('barra-progreso');
    const overlay = document.getElementById('overlayCompletado');
    let posX = window.innerWidth / 2;
    let progreso = 0;
    const objetivo = 100;

    // Movimiento del personaje
    document.addEventListener('mousemove', (e) => {
        posX = e.clientX - 40;
        personaje.style.left = `${posX}px`;
    });

    // Actualizar barra
    function actualizarBarra() {
        barraProgreso.style.width = `${progreso}%`;
        if (progreso >= objetivo) {
            mostrarOverlay();
        }
    }

    // Mostrar overlay
    function mostrarOverlay() {
        overlay.classList.remove('hidden');
    }

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

            if (posY > window.innerHeight) {
                img.remove();
                clearInterval(intervalo);
            }
        }, 50);
    }

    setInterval(crearResiduo, 2000);
});
