using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class CatalogueDto
    {
        [Required]
        public int catalogo { get; set; }
#nullable enable
        public int? id { get; set; }        
        public string? nombre { get; set; }
        public string? codigo { get; set; }
        public string? usuario { get; set; }
        public int? estado { get; set; }

        public CatalogueDto() 
        {
            catalogo = 0;
        }
    }
}
