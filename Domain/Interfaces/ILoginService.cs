using Application.DTO;
using Domain.Entities;

namespace Domain.Interfaces
{
    public  interface ILoginService
    {
        ServiceResultEntity LoginUser(LoginDto login);
    }
}
