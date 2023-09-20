using Application.DTO;
using Application.Validator;
using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Database;
using Infraestructure.Helpers;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Infraestructure.Implementations
{
    public class ParagraphService : IParagraphService
    {
        private readonly IConfiguration _configuration;
        private readonly ParagraphDtoValidator _validator;
        private readonly string _commandTextDocumento;
        private readonly string _commandTextPagina;
        private readonly string _commandTextParrafos;

        public ParagraphService(IConfiguration configuration, ParagraphDtoValidator loginDtoValidator)
        {
            _configuration = configuration;
            _validator = loginDtoValidator;
            _commandTextDocumento = configuration["DataBase:StoredProcedures:Documento"];
            _commandTextPagina = configuration["DataBase:StoredProcedures:Pagina"];
            _commandTextParrafos = configuration["DataBase:StoredProcedures:Parrafos"];
        }

        public ServiceResultEntity Create(ParagraphDtoReq request)
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
                    conn.Open();

                    using (SqlCommand cmd = new(_commandTextDocumento, conn))
                    {
                        SqlTransaction transaction = conn.BeginTransaction("transaction");
                        cmd.Transaction = transaction;

                        cmd.Parameters.AddWithValue("@opcion", 1);
                        cmd.Parameters.AddWithValue("@nombre", request.nombreDocumento);
                        cmd.Parameters.AddWithValue("@carpeta", request.carpeta);
                        cmd.Parameters.AddWithValue("@usuario", request.usuario ?? _configuration["UserCreate"]);

                        ServiceResultEntity validationResult = DataValidator.ValidateDatabaseData(cmd);

                        DataTable? documentoDataTable = validationResult.Data as DataTable;

                        if (documentoDataTable == null)
                        {
                            transaction.Rollback();
                            return new ServiceResultEntity { Success = false, Message = "Error al obtener datos del documento." };
                        }

                        cmd.CommandText = _commandTextPagina;
                        cmd.Parameters.Clear();

                        cmd.Parameters.AddWithValue("@opcion", 1);
                        cmd.Parameters.AddWithValue("@documento", documentoDataTable.Rows[0]["id"]);
                        cmd.Parameters.AddWithValue("@usuario", request.usuario ?? _configuration["UserCreate"]);

                        ServiceResultEntity pageResult = DataValidator.ValidateDatabaseData(cmd);

                        if (pageResult.Data is not DataTable paginasDataTable)
                        {
                            transaction.Rollback();
                            return new ServiceResultEntity { Success = false, Message = "Error al obtener datos del documento." };
                        }

                        cmd.CommandText = _commandTextParrafos;

                        foreach (var pageData in request.data!)
                        {
                            foreach (var paragraph in pageData.data!)
                            {
                                cmd.Parameters.Clear();

                                cmd.Parameters.AddWithValue("@opcion", 1);
                                cmd.Parameters.AddWithValue("@documento", documentoDataTable.Rows[0]["id"]);
                                cmd.Parameters.AddWithValue("@pagina", paginasDataTable.Rows[0]["id"]); // Usando el ID de la página obtenido previamente
                                cmd.Parameters.AddWithValue("@parrafo", paragraph.paragraph);
                                cmd.Parameters.AddWithValue("@usuario", request.usuario ?? _configuration["UserCreate"]);

                                ServiceResultEntity paragraphResult = DataValidator.ValidateDatabaseData(cmd);

                                if (!paragraphResult.Success)
                                {
                                    transaction.Rollback();
                                    return paragraphResult;
                                }
                            }
                        }

                        transaction.Commit();
                        return new ServiceResultEntity { Success = true, Message = "El archivo se ha almacenado con éxito." };
                    }
                }
            }
            catch (Exception ex)
            {
                return new ServiceResultEntity { Success = false, Message = ex.ToString() };
            }
        }


        public ServiceResultEntity Update(ParagraphDtoReq request)
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
                    using (SqlCommand cmd = new(_commandTextParrafos, conn))
                    {
                        cmd.Parameters.AddWithValue("@opcion", 2);
                        cmd.Parameters.AddWithValue("@id", request.id);
                        cmd.Parameters.AddWithValue("@nombre", request.nombreDocumento);
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

        public ServiceResultEntity Action(ParagraphDtoReq request)
        {
            try
            {

                if (request.id is null || request.estado is null || request.usuario is null)
                {
                    return new ServiceResultEntity { Success = false, Message = "Errores de validacion.", Data = "id, estado y usuario no se estan enviando correctamente." };
                }

                using (SqlConnection conn = MainConnection.Connection(_configuration))
                {
                    using (SqlCommand cmd = new(_commandTextParrafos, conn))
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

        public ServiceResultEntity All(ParagraphDtoReq request)
        {
            try
            {
                using (SqlConnection conn = MainConnection.Connection(_configuration))
                {
                    using (SqlCommand cmd = new(_commandTextParrafos, conn))
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

        public ServiceResultEntity One(ParagraphDtoReq request)
        {
            try
            {
                if (request.id is null)
                {
                    return new ServiceResultEntity { Success = false, Message = "Errores de validacion.", Data = "id no se esta enviando correctamente." };
                }

                using (SqlConnection conn = MainConnection.Connection(_configuration))
                {
                    using (SqlCommand cmd = new(_commandTextParrafos, conn))
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
    }
}
