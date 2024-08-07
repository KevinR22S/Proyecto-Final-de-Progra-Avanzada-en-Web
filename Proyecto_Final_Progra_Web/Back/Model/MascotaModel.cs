namespace Back.Model
{
    public class MascotaModel
    {
         public int IdCita { get; set; }

    public string? UsuarioCreacionId { get; set; }

    public string? UsuarioModificacionId { get; set; }

    public int IdMascota { get; set; }

    public int IdMedicamento { get; set; }

    public DateTime Fecha { get; set; }

    public TimeOnly Hora { get; set; }

    public string? Descripcion { get; set; }

    public string? Estado { get; set; }

    public string? Diagnostico { get; set; }

    public string? Veterinario { get; set; }

    public string? VeterinarioSec { get; set; }
    }
}
