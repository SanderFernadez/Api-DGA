namespace Api_DGA.Application.Dtos.Client
{
    /// <summary>
    /// DTO para obtener información de un cliente
    /// </summary>
    public class GetClientDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
