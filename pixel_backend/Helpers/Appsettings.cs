namespace Presentation.Helpers
{
    public class Appsettings
    {
        public ConnectionStringsConfig? ConnectionStrings { get; set; }
        public LoggingConfig? Logging { get; set; }
        public string? AllowedHosts { get; set; }
        public JwtConfig? Jwt { get; set; }
        public DataBaseConfig? DataBase { get; set; }
    }

    public class ConnectionStringsConfig
    {
        public string? Connection { get; set; }
    }

    public class LoggingConfig
    {
        public LogLevelConfig? LogLevel { get; set; }
    }

    public class LogLevelConfig
    {
        public string? Default { get; set; }
        public string? MicrosoftAspNetCore { get; set; }
    }

    public class JwtConfig
    {
        public string? OriginCors { get; set; }
        public string? Secret { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
    }

    public class DataBaseConfig
    {
        public StoredProceduresConfig? StoredProcedures { get; set; }
    }

    public class StoredProceduresConfig
    {
        public string? Login { get; set; }
    }
}
