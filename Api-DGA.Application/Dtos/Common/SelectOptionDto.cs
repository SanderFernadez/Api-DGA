namespace Api_DGA.Application.Dtos.Common
{
    /// <summary>
    /// DTO para opciones de selección
    /// </summary>
    public class SelectOptionDto
    {
        public string Value { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public bool Selected { get; set; }
        public bool Disabled { get; set; }
        public string? Group { get; set; }
    }
}
