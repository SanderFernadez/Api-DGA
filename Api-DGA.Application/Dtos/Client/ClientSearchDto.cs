namespace Api_DGA.Application.Dtos.Client
{
    /// <summary>
    /// DTO para búsquedas de clientes
    /// </summary>
    public class ClientSearchDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
