namespace Api_DGA.Application.Dtos.Common
{
    /// <summary>
    /// DTO para estadísticas básicas
    /// </summary>
    public class StatisticsDto
    {
        public int TotalCount { get; set; }
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
    }
}
