


namespace MethodVerb.Responses
{
    public class PaginationResponse<T> 
    {
        public IEnumerable<T> Item { get; set; } 
        public int PageSize { get; set; }
        public int CountItems { get; set; }
        public int PageTotal => (int)Math.Ceiling(CountItems/(double)PageSize);

        public int PageCurrent { get; set; }

        private PaginationResponse() { }
        public static PaginationResponse<T> Create(IEnumerable<T> items , int pageSize , int countItem , int pageCurrent)
        {
            return new PaginationResponse<T>
            {
                Item = items,
                PageSize = pageSize,
                CountItems = countItem,
                PageCurrent = pageCurrent,
             };
        }

    }
}
