using Microsoft.EntityFrameworkCore;

namespace Electro_Project.Helpers.Pagging
{
    public class PaginatedList<T> : List<T>
    {

        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int TotalProducts { get; set; }

        public PaginatedList(List<T> products, int count, int pageIndex, int pageSize, int totalProducts)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalProducts = totalProducts;

            this.AddRange(products);
        }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public static PaginatedList<T> Create(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageIndex, pageSize, source.Count());
        }
    }
}
