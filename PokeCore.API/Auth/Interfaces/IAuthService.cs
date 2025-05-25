namespace PokeCore.API.Auth.Interfaces;

public interface IAuthService
{
    Task<string> RegisterAsync(string email, string password);
    Task<string> LoginAsync(string email, string password);
    Task<bool> ValidateTokenAsync(string token);
    Task<bool> LogoutAsync(string accessToken);
}