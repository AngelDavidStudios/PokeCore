using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ConsoleLogin.Service;

public class ComparadorService
{
    public async Task<string> CompararAsync(string token, string poke1, string poke2)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var compareData = new { PokemonA = poke1, PokemonB = poke2 };
        var compareJson = JsonSerializer.Serialize(compareData);
        var content = new StringContent(compareJson, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("http://localhost:5213/api/Comparador/comparar", content);
        return await response.Content.ReadAsStringAsync();
    }
}