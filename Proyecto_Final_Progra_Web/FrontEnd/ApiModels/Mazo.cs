namespace FrontEnd.ApiModels
{
    public class Mazo
    {
        public int MazoId { get; set; }

        public string? UsuarioId { get; set; }

        public string NombreMazo { get; set; } = null!;

        public DateTime CreadoEn { get; set; }

    }
}
