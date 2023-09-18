namespace Application.DTO
{
    public class FolderDto
    {
        public string nombre { get; set; }
        public string nombreFisico { get; set; }
        public int? id { get; set; }
        public int? persona { get; set; }
        public int? padre { get; set; }
        public string? ubicacion { get; set; }
        public string? usuario { get; set; }
        public int? estado { get; set; }
        public int? pageSize { get; set; }
        public int? pageNumber { get; set; }
        public FolderDto() 
        { 
            nombre = string.Empty;
            nombreFisico = string.Empty;
        }
    }
}
