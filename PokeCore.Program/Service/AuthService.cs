using System.Text;
using System.Text.Json;

namespace ConsoleLogin.Service;

public class AuthService
{
    public async Task<string> LoginAsync(string email, string password)
    {
        using var client = new HttpClient();
        var loginData = new { Email = email, Password = password };
        var jsonData = JsonSerializer.Serialize(loginData);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var response = await client.PostAsync("http://localhost:5213/api/Auth/login", content);
        var body = await response.Content.ReadAsStringAsync();

        return JsonDocument.Parse(body).RootElement.GetProperty("token").GetString();
    }
}