using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Internal.Api.Testing
{
    [TestClass]
    public class InternalRepository_Tests
    {
        private static Repository.IMovie<Internal.Models.Domain.Movie> movieRepo;

        public static void RunSetup()
        {
            try
            {
                movieRepo = new Repository.Text.Movie<Internal.Models.Domain.Movie>();

                if (movieRepo == null) throw new Exception("Repo null.");
            }
            catch (Exception ex)
            {
                //Maybe log?
                throw new AssertFailedException(string.Format("Setup error. Ex: {0}", ex.Message));
            }
        }


        [TestMethod]
        public void Movie_GetAll_Test()
        {
            RunSetup();

            try
            {
                //Have one movie as an example that we would expect back?
                var expectedResult = new Internal.Models.Domain.Movie()
                {
                    Year = 2006,
                    Title = "Kidulthood",
                    Info = new Internal.Models.Domain.MovieInfo()
                    {
                        Directors = new List<string> { "Menhaj Huda" },
                        Release_Date = Convert.ToDateTime("2006-03-03T00:00:00Z"),
                        Rating = 6.5m,
                        Genres = new List<string> { "Drama" },
                        Image_Url = "https://ia.media-imdb.com/images/M/MV5BMzg2Nzc2MTg0N15BMl5BanBnXkFtZTcwMDc1NzA0Mw@@._V1_SX400_.jpg",
                        Plot = "A day in the life of a group of troubled 15-year-olds growing up in west London.",
                        Rank = 4894,
                        Running_Time_Secs = 5340,
                        Actors = new List<string> { "Aml Ameen", "Red Madrell", "Noel Clarke" }
                    }
                };

                var expectedResults = new List<Internal.Models.Domain.Movie> { expectedResult };
                var actualResults = movieRepo.GetAll();

                //Could do a count on the results but obviously this could change over time?

                AssertMovies(expectedResults, actualResults.Where(x => x.Title == expectedResult.Title && x.Year == expectedResult.Year).ToList());
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void Movie_GetByTitleYear_Test()
        {
            RunSetup();

            try
            {
                var expectedResult = new Internal.Models.Domain.Movie()
                {
                    Year = 2006,
                    Title = "Kidulthood",
                    Info = new Internal.Models.Domain.MovieInfo()
                    {
                        Directors = new List<string> { "Menhaj Huda" },
                        Release_Date = Convert.ToDateTime("2006-03-03T00:00:00Z"),
                        Rating = 6.5m,
                        Genres = new List<string> { "Drama" },
                        Image_Url = "https://ia.media-imdb.com/images/M/MV5BMzg2Nzc2MTg0N15BMl5BanBnXkFtZTcwMDc1NzA0Mw@@._V1_SX400_.jpg",
                        Plot = "A day in the life of a group of troubled 15-year-olds growing up in west London.",
                        Rank = 4894,
                        Running_Time_Secs = 5340,
                        Actors = new List<string> { "Aml Ameen", "Red Madrell", "Noel Clarke" }
                    }
                };

                var expectedResults = new List<Internal.Models.Domain.Movie> { expectedResult };
                var actualResults = new List<Internal.Models.Domain.Movie> { movieRepo.GetByTitleYear(expectedResult.Title, expectedResult.Year) };

                AssertMovies(expectedResults, actualResults);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        //After reflecting this could probably be refactored as I'm only using one model...
        [TestMethod]
        public void Movie_SearchByYear_Test()
        {
            RunSetup();

            try
            {
                var expectedResult = new Internal.Models.Domain.Movie()
                {
                    Year = 1922,
                    Title = "Nosferatu, eine Symphonie des Grauens",
                    Info = new Internal.Models.Domain.MovieInfo()
                    {
                        Directors = new List<string> { "F.W. Murnau" },
                        Release_Date = Convert.ToDateTime("1922-02-17T00:00:00Z"),
                        Rating = 8m,
                        Genres = new List<string> { "Horror" },
                        Image_Url = "https://ia.media-imdb.com/images/M/MV5BMTYyNjY3Nzg4MV5BMl5BanBnXkFtZTcwMzYxMzczMw@@._V1_SX400_.jpg",
                        Plot = "Vampire Count Orlok expresses interest in a new residence and real estate agent Hutter's wife. Silent classic based on the story \"Dracula.\"",
                        Rank = 2993,
                        Running_Time_Secs = 5640,
                        Actors = new List<string> { "Max Schreck", "Greta Schroder", "Ruth Landshoff" }
                    }
                };

                var expectedResults = new List<Internal.Models.Domain.Movie> { expectedResult };
                var actualResults = movieRepo.Search(new List<string> { expectedResult.Year.ToString() }, Internal.Models.Enums.SearchType.Year);

                AssertMovies(expectedResults, actualResults);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void Movie_SearchByTitle_Test()
        {
            RunSetup();

            try
            {
                var expectedResult = new Internal.Models.Domain.Movie()
                {
                    Year = 1922,
                    Title = "Nosferatu, eine Symphonie des Grauens",
                    Info = new Internal.Models.Domain.MovieInfo()
                    {
                        Directors = new List<string> { "F.W. Murnau" },
                        Release_Date = Convert.ToDateTime("1922-02-17T00:00:00Z"),
                        Rating = 8m,
                        Genres = new List<string> { "Horror" },
                        Image_Url = "https://ia.media-imdb.com/images/M/MV5BMTYyNjY3Nzg4MV5BMl5BanBnXkFtZTcwMzYxMzczMw@@._V1_SX400_.jpg",
                        Plot = "Vampire Count Orlok expresses interest in a new residence and real estate agent Hutter's wife. Silent classic based on the story \"Dracula.\"",
                        Rank = 2993,
                        Running_Time_Secs = 5640,
                        Actors = new List<string> { "Max Schreck", "Greta Schroder", "Ruth Landshoff" }
                    }
                };

                var expectedResults = new List<Internal.Models.Domain.Movie> { expectedResult };
                var actualResults = movieRepo.Search(new List<string> { expectedResult.Title }, Internal.Models.Enums.SearchType.Title);

                AssertMovies(expectedResults, actualResults);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void Movie_SearchByDirectors_Test()
        {
            RunSetup();

            try
            {
                var expectedResult = new Internal.Models.Domain.Movie()
                {
                    Year = 1922,
                    Title = "Nosferatu, eine Symphonie des Grauens",
                    Info = new Internal.Models.Domain.MovieInfo()
                    {
                        Directors = new List<string> { "F.W. Murnau" },
                        Release_Date = Convert.ToDateTime("1922-02-17T00:00:00Z"),
                        Rating = 8m,
                        Genres = new List<string> { "Horror" },
                        Image_Url = "https://ia.media-imdb.com/images/M/MV5BMTYyNjY3Nzg4MV5BMl5BanBnXkFtZTcwMzYxMzczMw@@._V1_SX400_.jpg",
                        Plot = "Vampire Count Orlok expresses interest in a new residence and real estate agent Hutter's wife. Silent classic based on the story \"Dracula.\"",
                        Rank = 2993,
                        Running_Time_Secs = 5640,
                        Actors = new List<string> { "Max Schreck", "Greta Schroder", "Ruth Landshoff" }
                    }
                };

                var expectedResults = new List<Internal.Models.Domain.Movie> { expectedResult };
                var actualResults = movieRepo.Search(expectedResult.Info.Directors, Internal.Models.Enums.SearchType.Directors);

                AssertMovies(expectedResults, actualResults);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void Movie_SearchByReleaseDate_Test()
        {
            RunSetup();

            try
            {
                var expectedResult = new Internal.Models.Domain.Movie()
                {
                    Year = 1922,
                    Title = "Nosferatu, eine Symphonie des Grauens",
                    Info = new Internal.Models.Domain.MovieInfo()
                    {
                        Directors = new List<string> { "F.W. Murnau" },
                        Release_Date = Convert.ToDateTime("1922-02-17T00:00:00Z"),
                        Rating = 8m,
                        Genres = new List<string> { "Horror" },
                        Image_Url = "https://ia.media-imdb.com/images/M/MV5BMTYyNjY3Nzg4MV5BMl5BanBnXkFtZTcwMzYxMzczMw@@._V1_SX400_.jpg",
                        Plot = "Vampire Count Orlok expresses interest in a new residence and real estate agent Hutter's wife. Silent classic based on the story \"Dracula.\"",
                        Rank = 2993,
                        Running_Time_Secs = 5640,
                        Actors = new List<string> { "Max Schreck", "Greta Schroder", "Ruth Landshoff" }
                    }
                };

                var expectedResults = new List<Internal.Models.Domain.Movie> { expectedResult };
                var actualResults = movieRepo.Search(new List<string> { expectedResult.Info.Release_Date.ToString() }, Internal.Models.Enums.SearchType.ReleaseDate);

                AssertMovies(expectedResults, actualResults);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void Movie_SearchByRating_Test()
        {
            RunSetup();

            try
            {
                var expectedResult = new Internal.Models.Domain.Movie()
                {
                    Year = 1922,
                    Title = "Nosferatu, eine Symphonie des Grauens",
                    Info = new Internal.Models.Domain.MovieInfo()
                    {
                        Directors = new List<string> { "F.W. Murnau" },
                        Release_Date = Convert.ToDateTime("1922-02-17T00:00:00Z"),
                        Rating = 8m,
                        Genres = new List<string> { "Horror" },
                        Image_Url = "https://ia.media-imdb.com/images/M/MV5BMTYyNjY3Nzg4MV5BMl5BanBnXkFtZTcwMzYxMzczMw@@._V1_SX400_.jpg",
                        Plot = "Vampire Count Orlok expresses interest in a new residence and real estate agent Hutter's wife. Silent classic based on the story \"Dracula.\"",
                        Rank = 2993,
                        Running_Time_Secs = 5640,
                        Actors = new List<string> { "Max Schreck", "Greta Schroder", "Ruth Landshoff" }
                    }
                };

                var expectedResults = new List<Internal.Models.Domain.Movie> { expectedResult };
                var searchResults = movieRepo.Search(new List<string> { expectedResult.Info.Rating.ToString() }, Internal.Models.Enums.SearchType.Rating);

                var actualResults = searchResults.Where(x => x.Title == expectedResult.Title && x.Year == expectedResult.Year).ToList();

                AssertMovies(expectedResults, actualResults);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void Movie_SearchByGenres_Test()
        {
            RunSetup();

            try
            {
                var expectedResult = new Internal.Models.Domain.Movie()
                {
                    Year = 1922,
                    Title = "Nosferatu, eine Symphonie des Grauens",
                    Info = new Internal.Models.Domain.MovieInfo()
                    {
                        Directors = new List<string> { "F.W. Murnau" },
                        Release_Date = Convert.ToDateTime("1922-02-17T00:00:00Z"),
                        Rating = 8m,
                        Genres = new List<string> { "Horror" },
                        Image_Url = "https://ia.media-imdb.com/images/M/MV5BMTYyNjY3Nzg4MV5BMl5BanBnXkFtZTcwMzYxMzczMw@@._V1_SX400_.jpg",
                        Plot = "Vampire Count Orlok expresses interest in a new residence and real estate agent Hutter's wife. Silent classic based on the story \"Dracula.\"",
                        Rank = 2993,
                        Running_Time_Secs = 5640,
                        Actors = new List<string> { "Max Schreck", "Greta Schroder", "Ruth Landshoff" }
                    }
                };

                var expectedResults = new List<Internal.Models.Domain.Movie> { expectedResult };
                var searchResults = movieRepo.Search(expectedResult.Info.Genres, Internal.Models.Enums.SearchType.Genres);

                var actualResults = searchResults.Where(x => x.Title == expectedResult.Title && x.Year == expectedResult.Year).ToList();

                AssertMovies(expectedResults, actualResults);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void Movie_SearchByRank_Test()
        {
            RunSetup();

            try
            {
                var expectedResult = new Internal.Models.Domain.Movie()
                {
                    Year = 1922,
                    Title = "Nosferatu, eine Symphonie des Grauens",
                    Info = new Internal.Models.Domain.MovieInfo()
                    {
                        Directors = new List<string> { "F.W. Murnau" },
                        Release_Date = Convert.ToDateTime("1922-02-17T00:00:00Z"),
                        Rating = 8m,
                        Genres = new List<string> { "Horror" },
                        Image_Url = "https://ia.media-imdb.com/images/M/MV5BMTYyNjY3Nzg4MV5BMl5BanBnXkFtZTcwMzYxMzczMw@@._V1_SX400_.jpg",
                        Plot = "Vampire Count Orlok expresses interest in a new residence and real estate agent Hutter's wife. Silent classic based on the story \"Dracula.\"",
                        Rank = 2993,
                        Running_Time_Secs = 5640,
                        Actors = new List<string> { "Max Schreck", "Greta Schroder", "Ruth Landshoff" }
                    }
                };

                var expectedResults = new List<Internal.Models.Domain.Movie> { expectedResult };
                var searchResults = movieRepo.Search(new List<string> { expectedResult.Info.Rank.ToString() }, Internal.Models.Enums.SearchType.Rank);

                var actualResults = searchResults.Where(x => x.Title == expectedResult.Title && x.Year == expectedResult.Year).ToList();

                AssertMovies(expectedResults, actualResults);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void Movie_SearchByRuntime_Test()
        {
            RunSetup();

            try
            {
                var expectedResult = new Internal.Models.Domain.Movie()
                {
                    Year = 1922,
                    Title = "Nosferatu, eine Symphonie des Grauens",
                    Info = new Internal.Models.Domain.MovieInfo()
                    {
                        Directors = new List<string> { "F.W. Murnau" },
                        Release_Date = Convert.ToDateTime("1922-02-17T00:00:00Z"),
                        Rating = 8m,
                        Genres = new List<string> { "Horror" },
                        Image_Url = "https://ia.media-imdb.com/images/M/MV5BMTYyNjY3Nzg4MV5BMl5BanBnXkFtZTcwMzYxMzczMw@@._V1_SX400_.jpg",
                        Plot = "Vampire Count Orlok expresses interest in a new residence and real estate agent Hutter's wife. Silent classic based on the story \"Dracula.\"",
                        Rank = 2993,
                        Running_Time_Secs = 5640,
                        Actors = new List<string> { "Max Schreck", "Greta Schroder", "Ruth Landshoff" }
                    }
                };

                var expectedResults = new List<Internal.Models.Domain.Movie> { expectedResult };
                var searchResults = movieRepo.Search(new List<string> { expectedResult.Info.Running_Time_Secs.ToString() }, Internal.Models.Enums.SearchType.RunTime);

                var actualResults = searchResults.Where(x => x.Title == expectedResult.Title && x.Year == expectedResult.Year).ToList();

                AssertMovies(expectedResults, actualResults);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void Movie_SearchByActors_Test()
        {
            RunSetup();

            try
            {
                var expectedResult = new Internal.Models.Domain.Movie()
                {
                    Year = 1922,
                    Title = "Nosferatu, eine Symphonie des Grauens",
                    Info = new Internal.Models.Domain.MovieInfo()
                    {
                        Directors = new List<string> { "F.W. Murnau" },
                        Release_Date = Convert.ToDateTime("1922-02-17T00:00:00Z"),
                        Rating = 8m,
                        Genres = new List<string> { "Horror" },
                        Image_Url = "https://ia.media-imdb.com/images/M/MV5BMTYyNjY3Nzg4MV5BMl5BanBnXkFtZTcwMzYxMzczMw@@._V1_SX400_.jpg",
                        Plot = "Vampire Count Orlok expresses interest in a new residence and real estate agent Hutter's wife. Silent classic based on the story \"Dracula.\"",
                        Rank = 2993,
                        Running_Time_Secs = 5640,
                        Actors = new List<string> { "Max Schreck", "Greta Schroder", "Ruth Landshoff" }
                    }
                };

                var expectedResults = new List<Internal.Models.Domain.Movie> { expectedResult };
                var searchResults = movieRepo.Search(expectedResult.Info.Actors, Internal.Models.Enums.SearchType.Actors);

                var actualResults = searchResults.Where(x => x.Title == expectedResult.Title && x.Year == expectedResult.Year).ToList();

                AssertMovies(expectedResults, actualResults);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


        public void AssertMovies(List<Internal.Models.Domain.Movie> expectedResults, List<Internal.Models.Domain.Movie> actualResults)
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