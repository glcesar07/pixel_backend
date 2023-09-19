using Application.DTO;
using Application.Functions;
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
    public class DocumentService : IDocumentService
    {
        private readonly IConfiguration _configuration;
        private readonly DocumentDtoValidator _validator;
        private readonly string _commandText;

        public DocumentService(IConfiguration configuration, DocumentDtoValidator loginDtoValidator)
        {
            _configuration = configuration;
            _validator = loginDtoValidator;
            _commandText = configuration["DataBase:StoredProcedures:Documento"];
        }

        public ServiceResultEntity Create(DocumentDto request)
        {
            try
            {
                var validation = _validator.Validate(request);

                if (!validation.IsValid)
                {
                    return new ServiceResultEntity { Success = false, Message = "Errores de validacion.", Data = validation.Errors };
                }

                string root = Path.Combine(_configuration["Root"], _configuration["Path"]);
                string pathCreateServer;
                byte[] fileBytes;

                try
                {
                    fileBytes = Convert.FromBase64String(request.archivo);
                }
                catch (FormatException)
                {
                    return new ServiceResultEntity { Success = false, Message = "El formato del archivo proporcionado no es válido." };
                }

                string nombre = request.nombre;
                string nombreFisico = CleanText.RemoveSpaces(nombre);
                nombreFisico = CleanText.RemoveAccents(nombreFisico);
                string extension = request.extension!;

                var allowedExtensions = new HashSet<string> { ".pdf", ".png", ".jpeg", ".jpg", ".webp"};

                if (!allowedExtensions.Contains(extension.ToLower()))
                {
                    return new ServiceResultEntity { Success = false, Message = "Formato de archivo no soportado." + " " + request.nombre };
                }

                using (SqlConnection conn = MainConnection.Connection(_configuration))
                {
                    conn.Open();

                    using (SqlCommand cmd = new(_commandText, conn))
                    {
                        SqlTransaction transaction = conn.BeginTransaction("carpetaTransaction");
                        cmd.Transaction = transaction;

                        cmd.Parameters.AddWithValue("@opcion", 7);
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@extension", extension);
                        cmd.Parameters.AddWithValue("@carpeta", request.carpeta);

                        ServiceResultEntity validationResult = DataValidator.ValidateDatabaseData(cmd);

                        if (!validationResult.Success)
                        {
                            transaction.Rollback();
                            return validationResult;
                        }

                        DataTable? table = validationResult.Data as DataTable;

                        if (table != null && table.Rows.Count <= 0)
                        {
                            cmd.Parameters.Clear();

                            string thisTime = DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss");
                            string nameUnique = Guid.NewGuid().ToString();
                            string newNameFisico = thisTime + "-" + nameUnique + "-" + nombreFisico;

                            pathCreateServer = Path.Combine(root, request.ubicacion, newNameFisico);

                            cmd.Parameters.AddWithValue("@opcion", 1);
                            cmd.Parameters.AddWithValue("@nombre", nombre);
                            cmd.Parameters.AddWithValue("@carpeta", request.carpeta);
                            cmd.Parameters.AddWithValue("@extension", extension);
                            cmd.Parameters.AddWithValue("@nombreFisico", newNameFisico);
                            cmd.Parameters.AddWithValue("@ubicacion", request.ubicacion);
                            cmd.Parameters.AddWithValue("@usuario", request.usuario ?? _configuration["UserCreate"]);

                            int result = cmd.ExecuteNonQuery();

                            if (result <= 0)
                            {
                                transaction.Rollback();
                                return new ServiceResultEntity { Success = false, Message = "No se ha podido almacenar el archivo." };
                            }

                            try
                            {
                                using (var filestream = new FileStream(pathCreateServer, FileMode.Create))
                                {
                                    filestream.Write(fileBytes, 0, fileBytes.Length);
                                }
                            }
                            catch (IOException ex)
                            {
                                transaction.Rollback();
                                return new ServiceResultEntity { Success = false, Message = "Error al escribir el archivo: " + ex.Message };
                            }
                            catch (UnauthorizedAccessException ex)
                            {
                                transaction.Rollback();
                                return new ServiceResultEntity { Success = false, Message = "No tiene permisos para escribir el archivo: " + ex.Message };
                            }

                            transaction.Commit();
                            return new ServiceResultEntity { Success = true, Message = "El archivo se ha almacenado con éxito." };
                        }
                        else
                        {
                            transaction.Rollback();
                            return new ServiceResultEntity { Success = false, Message = "No se ha podido almacenar el archivo, archivo duplicado." };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new ServiceResultEntity { Success = false, Message = ex.ToString() };
            }
        }


        public ServiceResultEntity Update(DocumentDto request)
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

        public ServiceResultEntity Action(DocumentDto request)
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

        public ServiceResultEntity All(DocumentDto request)
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

        public ServiceResultEntity One(DocumentDto request)
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

        public ServiceResultEntity Subfolder(DocumentDto request)
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
