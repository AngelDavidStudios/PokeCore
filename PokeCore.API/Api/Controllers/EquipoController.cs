using Microsoft.AspNetCore.Mvc;
using PokeCore.API.Core.DTOs;
using PokeCore.API.Core.Interfaces;

namespace PokeCore.API.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipoController : ControllerBase
{
    private readonly IValidacionService _validacionService;

    public EquipoController(IValidacionService validacionService)
    {
        _validacionService = validacionService;
    }
    
    [HttpPost("validar")]
    public async Task<ActionResult<ValidacionEquipoResponse>> Validar([FromBody] ValidacionEquipoRequest request)
    {
        if (request.Nombres == null || request.Nombres.Count == 0 || request.Nombres.Count > 6)
            return BadRequest("Debes enviar entre 1 y 6 Pokémon.");

        // ⚠️ Temporalmente el ID del usuario está quemado
        var usuarioId = new Guid("00000000-0000-0000-0000-000000000001");

        try
        {
            var resultado = await _validacionService.ValidarEquipoAsync(request, usuarioId);
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al validar equipo: {ex.Message}");
        }
    }
}