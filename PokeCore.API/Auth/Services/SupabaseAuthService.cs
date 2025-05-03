using PokeCore.API.Auth.Interfaces;
using Supabase;

namespace PokeCore.API.Auth.Services;

public class SupabaseAuthService : IAuthService
{
    private Supabase.Client _client;

    public SupabaseAuthService(IConfiguration config)
    {
        var url = config["Supabase:Url"];
        var key = config["Supabase:Key"];
        
        _client = new Supabase.Client(url, key, new SupabaseOptions
        {
            AutoConnectRealtime = true
        });
        _client.InitializeAsync().Wait();
    }

    public async Task<string> RegisterAsync(string email, string password)
    {
        var session = await _client.Auth.SignUp(email, password);
        return session != null ? "Registration successful" : "Registration failed";
    }
    
    public async Task<string> LoginAsync(string email, string password)
    {
        var session = await _client.Auth.SignIn(email, password);
        return session != null ? session.AccessToken : throw new Exception("Login fallido");
    }

    public async Task<bool> ValidateTokenAsync(string token)
    {
        var user = await _client.Auth.GetUser(token);
        return user != null;
    }
}