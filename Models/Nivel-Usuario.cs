namespace Ecoplay.Models
{
    public class NivelUsuario
    {
        public int IDNivelUsuario { get; set; }
        public int IDNivel { get; set; }
        public int AspectoEquipado { get; set; }
        public int Monedas { get; set; }
        public int Estrellas { get; set; }

        public NivelUsuario() { }

        public NivelUsuario(int IDNivel, int AspectoEquipado, int Monedas, int Estrellas)
        {
            this.IDNivel = IDNivel;
            this.AspectoEquipado = AspectoEquipado;
            this.Monedas = Monedas;
            this.Estrellas = Estrellas;
        }
    }
}
