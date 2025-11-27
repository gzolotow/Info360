document.addEventListener("DOMContentLoaded", () => {
    const residuos = document.querySelectorAll(".residuo");
    const tachos = document.querySelectorAll(".tacho");
    const overlayCompletado = document.getElementById("overlayCompletado");

    let totalResiduos = residuos.length;
    let residuosCorrectos = 0;

    residuos.forEach(residuo => {
        residuo.addEventListener("dragstart", e => {
            e.dataTransfer.setData("tipo", residuo.dataset.tipo);
            e.dataTransfer.setData("id", residuo.id);
            residuo.classList.add("dragging");
        });

        residuo.addEventListener("dragend", () => {
            residuo.classList.remove("dragging");
        });
    });

    tachos.forEach(tacho => {
        tacho.addEventListener("dragover", e => {
            e.preventDefault();
            tacho.classList.add("highlight");
        });

        tacho.addEventListener("dragleave", () => {
            tacho.classList.remove("highlight");
        });

        tacho.addEventListener("drop", e => {
            tacho.classList.remove("highlight");
            const tipoResiduo = e.dataTransfer.getData("tipo");
            const residuoId = e.dataTransfer.getData("id");
            const residuo = document.getElementById(residuoId);
            const tipoTacho = tacho.classList.contains("verde") ? "verde" : "negro";

            if (tipoResiduo === tipoTacho) {
                residuo.classList.add("correcto");
                setTimeout(() => {
                    residuo.remove();
                    residuosCorrectos++;

                    
if (residuosCorrectos === totalResiduos) {
    overlayCompletado.classList.remove("hidden");

    // Avisar al servidor que el nivel se completó
    fetch('/Home/GuardarResultadoNivel', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ nivelId: 1, completado: true })
    });
}

                }, 500);
            } else {
                alert("Vuelve a intentar");
            }
        });
    });
});
