namespace EcoPlay.Models;

public class Aspecto
{
    public int IDAspecto { get; set; }
    public string Nombre { get; set; }
    public int Precio { get; set; }
    public bool desbloqueado { get; set; }
    public string Image { get; set; }

    public Aspecto() { }
    public Aspecto (string Nombre, bool desbloqueado, int Precio, string Image)
    {
        this.Nombre = Nombre;
        this.desbloqueado = desbloqueado;
        this.Precio = Precio;
        this.Image = Image;
    }
}
