using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Internal.Repository
{
    public interface IMovie<T> : IRepositoryBase<T>
    {
        Models.Domain.Movie GetByTitleYear(string title, int year);

        List<Models.Domain.Movie> Search(List<string> searchTerms, Models.Enums.SearchType searchType);
    }
}
