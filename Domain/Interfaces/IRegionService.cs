using Application.DTO;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IRegionService
    {
        ServiceResultEntity Label(CountryDto request);
    }
}
