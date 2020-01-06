using System;
using System.Collections.Generic;
using System.Text;

namespace Internal.Models.Domain
{
    public class Movie
    {
        public int Year { get; set; }

        public string Title { get; set; }

        public Domain.MovieInfo Info { get; set; }
    }
}
