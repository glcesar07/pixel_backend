using Application.DTO;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICityService
    {
        ServiceResultEntity Label(CountryDto request);
    }
}
