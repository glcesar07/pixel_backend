namespace Domain.Entities
{
    public class OcrResponseEntity
    {
        public bool success { get; set; }
        public string message { get; set; }
        public List<PageDataDto>? data { get; set; }
        public int pages { get; set; }
        public OcrResponseEntity() 
        {
            success = false;
            message = string.Empty;            
            pages = 0;
        }
    }

    public class PageDataDto
    {
        public int Page { get; set; }
        public List<ParagraphDto>? Data { get; set; }
    }

    public class ParagraphDto
    {
        public string? Paragraph { get; set; }
    }
}
