namespace SIMSWeb.Models
{
    public class PaginatedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalRecords / (double)PageSize);
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
    }
}
