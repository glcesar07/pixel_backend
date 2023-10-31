namespace Application.DTO
{
    public class PersonDto
    {
        public int? id { get; set; }
        public string? nombre { get; set; }
        public string? email { get; set; }
        public string? fechaNacimiento { get; set; }
        public int? ciudad { get; set; }
        public int? tipoPersona { get; set; }
        public int? genero { get; set; }
        public string? usuario { get; set; }
        public int? estado { get; set; }

        public string? empresa { get; set; }
        public string? password { get; set; }
        public int? rol { get; set; }
        public int? proveedorAutenticacion { get; set; }
        public int? pageSize { get; set; }
        public int? pageNumber { get; set; }
    }
}
