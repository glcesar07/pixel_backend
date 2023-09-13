using Application.DTO;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICatalogueService
    {
        ServiceResultEntity Create(CatalogueDto login);
        ServiceResultEntity Update(CatalogueDto login);
        ServiceResultEntity Action(CatalogueDto login);
        ServiceResultEntity All(CatalogueDto login);
        ServiceResultEntity One(CatalogueDto login);
        ServiceResultEntity Label(CatalogueDto login);
    }
}
