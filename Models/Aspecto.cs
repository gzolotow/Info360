namespace EcoPlay.Models;
using Newtonsoft.Json;

public class Aspecto
{
    [JsonProperty]
    public int IDAspecto { get; set; }
    [JsonProperty]
    public string Nombre { get; set; }
    [JsonProperty]
    public string Imagen { get; set; }
    [JsonProperty]
    public bool Desbloqueado { get; set; }

    public Aspecto() { }
    public Aspecto (string Nombre, string Imagen, bool Desbloqueado)
    {
        this.Nombre = Nombre;
        this.Imagen = Imagen;
        this.Desbloqueado = Desbloqueado;
    }
}
