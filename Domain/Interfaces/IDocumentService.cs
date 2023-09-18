using Application.DTO;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IDocumentService
    {
        ServiceResultEntity Create(DocumentDto request);
        ServiceResultEntity Update(DocumentDto request);
        ServiceResultEntity Action(DocumentDto request);
        ServiceResultEntity All(DocumentDto request);
        ServiceResultEntity One(DocumentDto request);
        //ServiceResultEntity Subfolder(DocumentDto request);
    }
}
