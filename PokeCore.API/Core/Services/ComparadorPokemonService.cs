using PokeCore.API.Core.DTOs;
using PokeCore.API.Core.Entities;
using PokeCore.API.Core.Interfaces;
using PokeCore.API.Infrastructure.Supabase;

namespace PokeCore.API.Core.Services;

public class ComparadorPokemonService
{
    private readonly IPokeApiService _pokeApi;
    private readonly SupabaseComparacionRepository _repo;

    public ComparadorPokemonService(IPokeApiService pokeApi, SupabaseComparacionRepository repo)
    {
        _pokeApi = pokeApi;
        _repo = repo;
    }

    public async Task<ComparacionResponse> CompararAsync(string nombreA, string nombreB, Guid usuarioId)
    {
        var pokeA = await _pokeApi.GetPokemonAsync(nombreA);
        var pokeB = await _pokeApi.GetPokemonAsync(nombreB);

        var tipoA = pokeA.Types.First();
        var tipoB = pokeB.Types.First();

        var efectividadA = await _pokeApi.GetTypeEffectivenessAsync(tipoA);
        var efectividadB = await _pokeApi.GetTypeEffectivenessAsync(tipoB);

        float modA = efectividadA.DoubleDamageTo.Contains(tipoB) ? 2f :
            efectividadA.HalfDamageTo.Contains(tipoB) ? 0.5f :
            efectividadA.NoDamageTo.Contains(tipoB) ? 0f : 1f;

        float modB = efectividadB.DoubleDamageTo.Contains(tipoA) ? 2f :
            efectividadB.HalfDamageTo.Contains(tipoA) ? 0.5f :
            efectividadB.NoDamageTo.Contains(tipoA) ? 0f : 1f;

        var indiceA = (float)pokeA.Attack / pokeB.Defense * modA;
        var indiceB = (float)pokeB.Attack / pokeA.Defense * modB;
        var ganador = indiceA > indiceB ? pokeA.Name : pokeB.Name;

        var detalle = $"{pokeA.Name} vs {pokeB.Name} → gana {ganador}";

        await _repo.GuardarComparacionAsync(new Comparacion()
        {
            UsuarioId = usuarioId,
            PokemonA = pokeA.Name,
            PokemonB = pokeB.Name,
            IndiceA = (float)Math.Round(indiceA, 2),
            IndiceB = (float)Math.Round(indiceB, 2),
            Ganador = ganador,
            Detalle = detalle,
            Fecha = DateTime.UtcNow
        });

        return new ComparacionResponse
        {
            Ganador = ganador,
            IndiceA = (float)Math.Round(indiceA, 2),
            IndiceB = (float)Math.Round(indiceB, 2),
            Detalle = detalle
        };
    }
}