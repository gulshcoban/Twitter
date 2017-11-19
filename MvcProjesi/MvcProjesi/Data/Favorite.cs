using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace MvcProjesi.Data
{
    public class Favorite
    {
        public int FavoriteID { get; set; }
        public int TweetID { get; set; }
    }
}