namespace PokeCore.API.Core.DTOs;

public class ValidacionEquipoRequest
{
    public List<string> Nombres { get; set; } = new(); // hasta 6 nombres de Pokémon

}