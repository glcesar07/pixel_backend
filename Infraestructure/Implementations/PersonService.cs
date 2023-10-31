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
    public class PersonService : IPersonService
    {
        private readonly IConfiguration _configuration;
        private readonly PersonDtoValidator _validator;
        private readonly string _commandText;

        public PersonService(IConfiguration configuration, PersonDtoValidator loginDtoValidator)
        {
            _configuration = configuration;
            _validator = loginDtoValidator;
            _commandText = configuration["DataBase:StoredProcedures:Persona"];
        }

        public ServiceResultEntity Create(PersonDto request)
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
                    using (SqlCommand cmd = new SqlCommand(_commandText, conn))
                    {
                        cmd.Parameters.AddWithValue("@opcion", 1);
                        cmd.Parameters.AddWithValue("@nombre", request.nombre);
                        cmd.Parameters.AddWithValue("@email", request.email);
                        cmd.Parameters.AddWithValue("@fechaNacimiento", request.fechaNacimiento);
                        cmd.Parameters.AddWithValue("@ciudad", request.ciudad);
                        cmd.Parameters.AddWithValue("@tipoPersona", request.tipoPersona);
                        cmd.Parameters.AddWithValue("@genero", request.genero);

                        cmd.Parameters.AddWithValue("@empresa", request.empresa);
                        cmd.Parameters.AddWithValue("@password", BCrypt.Net.BCrypt.HashPassword(request.password) ?? BCrypt.Net.BCrypt.HashPassword(_configuration["PasswordUser"]));                        
                        cmd.Parameters.AddWithValue("@rol", request.rol ?? 2);
                        cmd.Parameters.AddWithValue("@proveedorAutenticacion", request.proveedorAutenticacion);

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

        public ServiceResultEntity Update(PersonDto request)
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
                    using (SqlCommand cmd = new SqlCommand(_commandText, conn))
                    {
                        cmd.Parameters.AddWithValue("@opcion", 2);
                        cmd.Parameters.AddWithValue("@id", request.id);
                        cmd.Parameters.AddWithValue("@nombre", request.nombre);
                        cmd.Parameters.AddWithValue("@email", request.email);
                        cmd.Parameters.AddWithValue("@fechaNacimiento", request.fechaNacimiento);
                        cmd.Parameters.AddWithValue("@ciudad", request.ciudad);
                        cmd.Parameters.AddWithValue("@tipoPersona", request.tipoPersona);
                        cmd.Parameters.AddWithValue("@genero", request.genero);
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

        public ServiceResultEntity Action(PersonDto request)
        {
            try
            {

                if (request.id is null || request.estado is null || request.usuario is null)
                {
                    return new ServiceResultEntity { Success = false, Message = "Errores de validacion.", Data = "id, estado y usuario no se estan enviando correctamente." };
                }

                using (SqlConnection conn = MainConnection.Connection(_configuration))
                {
                    using (SqlCommand cmd = new SqlCommand(_commandText, conn))
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

        public ServiceResultEntity All(PersonDto request)
        {
            try
            {
                using (SqlConnection conn = MainConnection.Connection(_configuration))
                {
                    using (SqlCommand cmd = new SqlCommand(_commandText, conn))
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

        public ServiceResultEntity One(PersonDto request)
        {
            try
            {
                if (request.id is null)
                {
                    return new ServiceResultEntity { Success = false, Message = "Errores de validacion.", Data = "id no se esta enviando correctamente." };
                }

                using (SqlConnection conn = MainConnection.Connection(_configuration))
                {
                    using (SqlCommand cmd = new SqlCommand(_commandText, conn))
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

        public ServiceResultEntity Label(PersonDto request)
        {
            try
            {
                using (SqlConnection conn = MainConnection.Connection(_configuration))
                {
                    using (SqlCommand cmd = new SqlCommand(_commandText, conn))
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
    }
}
