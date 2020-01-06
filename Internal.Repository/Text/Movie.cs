using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Internal.Models;
using Internal.Models.Domain;

namespace Internal.Repository.Text
{
    public class Movie<T> : IMovie<Models.Domain.Movie>
    {
        public Movie Create(Movie obj)
        {
            throw new NotImplementedException();
        }

        public Movie Update(Movie obj)
        {
            throw new NotImplementedException();
        }

        public Movie Delete(Movie obj)
        {
            throw new NotImplementedException();
        }

        public List<Movie> GetAll()
        {
            try
            {
                var byteArray = Properties.Resources.moviedata;
                var json = System.Text.Encoding.Default.GetString(byteArray);

                var results = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.Domain.Movie>>(json);

                return results;
            }
            catch (Exception ex)
            {
                //Log Error
                return null;
            }
        }

        public Movie GetByTitleYear(string title, int year)
        {
            try
            {
                var byteArray = Properties.Resources.moviedata;
                var json = System.Text.Encoding.Default.GetString(byteArray);

                var results = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.Domain.Movie>>(json);

                var result = results.FirstOrDefault(x => x.Title.ToUpper() == title.ToUpper() && x.Year == year);

                return result;
            }
            catch (Exception ex)
            {
                //Log Error
                return null;
            }
        }

        public List<Movie> Search(List<string> searchTerms, Enums.SearchType searchType)
        {
            try
            {
                var byteArray = Properties.Resources.moviedata;
                var json = System.Text.Encoding.Default.GetString(byteArray);

                var results = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.Domain.Movie>>(json);

                switch (searchType)
                {
                    case Enums.SearchType.Year:
                        results = results.Where(x => x.Year.ToString() == searchTerms.FirstOrDefault()).ToList();
                        break;
                    case Enums.SearchType.Title:
                        results = results.Where(x => x.Title.Contains(searchTerms.FirstOrDefault())).ToList();
                        break;
                    case Enums.SearchType.Directors:
                        if (searchTerms.Count > 1) results = results.Where(x => x.Info != null && x.Info.Directors != null && x.Info.Directors.SequenceEqual(searchTerms)).ToList();
                        else results = results.Where(x => x.Info != null && x.Info.Directors != null && x.Info.Directors.Contains(searchTerms.FirstOrDefault())).ToList();
                        break;
                    case Enums.SearchType.ReleaseDate:
                        results = results.Where(x => x.Info.Release_Date == Convert.ToDateTime(searchTerms.FirstOrDefault())).ToList();
                        break;
                    case Enums.SearchType.Rating:
                        results = results.Where(x => x.Info.Rating == Convert.ToDecimal(searchTerms.FirstOrDefault())).ToList();
                        break;
                    case Enums.SearchType.Genres:
                        if (searchTerms.Count > 1) results = results.Where(x => x.Info != null && x.Info.Genres != null && x.Info.Genres.SequenceEqual(searchTerms)).ToList();
                        else results = results.Where(x => x.Info != null && x.Info.Genres != null && x.Info.Genres.Contains(searchTerms.FirstOrDefault())).ToList();
                        break;
                    case Enums.SearchType.Rank: //This makes more sense than someone searching one and equal to a rank.
                        results = results.Where(x => x.Info.Rank <= Convert.ToInt32(searchTerms.FirstOrDefault())).ToList();
                        break;
                    case Enums.SearchType.RunTime: //Same aproach as rank, makes more sense to search for below and equal to a time
                        results = results.Where(x => x.Info.Running_Time_Secs <= Convert.ToInt32(searchTerms.FirstOrDefault())).ToList();
                        break;
                    case Enums.SearchType.Actors: 
                        if (searchTerms.Count > 1) results = results.Where(x => x.Info != null && x.Info.Actors != null && x.Info.Actors.SequenceEqual(searchTerms)).ToList();
                        else results = results.Where(x => x.Info != null && x.Info.Actors != null && x.Info.Actors.Contains(searchTerms.FirstOrDefault())).ToList();
                        break;
                    default:
                        results = null;
                        break;
                }


                return results;
            }
            catch (Exception ex)
            {
                //Log Error
                return null;
            }
        }

    }
}
