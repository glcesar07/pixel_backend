namespace Application.DTO
{
    public class ParagraphDtoReq
    {
        public int? id { get; set; }
        public int? carpeta { get; set; }
        public int? nombreDocumento { get; set; }
        public List<PageDataDto>? data { get; set; }
        public string? usuario { get; set; }
        public int? estado { get; set; }
    }

    public class PageDataDto
    {
        public int Page { get; set; }
        public List<Paragraph>? data { get; set; }
    }

    public class Paragraph
    {
        public string? paragraph { get; set; }
    }
}
