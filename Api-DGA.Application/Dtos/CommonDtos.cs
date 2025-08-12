namespace Api_DGA.Application.Dtos
{
    /// <summary>
    /// DTO para respuestas paginadas
    /// </summary>
    /// <typeparam name="T">Tipo de datos a paginar</typeparam>
    public class PaginatedResultDto<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
    }

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
    }

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
    }

    /// <summary>
    /// DTO para respuestas simples sin datos
    /// </summary>
    public class ApiResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new List<string>();
    }
}
