using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ConsoleLogin.Service;

public class TeamService
{
    public async Task<string> ValidarEquipoAsync(string token, List<string> nombres)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var payload = new { Nombres = nombres };
        var jsonData = JsonSerializer.Serialize(payload);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("http://localhost:5213/api/Equipo/validar", content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"Team validation failed: {error}");
        }

        return await response.Content.ReadAsStringAsync();
    }
}