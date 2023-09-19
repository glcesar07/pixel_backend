using Application.DTO;
using Application.Validator;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;

namespace Infraestructure.Implementations
{
    public class OcrService : IOcrService
    {
        private readonly IConfiguration _configuration;
        private readonly OcrDtoValidator _validator;

        public OcrService(IConfiguration configuration, OcrDtoValidator validator)
        {
            _configuration = configuration;
            _validator = validator;
        }

        public ServiceResultEntity OCR(OcrDto request)
        {
            try
            {
                var validation = _validator.Validate(request);

                if (!validation.IsValid)
                {
                    return new ServiceResultEntity { Success = false, Message = "Errores de validacion.", Data = validation.Errors };
                }

                var client = new RestClient(_configuration["OCRService"]);
                RestRequest restRequest = new()
                {
                    Method = Method.Post
                };

                var requestBody = new
                {
                    base64_images = request.base64Images,
                    request.language
                };

                var jsonBody = JsonConvert.SerializeObject(requestBody);


                restRequest.AddJsonBody(jsonBody);

                var response = client.Execute<OcrResponseEntity>(restRequest);

                if (!response.IsSuccessful)
                {
                    return new ServiceResultEntity { Success = false, Message = response.ErrorMessage ?? "Error al consumir el servicio OCR.", Data = response.Content };
                }

                var responseData = new
                {
                    response.Data?.pages,
                    response.Data?.data
                };

                return new ServiceResultEntity { Success = true, Message = "Proceso realizado con éxito.", Data = responseData };                
            }
            catch (Exception ex)
            {
                return new ServiceResultEntity { Success = false, Message = ex.ToString() };
            }
        }
    }
}

