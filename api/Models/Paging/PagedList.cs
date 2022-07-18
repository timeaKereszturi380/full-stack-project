using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.Paging
{
    public class PagedList<TModel>
    {
       
        public int TotalItems { get; private set; }
        
        public int PageNumber { get; set; }
        
        public int PageSize { get; private set; }
       
        public List<TModel> List { get; private set; }

        public int TotalPages =>
            (int)Math.Ceiling(TotalItems / (double)PageSize);
        
        public bool HasPreviousPage => PageNumber > 1;
       
        public bool HasNextPage => PageNumber < TotalPages;
        public int NextPageNumber =>
            this.HasNextPage ? PageNumber + 1 : TotalPages;
        public int PreviousPageNumber =>
            this.HasPreviousPage ? PageNumber - 1 : 1;


        private PagedList()
        {
        }

        public static PagedList<TModel> FromExisting(ICollection<TModel> list, int totalItems, int pageNumber, int pageSize)
        {
            HandleExceptions(list, pageNumber, pageSize, totalItems);
            var enumerable = list?.ToList();
            var model = new PagedList<TModel>()
            {
                TotalItems = totalItems,
                PageNumber = pageNumber,
                PageSize = pageSize,
                List = new List<TModel>(enumerable)
            };
            return model;
        }


        private static void HandleExceptions(ICollection<TModel> list, int pageNumber, int pageSize, int totalItems = 0)
        {
            if (list == null)
            {
                throw new PagedListException("List is null");
            }

            if (pageNumber < 1)
            {
                throw new PagedListException("The provided page number must be higher or equal to 1.");
            }

            if (pageSize < 1)
            {
                throw new PagedListException("The provided page size must be higher or equal to 1.");
            }
        }


        public static PagedList<TModel> CreatePagedList(ICollection<TModel> list, int pageNumber,
            int pageSize)
        {
            HandleExceptions(list, pageNumber, pageSize);

            var model = new PagedList<TModel>()
            {
                TotalItems = list.Count(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                List = list
                    .Skip(pageSize * (pageNumber - 1))
                    .Take(pageSize)
                    .ToList()
            };

            return model;
        }

    }

    public static class PagedListExtensions
    {
        public static PagedList<TModel> SortBy<TModel>(this PagedList<TModel> pagedList, string attribute, string direction)
        {

            var tmpList = new List<TModel>();
            var prop = TypeDescriptor.GetProperties(typeof(TModel)).Find(attribute, true);

            tmpList = direction.Equals("Descending", StringComparison.CurrentCultureIgnoreCase)
                ? pagedList.List.OrderByDescending(x => prop.GetValue(x)).ToList()
                : pagedList.List.OrderBy(x => prop.GetValue(x)).ToList();

            var list = PagedList<TModel>.CreatePagedList(tmpList, pagedList.PageNumber,
                pagedList.PageSize);

            return list;
        }
    }

    public class PagedListException : Exception
    {
        public PagedListException(string message) : base(message)
        {
        }
    }
}

