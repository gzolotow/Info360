namespace EcoPlay.Models;
public class Nivel
{
    public int IDNivel { get; set; }
    public int Puntos { get; set; }
    public int Errores { get; set; }
    public int Tiempo { get; set; }
    public bool Completado { get; set; }

    public Nivel() { }
    public Nivel (int Puntos, int Errores, int Tiempo, bool Completado)
    {
        this.Puntos = Puntos;
        this.Errores = Errores;
        this.Tiempo = Tiempo;
        this.Completado = Completado;
    }
}
