namespace Core.Specifications
{
    public class PaginatedResponse<T>(int PageIndex, int PageSize, int Count, IEnumerable<T> Data)
    {
        public int PageIndex { get; set; } = PageIndex;
        public int PageSize { get; set; } = PageSize;
        public int Count { get; set; } = Count;
        public IEnumerable<T> Data { get; set; } = Data;
    }
}
