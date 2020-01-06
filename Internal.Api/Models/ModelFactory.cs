using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Internal.Api.Models
{
    public class ModelFactory
    {
        #region Movies

        #region Stub

        public List<Models.Return.MovieStubReturnModel> CreateStub_Movie(List<Internal.Models.Domain.Movie> movies)
        {
            var stubList = new List<Models.Return.MovieStubReturnModel>();

            foreach (var movie in movies)
            {
                stubList.Add(CreateStub_Movie(movie));
            }

            return stubList;
        }

        public Models.Return.MovieStubReturnModel CreateStub_Movie(Internal.Models.Domain.Movie movie)
        {
            var stub = new Models.Return.MovieStubReturnModel()
            {
                Year = movie.Year,
                Title = movie.Title
            };

            return stub;
        }

        #endregion

        #region Full

        public List<Models.Return.MovieFullReturnModel> CreateFull_Movie(List<Internal.Models.Domain.Movie> movies)
        {
            var fullList = new List<Models.Return.MovieFullReturnModel>();

            foreach (var movie in movies)
            {
                fullList.Add(CreateFull_Movie(movie));
            }

            return fullList;
        }

        public Models.Return.MovieFullReturnModel CreateFull_Movie(Internal.Models.Domain.Movie movie)
        {
            var fullInfo = new Models.Return.MovieInfoFullReturnModel()
            {
                Directors = movie.Info.Directors,
                Release_Date = movie.Info.Release_Date,
                Rating = movie.Info.Rating,
                Genres = movie.Info.Genres,
                Image_Url = movie.Info.Image_Url,
                Plot = movie.Info.Plot,
                Rank = movie.Info.Rank,
                Running_Time_Secs = movie.Info.Running_Time_Secs,
                Actors = movie.Info.Actors
            };

            var full = new Models.Return.MovieFullReturnModel()
            {
                Year = movie.Year,
                Title = movie.Title,
                Info = fullInfo
            };

            return full;
        }

        #endregion


        #endregion

    }
}