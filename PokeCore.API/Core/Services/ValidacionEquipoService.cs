using PokeCore.API.Core.DTOs;
using PokeCore.API.Core.Entities;
using PokeCore.API.Core.Interfaces;
using PokeCore.API.Infrastructure.Supabase;

namespace PokeCore.API.Core.Services;

public class ValidacionEquipoService : IValidacionService
{
    private readonly IPokeApiService _pokeApi;
    private readonly SupabaseValidacionRepository _repo;

    public ValidacionEquipoService(IPokeApiService pokeApi, SupabaseValidacionRepository repo)
    {
        _pokeApi = pokeApi;
        _repo = repo;
    }
    
    public async Task<ValidacionEquipoResponse> ValidarEquipoAsync(ValidacionEquipoRequest request, Guid usuarioId)
    {
        var tipos = new HashSet<string>();
        var debilidades = new HashSet<string>();
        var roles = new Dictionary<string, string>();
        int ofensivos = 0, defensivos = 0, veloces = 0;

        foreach (var nombre in request.Nombres.Take(6))
        {
            var p = await _pokeApi.GetPokemonAsync(nombre);

            // Acumular tipos únicos
            foreach (var tipo in p.Types)
                tipos.Add(tipo);

            // Rol por stats
            string rol = "Equilibrado";
            if (p.Attack > 90) { rol = "Ofensivo"; ofensivos++; }
            if (p.Defense > 90) { rol = "Defensivo"; defensivos++; }
            if (p.Types.Contains("ghost") || p.Types.Contains("psychic") || p.Attack < 80 && p.Defense < 80)
            {
                if (p.Attack < 80 && p.Defense < 80) rol = "Veloz";
                veloces++;
            }

            roles[p.Name] = rol;

            // Cobertura: tipos a los que hace daño doble
            foreach (var tipo in p.Types)
            {
                var matrix = await _pokeApi.GetTypeEffectivenessAsync(tipo);
                foreach (var t in matrix.DoubleDamageTo)
                    debilidades.Add(t);
            }
        }

        // Puntaje base
        int puntaje = tipos.Count * 10;
        puntaje += Math.Min(20, debilidades.Count * 2);
        puntaje += Math.Min(20, (ofensivos + defensivos + veloces) * 3);

        string obs = $"Diversidad: {tipos.Count} tipos. Cobertura: {debilidades.Count} tipos.";
        if (puntaje >= 80) obs += " Equipo bien balanceado.";
        else if (puntaje >= 60) obs += " Balance aceptable.";
        else obs += " El equipo tiene cobertura o roles limitados.";
        
        // Crear entidad de validación
        var validacion = new Validacion()
        {
            UsuarioId = usuarioId,
            PuntajeBalance = puntaje,
            Observaciones = obs
        };

        var validacionId = await _repo.GuardarValidacionAsync(validacion);

        // Crear detalles
        var detalles = roles.Select(r => new DetalleEquipo
        {
            ValidacionId = validacionId,
            NombrePokemon = r.Key,
            Tipos = request.Nombres.Contains(r.Key) ? request.Nombres.ToArray() : [],
            Rol = r.Value
        });

        await _repo.GuardarDetalleAsync(detalles);


        return new ValidacionEquipoResponse
        {
            PuntajeBalance = puntaje,
            TiposUnicos = tipos.ToList(),
            RolesPorPokemon = roles,
            CoberturaDebilidades = debilidades.ToList(),
            Observaciones = obs
        };
    }
}