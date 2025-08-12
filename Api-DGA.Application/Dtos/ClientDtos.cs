namespace Api_DGA.Application.Dtos
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

    /// <summary>
    /// DTO para crear un nuevo cliente
    /// </summary>
    public class CreateClientDto
    {
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO para actualizar un cliente existente
    /// </summary>
    public class UpdateClientDto
    {
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}

