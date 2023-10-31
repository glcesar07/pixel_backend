namespace Application.DTO
{
    public class ChangePasswordDto
    {
        public string? oldPassword { get; set; }
        public string? newPassword { get; set; }
        public string? confirmPassword { get; set; }    
        public int? persona { get; set; }
    }
}
