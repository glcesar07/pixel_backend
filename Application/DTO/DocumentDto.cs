namespace Application.DTO
{
    public class DocumentDto
    {
        public string nombre { get; set; }
        public string archivo { get; set; }
        public string ubicacion { get; set; }
        public int? id { get; set; }
        public int? carpeta { get; set; }
        public string? tamanio { get; set; }
        public string? extension { get; set; }
        public string? usuario { get; set; }
        public int? estado { get; set; }
        public int? pageSize { get; set; }
        public int? pageNumber { get; set; }
        public DocumentDto()
        {
            nombre = string.Empty;
            archivo = string.Empty;
            ubicacion = string.Empty;
        }
    }
}
