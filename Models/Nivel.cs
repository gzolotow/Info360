namespace EcoPlay.Models;
using Newtonsoft.Json;

public class Nivel
{
    [JsonProperty]
    public int IDNivel { get; set; }
    [JsonProperty]
    public int Puntos { get; set; }
    [JsonProperty]
    public int Errores { get; set; }
    [JsonProperty]
    public int Tiempo { get; set; }
    [JsonProperty]
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
