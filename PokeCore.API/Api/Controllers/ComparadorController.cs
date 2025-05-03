using Microsoft.AspNetCore.Mvc;
using PokeCore.API.Core.DTOs;
using PokeCore.API.Core.Services;
using PokeCore.API.Infrastructure.Extensions;

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
    public async Task<ActionResult<ComparacionResponse>> Comparar([FromBody] ComparacionRequest req)
    {
        var usuarioId = HttpContext.User.ObtenerUsuarioId();
        if (usuarioId is null)
            return Unauthorized("No se pudo extraer el usuario del token.");
        
        if (string.IsNullOrWhiteSpace(req.PokemonA) || string.IsNullOrWhiteSpace(req.PokemonB))
            return BadRequest("Debe proporcionar dos Pokémon.");

        try
        {
            var result = await _comparador.CompararAsync(req.PokemonA, req.PokemonB, usuarioId.Value);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al comparar: {ex.Message}");
        }
    }
}