namespace Domain.Entities
{
    public class ServiceResultEntity
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object? Data { get; set; }

        public ServiceResultEntity() 
        { 
            Success = false;
            Message = string.Empty;
        }
    }
}
