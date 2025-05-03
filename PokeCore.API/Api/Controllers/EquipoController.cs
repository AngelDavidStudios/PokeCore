using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokeCore.API.Core.DTOs;
using PokeCore.API.Core.Interfaces;
using PokeCore.API.Infrastructure.Extensions;

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
    [Authorize]
    public async Task<ActionResult<ValidacionEquipoResponse>> Validar([FromBody] ValidacionEquipoRequest request)
    {
        if (request.Nombres == null || request.Nombres.Count == 0 || request.Nombres.Count > 6)
            return BadRequest("Debes enviar entre 1 y 6 Pokémon.");
        
        var usuarioId = HttpContext.User.ObtenerUsuarioId();
        if (usuarioId is null)
            return Unauthorized("No se pudo extraer el usuario del token.");

        try
        {
            var resultado = await _validacionService.ValidarEquipoAsync(request, usuarioId.Value);
            return Ok(resultado);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al validar equipo: {ex.Message}");
        }
    }
}