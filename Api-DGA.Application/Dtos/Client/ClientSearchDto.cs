namespace Api_DGA.Application.Dtos.Client
{
    /// <summary>
    /// DTO para búsquedas de clientes
    /// </summary>
    public class ClientSearchDto
    {
        /// <summary>
        /// ID del cliente
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del cliente
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Email del cliente
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Teléfono del cliente
        /// </summary>
        public string Phone { get; set; } = string.Empty;
    }
}
