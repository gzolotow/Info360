window.addEventListener('DOMContentLoaded', () => {
    const objetos = document.querySelectorAll('.objeto.draggable');
    const tachos = document.querySelectorAll('.tacho');

    objetos.forEach(obj => {
        obj.setAttribute('draggable', true);

        obj.addEventListener('dragstart', (ev) => {
            ev.dataTransfer.setData('text/plain', ev.target.alt);
            ev.dataTransfer.setData('object-tacho', ev.target.dataset.tacho);
            ev.target.classList.add('dragging');
        });

        obj.addEventListener('dragend', (ev) => {
            ev.target.classList.remove('dragging');
        });
    });

    tachos.forEach(tacho => {
        tacho.addEventListener('dragover', (ev) => {
            ev.preventDefault();
            tacho.classList.add('drag-over');
        });

        tacho.addEventListener('dragleave', (ev) => {
            tacho.classList.remove('drag-over');
        });

        tacho.addEventListener('drop', (ev) => {
            ev.preventDefault();
            tacho.classList.remove('drag-over');

            const droppedTacho = ev.currentTarget.dataset.tacho;
            const draggedTacho = ev.dataTransfer.getData('object-tacho');
            const draggedObjectAlt = ev.dataTransfer.getData('text/plain');

            if (droppedTacho === draggedTacho) {
                const obj = [...document.querySelectorAll('.objeto')].find(x => x.alt === draggedObjectAlt);
                if (obj) {
                    obj.style.transition = 'transform 0.5s ease, opacity 0.5s ease';
                    obj.style.transform = 'scale(0) translateY(50px)';
                    obj.style.opacity = '0';
                    setTimeout(() => obj.remove(), 500);
                }
            } else {
                alert('¡Coloca el residuo en el tacho correcto!');
            }
        });
    });
});