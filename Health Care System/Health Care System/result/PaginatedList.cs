namespace Health_Care_System.result
{
    public class PaginatedList<T>
    {
        private const int MaxPageSize = 20;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int TotalRecords { get; set; }
        public int TotalPage { get; set; }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize)?MaxPageSize:value;
        }

        public IEnumerable<T>?Data  { get; set; }
    }
}
