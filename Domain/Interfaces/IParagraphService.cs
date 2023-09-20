using Application.DTO;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IParagraphService
    {
        ServiceResultEntity Create(ParagraphDtoReq request);
        ServiceResultEntity Update(ParagraphDtoReq request);
        ServiceResultEntity Action(ParagraphDtoReq request);
        ServiceResultEntity All(ParagraphDtoReq request);
        ServiceResultEntity One(ParagraphDtoReq request);
    }
}
