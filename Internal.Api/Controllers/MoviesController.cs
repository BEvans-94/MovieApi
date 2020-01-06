using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Internal.Api.Controllers
{
    public class MoviesController : BaseController
    {

        private static Repository.IMovie<Internal.Models.Domain.Movie> movieRepo = new Internal.Repository.Text.Movie<Internal.Models.Domain.Movie>();
        private static Services.Movie movieService = new Internal.Services.Movie(movieRepo);

        private static Models.ModelFactory modelFactory = new Models.ModelFactory();

        /// <summary>
        /// Gets Four Movies, ordered by year
        /// </summary>
        /// <param name="year">True = Asc, False = Desc (False is default)</param>
        /// <returns></returns>
        [HttpGet]
        [Route("movies/GetByYear")]
        public async Task<IHttpActionResult> GetByYear(bool asc = false)
        {
            var results = await movieService.GetAll();

            results = results.OrderByDescending(x => x.Year).ToList();
            if (asc) results = results.OrderBy(x => x.Year).ToList();

            if (results == null || (results != null && results.Count() == 0)) return NotFound();

            //We only need basic info and we only need 4 displayed
            var returnModel = modelFactory.CreateStub_Movie(results.Take(4).ToList());

            //Setup an action response to give back status, message, response object? This then leaves HTTP status' for critical errors
            return Ok(returnModel);
        }

        /// <summary>
        /// Get movie by title and year
        /// </summary>
        /// <param name="title">Title of movie</param>
        /// <param name="year">Year of movie</param>
        /// <returns></returns>
        [HttpGet]
        [Route("movies/GetByTitleYear")]
        public async Task<IHttpActionResult> GetByTitleYear(string title, int year)
        {
            var result = await movieService.GetByTitleYear(title, year);

            if (result == null) return NotFound();

            var returnModel = modelFactory.CreateFull_Movie(result);

            //Setup an action response to give back status, message, response object? This then leaves HTTP status' for critical errors
            return Ok(returnModel);
        }

        /// <summary>
        /// Search Movies
        /// </summary>
        /// <param name="searchTerm">Term to search against. If multiple terms please seperate with a comma</param>
        /// <param name="searchType">Year = 0, Title = 1, Directors = 2, ReleaseDate = 3, Rating = 4, Genres = 5, Rank = 6, RunTime = 7, Actors = 8</param>
        /// <returns></returns>
        [HttpGet]
        [Route("movies/Search")]
        public async Task<IHttpActionResult> Search(string searchTerm, Internal.Models.Enums.SearchType searchType)
        {
            var results = await movieService.Search(searchTerm, searchType);

            if (results == null || (results != null && results.Count() == 0)) return NotFound();

            //This results could be ordered, I have not done so as this is more of a preference.

            var returnModelList = modelFactory.CreateStub_Movie(results);

            //Setup an action response to give back status, message, response object? This then leaves HTTP status' for critical errors
            return Ok(returnModelList);
        }



    }
}
