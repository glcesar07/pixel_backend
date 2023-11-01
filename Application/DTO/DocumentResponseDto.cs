namespace Application.DTO
{
    public class DocumentResponseDto
    {
        public int Pages { get; set; }
        public List<PageResponseDto> Data { get; set; } = new List<PageResponseDto>();
    }

    public class PageResponseDto
    {
        public int Page { get; set; }
        public List<ParagraphResponseDto> Data { get; set; } = new List<ParagraphResponseDto>();
    }

    public class ParagraphResponseDto
    {
        public string? Paragraph { get; set; }
    }

}
