namespace Api_DGA.Application.Dtos.Client
{
    /// <summary>
    /// DTO para actualizar un cliente existente
    /// </summary>
    public class UpdateClientDto
    {
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
