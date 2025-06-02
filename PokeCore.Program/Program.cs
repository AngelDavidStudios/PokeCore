using System.Text.Json;
using ConsoleLogin.Models;
using ConsoleLogin.Service;
            
namespace ConsoleLogin
{
    class Program
    {
        static async Task Main()
        {
            Console.WriteLine("=== Welcome to the Pokemon Console App ===\n");
            Console.Write("Enter email: ");
            var email = Console.ReadLine();

            Console.Write("Enter password: ");
            var password = ReadPassword();
            Console.WriteLine("\nLogging in...");

            var authService = new AuthService();
            var token = await authService.LoginAsync(email, password);

            if (string.IsNullOrWhiteSpace(token))
            {
                Console.WriteLine("Login failed.");
                return;
            }
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Main Menu ===\n");
                Console.WriteLine("1 - Compare Pokemon");
                Console.WriteLine("2 - Validate Pokemon Team");
                Console.WriteLine("3 - Exit");
    
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        var comparador = new ComparadorService();
                        Console.WriteLine("Enter the name of the first Pokemon:");
                        var poke1 = Console.ReadLine();
                        Console.WriteLine("Enter the name of the second Pokemon:");
                        var poke2 = Console.ReadLine();

                        var result = await comparador.CompararAsync(token, poke1, poke2);
                        
                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };
                        var comparacion = JsonSerializer.Deserialize<ComparacionResponse>(result, options);

                        Console.WriteLine("\nComparison result (table):");
                        Console.WriteLine("+-----------------------------------------------+");
                        Console.WriteLine("| Ganador: " + comparacion.Ganador.PadRight(30) + "        |");
                        Console.WriteLine("| IndiceA: " + comparacion.IndiceA.ToString().PadRight(30) + "        |");
                        Console.WriteLine("| IndiceB: " + comparacion.IndiceB.ToString().PadRight(30) + "        |");
                        Console.WriteLine("| Detalle: " + comparacion.Detalle.PadRight(30) + "        |");
                        Console.WriteLine("+-----------------------------------------------+");
                    break;
                    case "2":
                        Console.Write("How many Pokémon to validate? ");
                        if (!int.TryParse(Console.ReadLine(), out var count) || count < 1) return;

                        var pokemons = new List<string>();
                        for (int i = 0; i < count; i++)
                        {
                            Console.Write($"Enter Pokémon {i + 1}: ");
                            pokemons.Add(Console.ReadLine());
                        }

                        var teamService = new TeamService();
                        var raw = await teamService.ValidarEquipoAsync(token, pokemons);

                        var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        var data = JsonSerializer.Deserialize<ValidacionEquipoResponse>(raw, opts);

                        Console.WriteLine("\nValidation result:");
                        Console.WriteLine($"Score: {data.PuntajeBalance}");
                        Console.WriteLine($"Types: {string.Join(", ", data.TiposUnicos)}");
                        Console.WriteLine($"Roles: {string.Join(", ", data.RolesPorPokemon.Select(r => r.Key + "-" + r.Value))}");
                        Console.WriteLine($"Coverage: {string.Join(", ", data.CoberturaDebilidades)}");
                        Console.WriteLine($"Notes: {data.Observaciones}");
                    break;
                    case "3":
                        Console.WriteLine("Exiting...");
                        return;
                }
            }
        }

        private static string ReadPassword()
        {
            var pass = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    pass = pass.Remove(pass.Length - 1);
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    pass += keyInfo.KeyChar;
                    Console.Write("*");
                }
            } while (key != ConsoleKey.Enter);
            return pass;
        }
    }
}