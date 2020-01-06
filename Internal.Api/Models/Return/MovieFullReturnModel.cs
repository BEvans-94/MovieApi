using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Internal.Api.Models.Return
{
    public class MovieFullReturnModel
    {
        public int Year { get; set; }

        public string Title { get; set; }

        public MovieInfoFullReturnModel Info { get; set; }

    }
}