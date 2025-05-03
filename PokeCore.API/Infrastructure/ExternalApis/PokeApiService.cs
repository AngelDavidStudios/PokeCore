using Newtonsoft.Json.Linq;
using PokeCore.API.Core.Interfaces;
using PokeCore.API.Core.Models;
using RestSharp;

namespace PokeCore.API.Infrastructure.ExternalApis;

public class PokeApiService : IPokeApiService
{
    private readonly RestClient _client;

    public PokeApiService()
    {
        _client = new RestClient("https://pokeapi.co/api/v2/");
    }
    
    public async Task<PokemonData> GetPokemonAsync(string name)
    {
        var request = new RestRequest($"pokemon/{name.ToLower()}", Method.Get);
        var response = await _client.ExecuteAsync(request);
        var json = JObject.Parse(response.Content!);

        var types = json["types"]!.Select(t => t["type"]!["name"]!.ToString()).ToList();
        var stats = json["stats"]!.ToDictionary(s => s["stat"]!["name"]!.ToString(), s => (int)s["base_stat"]!);

        return new PokemonData
        {
            Name = json["name"]!.ToString(),
            Attack = stats["attack"],
            Defense = stats["defense"],
            Types = types
        };
    }
    
    public async Task<TypeEffectivenessMatrix> GetTypeEffectivenessAsync(string type)
    {
        var request = new RestRequest($"type/{type.ToLower()}", Method.Get);
        var response = await _client.ExecuteAsync(request);
        var json = JObject.Parse(response.Content!);

        var damageRelations = json["damage_relations"]!;

        return new TypeEffectivenessMatrix
        {
            DoubleDamageTo = damageRelations["double_damage_to"]!.Select(t => t["name"]!.ToString()).ToList(),
            HalfDamageTo = damageRelations["half_damage_to"]!.Select(t => t["name"]!.ToString()).ToList(),
            NoDamageTo = damageRelations["no_damage_to"]!.Select(t => t["name"]!.ToString()).ToList()
        };
    }
}