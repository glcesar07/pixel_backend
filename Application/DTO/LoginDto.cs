using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Nombre de usuario requerido.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Contraseña requerida.")]
        public string Password { get; set; }

        public LoginDto()
        {
            Username = string.Empty;
            Password = string.Empty;
        }

    }
}
