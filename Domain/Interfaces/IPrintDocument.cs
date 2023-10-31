using Domain.Entities;
using Newtonsoft.Json.Linq;

namespace Domain.Interfaces
{
    public interface IPrintDocumentService
    {
        ServiceResultEntity PrintHtmlToPDF(JObject request);
    }
}
