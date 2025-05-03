using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace PokeCore.API.Core.Entities;

[Table("detalle_equipo")]
public class DetalleEquipo : BaseModel
{
    [PrimaryKey("id", false)]
    public Guid Id { get; set; }

    [Column("validacion_id")]
    public Guid ValidacionId { get; set; }

    [Column("nombre_pokemon")]
    public string NombrePokemon { get; set; } = "";

    [Column("tipos")]
    public string[] Tipos { get; set; } = [];

    [Column("rol")]
    public string Rol { get; set; } = "";
}