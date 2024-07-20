using Entities.Entities;

namespace BackEnd.Model
{
    public class MazoModel
    {
        public int MazoId { get; set; }

        public int? UsuarioId { get; set; }

        public string NombreMazo { get; set; } = null!;

        public DateTime? CreadoEn { get; set; }

        public virtual Usuario? Usuario { get; set; }
    }
}
