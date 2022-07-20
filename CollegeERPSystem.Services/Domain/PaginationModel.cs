namespace CollegeERPSystem.Services.Domain
{
    public class PaginationModel
    {
        public int PageSize { get; set; } = 5;
        public int CurrentPage { get; set; } = 1;
        public string?  OrderList { get; set; }
        public string? FilterList { get; set; } = null;
    }
}
