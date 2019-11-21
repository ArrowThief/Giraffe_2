using System;
using System.Collections.Generic;
using System.Text;

namespace Giraffe_2
{
    class movie
    {
        public string title;
        public string director;
        private string rating;
        public static string bob = "bob";

        public movie(string title, string director, string rating)
        {
            this.title = title;
            this.director = director;
            this.Rating = rating;
        }

        public bool Goodforkids()
        {
            if(this.Rating == "R" || this.Rating == "PG-13")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string Rating
        {
            get { return rating; }
            set { 
                  if (value == "G"||value == "PG" || value == "PG-13" || value == "R")
                {
                    rating = value;
                }else
                {
                    rating = "NR";
                }
                }
        }
    }
}
