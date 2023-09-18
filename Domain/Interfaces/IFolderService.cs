using Application.DTO;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IFolderService
    {
        ServiceResultEntity Create(FolderDto request);
        ServiceResultEntity Update(FolderDto request);
        ServiceResultEntity Action(FolderDto request);
        ServiceResultEntity All(FolderDto request);
        ServiceResultEntity One(FolderDto request);
        ServiceResultEntity Subfolder(FolderDto request);
    }
}
