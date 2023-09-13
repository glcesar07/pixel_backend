using Application.DTO;
using Application.Validator;
using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Database;
using Infraestructure.Helpers;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace Infraestructure.Implementations
{
    public class CatalogueService : ICatalogueService
    {
        private readonly IConfiguration _configuration;        
        private readonly CatalogueDtoValidator _validator;

        public CatalogueService(IConfiguration configuration, CatalogueDtoValidator loginDtoValidator)
        {
            _configuration = configuration;            
            _validator = loginDtoValidator;
        }

        public ServiceResultEntity Create(CatalogueDto request)
        {
            try
            {
                var validation = _validator.Validate(request);

                if (!validation.IsValid)
                {
                    return new ServiceResultEntity { Success = false, Message = "Errores de validacion.", Data = validation.Errors };
                }

                string storedProcedureName = GetStoredProcedureName(request.catalogo);

                if (string.IsNullOrEmpty(storedProcedureName))
                {
                    return new ServiceResultEntity { Success = false, Message = "Valor de catálogo no válido." };
                }

                using (SqlConnection conn = MainConnection.Connection(_configuration))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, conn))
                    {
                        cmd.Parameters.AddWithValue("@opcion", 1);
                        cmd.Parameters.AddWithValue("@nombre", request.nombre);
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

        public ServiceResultEntity Update(CatalogueDto request)
        {
            try
            {
                var validation = _validator.Validate(request);

                if (!validation.IsValid)
                {
                    return new ServiceResultEntity { Success = false, Message = "Errores de validacion.", Data = validation.Errors };
                }

                string storedProcedureName = GetStoredProcedureName(request.catalogo);

                if (string.IsNullOrEmpty(storedProcedureName))
                {
                    return new ServiceResultEntity { Success = false, Message = "Valor de catálogo no válido." };
                }

                using (SqlConnection conn = MainConnection.Connection(_configuration))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, conn))
                    {
                        cmd.Parameters.AddWithValue("@opcion", 2);
                        cmd.Parameters.AddWithValue("@id", request.id);
                        cmd.Parameters.AddWithValue("@nombre", request.nombre);
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

        public ServiceResultEntity Action(CatalogueDto request)
        {
            try
            {

                if (request.id is null || request.estado is null || request.usuario is null)
                {
                    return new ServiceResultEntity { Success = false, Message = "Errores de validacion.", Data = "id, estado y usuario no se estan enviando correctamente." };
                }

                string storedProcedureName = GetStoredProcedureName(request.catalogo);

                if (string.IsNullOrEmpty(storedProcedureName))
                {
                    return new ServiceResultEntity { Success = false, Message = "Valor de catálogo no válido." };
                }

                using (SqlConnection conn = MainConnection.Connection(_configuration))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, conn))
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

        public ServiceResultEntity All(CatalogueDto request)
        {
            try
            {
                string storedProcedureName = GetStoredProcedureName(request.catalogo);

                if (string.IsNullOrEmpty(storedProcedureName))
                {
                    return new ServiceResultEntity { Success = false, Message = "Valor de catálogo no válido." };
                }

                using (SqlConnection conn = MainConnection.Connection(_configuration))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, conn))
                    {
                        cmd.Parameters.AddWithValue("@opcion", 4);

                        return DataValidator.ValidateDatabaseData(cmd);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ServiceResultEntity { Success = false, Message = ex.ToString() };
            }
        }

        public ServiceResultEntity One(CatalogueDto request)
        {
            try
            {
                if (request.id is null)
                {
                    return new ServiceResultEntity { Success = false, Message = "Errores de validacion.", Data = "id no se esta enviando correctamente." };
                }

                string storedProcedureName = GetStoredProcedureName(request.catalogo);

                if (string.IsNullOrEmpty(storedProcedureName))
                {
                    return new ServiceResultEntity { Success = false, Message = "Valor de catálogo no válido." };
                }

                using (SqlConnection conn = MainConnection.Connection(_configuration))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, conn))
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

        public ServiceResultEntity Label(CatalogueDto request)
        {
            try
            {
                string storedProcedureName = GetStoredProcedureName(request.catalogo);

                if (string.IsNullOrEmpty(storedProcedureName))
                {
                    return new ServiceResultEntity { Success = false, Message = "Valor de catálogo no válido." };
                }

                using (SqlConnection conn = MainConnection.Connection(_configuration))
                {
                    using (SqlCommand cmd = new SqlCommand(storedProcedureName, conn))
                    {
                        cmd.Parameters.AddWithValue("@opcion", 6);

                        return DataValidator.ValidateDatabaseData(cmd);
                    }
                }
            }
            catch (Exception ex)
            {
                return new ServiceResultEntity { Success = false, Message = ex.ToString() };
            }
        }

        private string GetStoredProcedureName(int catalogo)
        {
            switch (catalogo)
            {
                case 1:
                    return _configuration["DataBase:StoredProcedures:Rol"];
                case 2:
                    return _configuration["DataBase:StoredProcedures:Genero"];
                case 3:
                    return _configuration["DataBase:StoredProcedures:Empresa"];
                case 4:
                    return _configuration["DataBase:StoredProcedures:TipoPersona"];
                case 5:
                    return _configuration["DataBase:StoredProcedures:ProveedorAutenticacion"];
                case 6:
                    return _configuration["DataBase:StoredProcedures:PalabrasClave"];                
                default:
                    return "";
            }
        }
    }
}
