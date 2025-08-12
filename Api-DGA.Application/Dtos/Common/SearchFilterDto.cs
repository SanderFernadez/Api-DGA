namespace Api_DGA.Application.Dtos.Common
{
    /// <summary>
    /// DTO para filtros de búsqueda comunes
    /// </summary>
    public class SearchFilterDto
    {
        public string? SearchTerm { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SortBy { get; set; }
        public bool IsAscending { get; set; } = true;
        public bool IncludeInactive { get; set; } = false;
    }
}
