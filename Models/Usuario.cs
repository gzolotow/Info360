namespace EcoPlay.Models;
public class Usuario
{
    public int IDUsuario { get; set; }
    public string Username { get; set; }
    public string Mail { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public string Contraseña { get; set; }
    public string Foto { get; set; }
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