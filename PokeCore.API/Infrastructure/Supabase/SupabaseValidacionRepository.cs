using PokeCore.API.Core.Entities;
using Supabase;
using Client = Supabase.Client;

namespace PokeCore.API.Infrastructure.Supabase;

public class SupabaseValidacionRepository
{
    private readonly Client _client;

    public SupabaseValidacionRepository(IConfiguration config)
    {
        _client = new Client(config["Supabase:Url"], config["Supabase:Key"], new SupabaseOptions
        {
            AutoConnectRealtime = false
        });

        _client.InitializeAsync().Wait();
    }
    
    public async Task<Guid> GuardarValidacionAsync(Validacion validacion)
    {
        var result = await _client.From<Validacion>().Insert(validacion);
        return result.Models.First().Id;
    }

    public async Task GuardarDetalleAsync(IEnumerable<DetalleEquipo> detalles)
    {
        foreach (var d in detalles)
        {
            await _client.From<DetalleEquipo>().Insert(d);
        }
    }
}