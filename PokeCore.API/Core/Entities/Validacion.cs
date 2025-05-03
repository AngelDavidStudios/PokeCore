using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace PokeCore.API.Core.Entities;

[Table("validaciones")]
public class Validacion : BaseModel
{
    [PrimaryKey("id", false)]
    public Guid Id { get; set; }

    [Column("usuario_id")]
    public Guid UsuarioId { get; set; }

    [Column("puntaje_balance")]
    public int PuntajeBalance { get; set; }

    [Column("observaciones")]
    public string Observaciones { get; set; } = "";

    [Column("fecha")]
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
}