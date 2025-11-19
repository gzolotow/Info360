namespace EcoPlay.Models;
using Newtonsoft.Json;

public class Usuario
{
    [JsonProperty]
    public int IDUsuario { get; set; }
    [JsonProperty]
    public string Username { get; set; }
    [JsonProperty]
    public string Mail { get; set; }
    [JsonProperty]
    public DateTime FechaNacimiento { get; set; }
    [JsonProperty]
    public string Contraseña { get; set; }
    [JsonProperty]
    public string Foto { get; set; }
    [JsonProperty]
    public int IDNivelUsuario { get; set; }

    public Usuario (){}
    public Usuario (string Username, string Mail, DateTime FechaNacimiento, string Contraseña, string Foto, int IDNivelUsuario)
    {
        this.Username = Username;
        this.Mail = Mail;
        this.FechaNacimiento = FechaNacimiento;
        this.Contraseña = Contraseña;
        this.Foto = Foto;
        this.IDNivelUsuario = IDNivelUsuario;
    }
}