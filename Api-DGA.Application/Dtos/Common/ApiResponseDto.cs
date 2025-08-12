namespace Api_DGA.Application.Dtos.Common
{
    /// <summary>
    /// DTO para respuestas de operaciones
    /// </summary>
    /// <typeparam name="T">Tipo de datos de la respuesta</typeparam>
    public class ApiResponseDto<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public int StatusCode { get; set; } = 200;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

    /// <summary>
    /// DTO para respuestas simples sin datos
    /// </summary>
    public class ApiResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new List<string>();
        public int StatusCode { get; set; } = 200;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
