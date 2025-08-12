namespace Api_DGA.Application.Dtos.Common
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
        public int? PreviousPageNumber { get; set; }
        public int? NextPageNumber { get; set; }
    }
}
