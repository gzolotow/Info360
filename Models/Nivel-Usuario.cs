namespace Ecoplay.Models
{
    public class NivelUsuario
    {
        public int IDNivelUsuario { get; set; }
        public int IDNivel { get; set; }
        public int AspectoEquipado { get; set; }
        public int Estrellas { get; set; }

        public NivelUsuario() { }

        public NivelUsuario(int IDNivel, int AspectoEquipado, int Estrellas)
        {
            this.IDNivel = IDNivel;
            this.AspectoEquipado = AspectoEquipado;
            this.Estrellas = Estrellas;
        }
    }
}
