using Newtonsoft.Json;

namespace Application.DTO
{
    public class OcrDto
    {
#nullable enable        
        public List<string>? base64Images { get; set; }        
        public string? language { get; set; }
    }
}
