namespace Api_DGA.Application.Dtos.Client
{
    /// <summary>
    /// DTO para crear un nuevo cliente
    /// </summary>
    public class CreateClientDto
    {
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
