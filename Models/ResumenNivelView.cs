using System;
using System.ComponentModel.DataAnnotations;
namespace EcoPlay.Models
{
    public class ResumenNivelView
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UsuarioMail { get; set; }  // FK al usuario (usando mail como ID único)
        [Required]
        public int NivelId { get; set; }  // Ej: 1 para Nivel1
        public int Estrellas { get; set; }  // 0-3
        public int Errores { get; set; }  // Cantidad de errores cometidos
        public TimeSpan Tiempo { get; set; }  // Tiempo total (ej: 00:01:45)
        public DateTime FechaCompletado { get; set; } = DateTime.Now;  // Fecha de finalización
        // Opcional: agregar campos como "MejorPuntuacion" si querés comparar
    }
}