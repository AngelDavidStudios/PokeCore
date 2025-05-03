using Microsoft.AspNetCore.Mvc;
using PokeCore.API.Core.DTOs;
using PokeCore.API.Core.Services;

namespace PokeCore.API.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComparadorController : ControllerBase
{
    private readonly ComparadorPokemonService _comparador;

    public ComparadorController(ComparadorPokemonService comparador)
    {
        _comparador = comparador;
    }
    
    [HttpPost]
    [Route("comparar")]
    public async Task<ActionResult<ComparacionResponse>> Comparar([FromBody] ComparacionRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.PokemonA) || string.IsNullOrWhiteSpace(request.PokemonB))
            return BadRequest("Debe proporcionar dos Pokémon.");

        try
        {
            var result = await _comparador.CompararAsync(request.PokemonA, request.PokemonB);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al comparar: {ex.Message}");
        }
    }
}