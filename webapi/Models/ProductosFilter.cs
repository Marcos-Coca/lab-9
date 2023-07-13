namespace webapi.Models
{
    public class ProductosFilter
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? Search { get; set; }
        public string? OrderBy { get; set; }
        public string? OrderDirection { get; set; }

        public ProductosFilter()
        {
            Page = 1;
            PageSize = 10;
        }
    }
}
