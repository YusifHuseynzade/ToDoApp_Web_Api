namespace DTO.PaginationDto
{
    public class PaginationListDto<T>
    {
        public PaginationListDto(List<T> items, int pageIndex, int pageSize, int totalCount)
        {
            Items = items;
            PageSize = pageSize;
            PageIndex = pageIndex;
            TotalPage = (int)Math.Ceiling(totalCount / (double)pageSize);
        }
        public List<T> Items { get; set; }
        public bool HasNext => PageIndex < TotalPage;
        public bool HasPrev => PageIndex > 1;
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPage { get; set; }
    }
}
