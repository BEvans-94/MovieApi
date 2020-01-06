using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Internal.Api.Models.Return
{
    public class MovieInfoFullReturnModel
    {
        public List<string> Directors { get; set; }

        public DateTime Release_Date { get; set; }

        public decimal Rating { get; set; }

        public List<string> Genres { get; set; }

        public string Image_Url { get; set; }

        public string Plot { get; set; }

        public int Rank { get; set; }

        public int Running_Time_Secs { get; set; }

        public List<string> Actors { get; set; }
    }
}