using Domain.Entities;
using Domain.Interfaces;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using Newtonsoft.Json.Linq;

namespace Infraestructure.Implementations
{
    public class PrintDocumentService : IPrintDocumentService
    {

        public ServiceResultEntity PrintHtmlToPDF(JObject request)
        {
            try
            {
                // Obtener el contenido HTML del request
                var htmlContent = request.GetValue("htmlContent")?.ToString();

                if (string.IsNullOrEmpty(htmlContent))
                {
                    return new ServiceResultEntity { Success = false, Message = "El contenido HTML es requerido." };
                }

                // Convertir HTML a PDF
                var pdfStream = new MemoryStream();
                using (var writer = new PdfWriter(pdfStream))
                {
                    var pdfDocument = new PdfDocument(writer);
                    var converterProperties = new ConverterProperties();
                    converterProperties.SetBaseUri(Directory.GetCurrentDirectory());

                    HtmlConverter.ConvertToPdf(htmlContent, pdfDocument, converterProperties);
                }

                // Convertir el MemoryStream a Base64 y devolverlo
                var base64String = Convert.ToBase64String(pdfStream.ToArray());

                return new ServiceResultEntity
                {
                    Success = true,
                    Message = "Proceso realizado con éxito.",
                    Data = base64String
                };
            }
            catch (Exception ex)
            {
                return new ServiceResultEntity { Success = false, Message = ex.Message };
            }
        }
    }
}
