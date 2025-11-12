namespace EcoPlay.Models;

public class Aspecto
{
    public int IDAspecto { get; set; }
    public string Nombre { get; set; }
    public string Imagen { get; set; }
    public bool Desbloqueado { get; set; }

    public Aspecto() { }
    public Aspecto (string Nombre, string Imagen, bool Desbloqueado)
    {
        this.Nombre = Nombre;
        this.Imagen = Imagen;
        this.Desbloqueado = Desbloqueado;
    }
}
