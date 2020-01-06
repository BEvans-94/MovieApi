using Internal.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internal.Services
{
    public class Movie
    {
        private readonly IMovie<Models.Domain.Movie> _repo;

        public Movie(IMovie<Models.Domain.Movie> movieRepo)
        {
            _repo = movieRepo;
        }

        public async Task<List<Models.Domain.Movie>> GetAll()
        {
            return _repo.GetAll();
        }

        public async Task<Models.Domain.Movie> GetByTitleYear(string title, int year)
        {
            return  _repo.GetByTitleYear(title, year);
        }

        public async Task<List<Models.Domain.Movie>> Search(string searchTerm, Models.Enums.SearchType searchType)
        {
            var searchTerms = new List<string>();

            if (searchType == Models.Enums.SearchType.Actors || searchType == Models.Enums.SearchType.Directors || searchType == Models.Enums.SearchType.Genres)
            {
                searchTerms.AddRange(searchTerm.Split(','));
                searchTerms = searchTerms.Select(x => x.Trim()).ToList(); //Trim all of the strings added to the list
            }
            else searchTerms.Add(searchTerm);

            return  _repo.Search(searchTerms, searchType);
        }
    }
}
