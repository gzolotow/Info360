namespace EcoPlay.Models;

public class Transaccion
{
    public int IDTransaccion { get; set; }
    public int IDNivelUsuario { get; set; }
    public int IDAspecto { get; set; }

    public Transaccion() { }
    public Transaccion (int IDNivelUsuario, int IDAspecto)
    {
        this.IDNivelUsuario = IDNivelUsuario;
        this.IDAspecto = IDAspecto;
    }
}