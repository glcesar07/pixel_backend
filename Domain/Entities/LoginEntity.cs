namespace Domain.Entities
{
    public class LoginEntity
    {
        public string Username { get; private set; }
        public string Password { get; private set; }

        public LoginEntity()
        {
            Username = string.Empty;
            Password = string.Empty;
        }
    }
}
