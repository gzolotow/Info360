namespace Ecoplay.Models;
using Newtonsoft.Json;


    public class NivelUsuario
    {
        [JsonProperty]
        public int IDNivelUsuario { get; set; }
        [JsonProperty]
        public int IDNivel { get; set; }
        [JsonProperty]
        public int AspectoEquipado { get; set; }
        [JsonProperty]
        public int Estrellas { get; set; }
        [JsonProperty]
        public int MisionesCompletadas { get; set; }


        public NivelUsuario() { }

        public NivelUsuario(int IDNivel, int AspectoEquipado, int Estrellas, int MisionesCompletadas)
        {
            this.IDNivel = IDNivel;
            this.AspectoEquipado = AspectoEquipado;
            this.Estrellas = Estrellas;
            this.MisionesCompletadas = MisionesCompletadas;
        }
    }

