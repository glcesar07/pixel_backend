using Application.DTO;
using Application.Functions;
using Application.Validator;
using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Database;
using Infraestructure.Helpers;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Infraestructure.Implementations
{
    public class FolderService : IFolderService
    {
        private readonly IConfiguration _configuration;
        private readonly FolderDtoValidator _validator;
        private readonly string _commandText;

        public FolderService(IConfiguration configuration, FolderDtoValidator loginDtoValidator)
        {
            _configuration = configuration;
            _validator = loginDtoValidator;
            _commandText = configuration["DataBase:StoredProcedures:Carpeta"];
        }

        public ServiceResultEntity Create(FolderDto request)
        {
            try
            {
                var validation = _validator.Validate(request);

                if (!validation.IsValid)
                {
                    return new ServiceResultEntity { Success = false, Message = "Errores de validacion.", Data = validation.Errors };
                }
                
                string root = Path.Combine(_configuration["Root"], _configuration["Path"]);
                string pathCreate;

                if (string.IsNullOrEmpty(request.ubicacion))
                {
                    pathCreate = Path.Combine(root, CleanText.AddGuion(request.nombreFisico));
                }
                else
                {
                    pathCreate = Path.Combine(root, request.ubicacion, CleanText.AddGuion(request.nombreFisico));
                }

                using (SqlConnection conn = MainConnection.Connection(_configuration))
                {
                    using (SqlCommand cmd = new(_commandText, conn))
                    {
                        SqlTransaction transaction = conn.BeginTransaction("carpetaTransaction");                        

                        cmd.Parameters.AddWithValue("@opcion", 1);
                        cmd.Parameters.AddWithValue("@nombre", request.nombre);
                        cmd.Parameters.AddWithValue("@persona", request.persona);
                        cmd.Parameters.AddWithValue("@padre", request.padre);
                        cmd.Parameters.AddWithValue("@ubicacion", request.ubicacion);
                        cmd.Parameters.AddWithValue("@nombreFisico", CleanText.AddGuion(request.nombreFisico));
                        cmd.Parameters.AddWithValue("@usuario", request.usuario ?? _configuration["UserCreate"]);

                        cmd.Transaction = transaction;

                        ServiceResultEntity validationResult = DataValidator.ValidateDatabaseData(cmd);

                        if (!validationResult.Success)
                        {
                            transaction.Rollback();
                            return validationResult;
                        }

                        transaction.Commit();

                        if (!Directory.Exists(pathCreate))
                        {
                            Directory.CreateDirectory(pathCreate);
                            return new ServiceResultEntity { Success = true, Message = "Carpeta creada exitosamente." };
                        }
                        else
                        {
                            return new ServiceResultEntity { Success = false, Message = "Lo sentimos la carpeta ya existe." };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new ServiceResultEntity { Success = false, Message = ex.ToString() };
            }
        }

        public ServiceResultEntity Update(FolderDto request)
        {
            try
            {
                var validation = _validator.Validate(request);

                if (!validation.IsValid)
                {
                    return new ServiceResultEntity { Success = false, Message = "Errores de validacion.", Data = validation.Errors };
                }

                using (SqlConnection conn = MainConnection.Connection(_configuration))
                {
                    using (SqlCommand cmd = new(_commandText, conn))
                    {
                        cmd.Parameters.AddWithValue("@opcion", 2);
                        cmd.Parameters.AddWithValue("@id", request.id);
                        cmd.Parameters.AddWithValue("@nombre", request.nombre);
                        cmd.Parameters.AddWithValue("@persona", request.persona);
                        cmd.Parameters.AddWithValue("@padre", request.padre);
                        cmd.Parameters.AddWithValue("@usuario", request.usuario ?? _configuration["UserCreate"]);

                        return DataValidator.ValidateDatabaseData(cmd);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ServiceResultEntity { Success = false, Message = ex.ToString() };
            }
        }

        public ServiceResultEntity Action(FolderDto request)
        {
            try
            {

                if (request.id is null || request.estado is null || request.usuario is null)
                {
                    return new ServiceResultEntity { Success = false, Message = "Errores de validacion.", Data = "id, estado y usuario no se estan enviando correctamente." };
                }

                using (SqlConnection conn = MainConnection.Connection(_configuration))
                {
                    using (SqlCommand cmd = new(_commandText, conn))
                    {
                        cmd.Parameters.AddWithValue("@opcion", 3);
                        cmd.Parameters.AddWithValue("@id", request.id);
                        cmd.Parameters.AddWithValue("@nombre", request.estado);
                        cmd.Parameters.AddWithValue("@usuario", request.usuario);

                        return DataValidator.ValidateDatabaseData(cmd);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ServiceResultEntity { Success = false, Message = ex.ToString() };
            }
        }

        public ServiceResultEntity All(FolderDto request)
        {
            try
            {
                using (SqlConnection conn = MainConnection.Connection(_configuration))
                {
                    using (SqlCommand cmd = new(_commandText, conn))
                    {
                        cmd.Parameters.AddWithValue("@opcion", 4);
                        cmd.Parameters.AddWithValue("@PageSize", request.pageSize);
                        cmd.Parameters.AddWithValue("@PageNumber", request.pageNumber);

                        return DataValidator.ValidateDatabaseData(cmd);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ServiceResultEntity { Success = false, Message = ex.ToString() };
            }
        }

        public ServiceResultEntity One(FolderDto request)
        {
            try
            {
                if (request.id is null)
                {
                    return new ServiceResultEntity { Success = false, Message = "Errores de validacion.", Data = "id no se esta enviando correctamente." };
                }

                using (SqlConnection conn = MainConnection.Connection(_configuration))
                {
                    using (SqlCommand cmd = new(_commandText, conn))
                    {
                        cmd.Parameters.AddWithValue("@opcion", 5);
                        cmd.Parameters.AddWithValue("@id", request.id);

                        return DataValidator.ValidateDatabaseData(cmd);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ServiceResultEntity { Success = false, Message = ex.ToString() };
            }
        }

        public ServiceResultEntity Subfolder(FolderDto request)
        {
            try
            {
                if (request.id is null)
                {
                    return new ServiceResultEntity { Success = false, Message = "Errores de validacion.", Data = "id no se esta enviando correctamente." };
                }

                using (SqlConnection conn = MainConnection.Connection(_configuration))
                {
                    using (SqlCommand cmd = new(_commandText, conn))
                    {
                        cmd.Parameters.AddWithValue("@opcion", 7);
                        cmd.Parameters.AddWithValue("@padre", request.padre);
                        cmd.Parameters.AddWithValue("@PageSize", request.pageSize);
                        cmd.Parameters.AddWithValue("@PageNumber", request.pageNumber);

                        return DataValidator.ValidateDatabaseData(cmd);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ServiceResultEntity { Success = false, Message = ex.ToString() };
            }
        }
    }
}
