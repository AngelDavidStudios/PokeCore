using PokeCore.API.Core.DTOs;

namespace PokeCore.API.Core.Interfaces;

public interface IValidacionService
{
    Task<ValidacionEquipoResponse> ValidarEquipoAsync(ValidacionEquipoRequest request, Guid usuarioId);
}