using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace PokeCore.API.Core.Entities;

public class Comparacion : BaseModel
{
    [PrimaryKey("id", false)]
    public Guid Id { get; set; }

    [Column("usuario_id")]
    public Guid UsuarioId { get; set; }

    [Column("pokemon_a")]
    public string PokemonA { get; set; } = "";

    [Column("pokemon_b")]
    public string PokemonB { get; set; } = "";

    [Column("indice_a")]
    public float IndiceA { get; set; }

    [Column("indice_b")]
    public float IndiceB { get; set; }

    [Column("ganador")]
    public string Ganador { get; set; } = "";

    [Column("detalle")]
    public string Detalle { get; set; } = "";

    [Column("fecha")]
    public DateTime Fecha { get; set; }
}