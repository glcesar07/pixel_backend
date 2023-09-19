using Application.DTO;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IOcrService
    {
        ServiceResultEntity OCR(OcrDto request);
    }
}
