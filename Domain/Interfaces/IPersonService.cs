using Application.DTO;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPersonService
    {
        ServiceResultEntity Create(PersonDto request);
        ServiceResultEntity Update(PersonDto request);
        ServiceResultEntity Action(PersonDto request);
        ServiceResultEntity All(PersonDto request);
        ServiceResultEntity One(PersonDto request);
        ServiceResultEntity Label(PersonDto request);
    }
}
