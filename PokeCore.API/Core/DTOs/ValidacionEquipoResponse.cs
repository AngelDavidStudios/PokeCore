namespace PokeCore.API.Core.DTOs;

public class ValidacionEquipoResponse
{
    public int PuntajeBalance { get; set; }
    public List<string> TiposUnicos { get; set; } = new();
    public Dictionary<string, string> RolesPorPokemon { get; set; } = new();
    public List<string> CoberturaDebilidades { get; set; } = new();
    public string Observaciones { get; set; } = "";
}