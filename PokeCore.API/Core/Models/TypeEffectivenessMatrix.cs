namespace PokeCore.API.Core.Models;

public class TypeEffectivenessMatrix
{
    public List<string> DoubleDamageTo { get; set; } = new();
    public List<string> HalfDamageTo { get; set; } = new();
    public List<string> NoDamageTo { get; set; } = new();
}