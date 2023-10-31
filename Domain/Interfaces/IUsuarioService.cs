using Application.DTO;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUsuarioService
    {
        ServiceResultEntity ChangePassword(ChangePasswordDto request);
    }
}
