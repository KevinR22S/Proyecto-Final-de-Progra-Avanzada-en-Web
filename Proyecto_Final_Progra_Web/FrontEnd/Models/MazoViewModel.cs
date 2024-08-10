using Entities.Entities;

namespace FrontEnd.Models
{
    public class MazoViewModel
    {
        public int MazoId { get; set; }

        public String UsuarioId { get; set; }

        public string NombreMazo { get; set; } = null!;

        public DateTime CreadoEn { get; set; }

        public string Estado { get; set; }

        public ApplicationUser? UsuarioModificacion { get; set; }

    }
}
