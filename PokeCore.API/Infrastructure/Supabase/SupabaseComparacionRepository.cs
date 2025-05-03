using PokeCore.API.Core.Entities;
using Supabase;
using Client = Supabase.Client;

namespace PokeCore.API.Infrastructure.Supabase;

public class SupabaseComparacionRepository
{
    private readonly Client _client;
    
    public SupabaseComparacionRepository(IConfiguration config)
    {
        _client = new Client(config["Supabase:Url"], config["Supabase:Key"], new SupabaseOptions()
        {
            AutoConnectRealtime = false
        });

        _client.InitializeAsync().Wait();
    }
    
    public async Task GuardarComparacionAsync(Comparacion comparacion)
    {
        await _client.From<Comparacion>().Insert(comparacion);
    }
}