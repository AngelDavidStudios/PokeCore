using PokeCore.API.Core.Models;

namespace PokeCore.API.Core.Interfaces;

public interface IPokeApiService
{
    Task<PokemonData> GetPokemonAsync(string name);
    Task<TypeEffectivenessMatrix> GetTypeEffectivenessAsync(string type);
}