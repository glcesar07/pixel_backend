namespace Domain.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(dynamic userObject);
    }
}
