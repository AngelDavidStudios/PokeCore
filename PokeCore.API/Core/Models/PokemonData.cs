namespace PokeCore.API.Core.Models;

public class PokemonData
{
    public string Name { get; set; } = "";
    public int Attack { get; set; }
    public int Defense { get; set; }
    public List<string> Types { get; set; } = new();
}