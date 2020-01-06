using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Internal.Api.Testing
{
    [TestClass]
    public class ModelFactory_Tests
    {
        private static Internal.Models.Domain.Movie _TestMovie;
        private static List<Internal.Models.Domain.Movie> _TestMovieList;

        private static Internal.Api.Models.ModelFactory _ModelFactory;

        public static void RunSetup()
        {
            try
            {
                Repository.IMovie<Internal.Models.Domain.Movie> movieRepo = new Repository.Text.Movie<Internal.Models.Domain.Movie>();
                var movieService = new Services.Movie(movieRepo);

                _ModelFactory = new Internal.Api.Models.ModelFactory();

                var testMovies = movieService.GetAll().Result;
                if (testMovies != null && testMovies.Count > 0)
                {
                    _TestMovie = testMovies.FirstOrDefault();
                    _TestMovieList = testMovies.Take(5).ToList();
                }
                else
                {
                    //Maybe log?
                    throw new AssertFailedException("Error obtaining test data.");
                }

            }
            catch (Exception ex)
            {
                //Maybe log?
                throw new AssertFailedException(string.Format("Error creating test data. Ex: {0}", ex.Message));
            }
        }


        [TestMethod]
        public void CreateFull_Movie_Test()
        {
            RunSetup();

            var expectedResults = new List<Internal.Models.Domain.Movie>();
            var actualResults = new List<Internal.Api.Models.Return.MovieFullReturnModel>();

            expectedResults.Add(_TestMovie);
            actualResults.Add(_ModelFactory.CreateFull_Movie(_TestMovie));

            AssertFullMovieList(expectedResults, actualResults);
        }

        [TestMethod]
        public void CreateFull_Movies_Test()
        {
            RunSetup();

            var expectedResults = new List<Internal.Models.Domain.Movie>();
            var actualResults = new List<Internal.Api.Models.Return.MovieFullReturnModel>();

            expectedResults.AddRange(_TestMovieList);
            actualResults.AddRange(_ModelFactory.CreateFull_Movie(_TestMovieList));

            AssertFullMovieList(expectedResults, actualResults);
        }

        [TestMethod]
        public void CreateStub_Movie_Test()
        {
            RunSetup();

            var expectedResults = new List<Internal.Models.Domain.Movie>();
            var actualResults = new List<Internal.Api.Models.Return.MovieStubReturnModel>();

            expectedResults.Add(_TestMovie);
            actualResults.Add(_ModelFactory.CreateStub_Movie(_TestMovie));

            AssertStubMovieList(expectedResults, actualResults);
        }

        [TestMethod]
        public void CreateStub_Movies_Test()
        {
            RunSetup();

            var expectedResults = new List<Internal.Models.Domain.Movie>();
            var actualResults = new List<Internal.Api.Models.Return.MovieStubReturnModel>();

            expectedResults.AddRange(_TestMovieList);
            actualResults.AddRange(_ModelFactory.CreateStub_Movie(_TestMovieList));

            AssertStubMovieList(expectedResults, actualResults);
        }

        public void AssertStubMovieList(List<Internal.Models.Domain.Movie> expectedResults, List<Internal.Api.Models.Return.MovieStubReturnModel> actualResults)
        {
            if (expectedResults == null || expectedResults.Count == 0 || actualResults == null || actualResults.Count == 0) Assert.Fail();

            for (int i = 0; i == expectedResults.Count; i++)
            {
                if (i == expectedResults.Count) break;

                var expectedResult = expectedResults.ElementAt(i);
                var actualResult = actualResults.ElementAt(i);

                Assert.AreEqual(expectedResult.Title, actualResult);
                Assert.AreEqual(expectedResult.Year, actualResult.Year);
            }
        }

        public void AssertFullMovieList(List<Internal.Models.Domain.Movie> expectedResults, List<Internal.Api.Models.Return.MovieFullReturnModel> actualResults)
        {
            if (expectedResults == null || expectedResults.Count == 0 || actualResults == null || actualResults.Count == 0) Assert.Fail();

            for (int i = 0; i == expectedResults.Count; i++)
            {
                if (i == expectedResults.Count) break;

                var expectedResult = expectedResults.ElementAt(i);
                var actualResult = actualResults.ElementAt(i);

                //I know there are nuget packages that can compare whole objects but sometimes entire objects don't need asserting so done it prop by prop.
                //This can then be edited and modified to mimic any modelfactory changes.

                Assert.AreEqual(expectedResult.Title, actualResult);
                Assert.AreEqual(expectedResult.Year, actualResult.Year);

                if (expectedResult.Info != null && actualResult.Info != null)
                {
                    if ((expectedResult.Info.Actors != null && expectedResult.Info.Actors.Count > 0) && (actualResult.Info.Actors != null && actualResult.Info.Actors.Count > 0))
                    {
                        Assert.AreEqual(true, expectedResult.Info.Actors.SequenceEqual(actualResult.Info.Actors));
                    }
                    if ((expectedResult.Info.Directors != null && expectedResult.Info.Directors.Count > 0) && (actualResult.Info.Directors != null && actualResult.Info.Directors.Count > 0))
                    {
                        Assert.AreEqual(true, expectedResult.Info.Directors.SequenceEqual(actualResult.Info.Directors));
                    }
                    if ((expectedResult.Info.Genres != null && expectedResult.Info.Genres.Count > 0) && (actualResult.Info.Genres != null && actualResult.Info.Genres.Count > 0))
                    {
                        Assert.AreEqual(true, expectedResult.Info.Genres.SequenceEqual(actualResult.Info.Genres));
                    }

                    Assert.AreEqual(expectedResult.Info.Image_Url, actualResult.Info.Image_Url);
                    Assert.AreEqual(expectedResult.Info.Plot, actualResult.Info.Plot);
                    Assert.AreEqual(expectedResult.Info.Rank, actualResult.Info.Rank);
                    Assert.AreEqual(expectedResult.Info.Rating, actualResult.Info.Rating);
                    Assert.AreEqual(expectedResult.Info.Release_Date, actualResult.Info.Release_Date);
                    Assert.AreEqual(expectedResult.Info.Running_Time_Secs, actualResult.Info.Running_Time_Secs);
                }


            }

        }
    }
}