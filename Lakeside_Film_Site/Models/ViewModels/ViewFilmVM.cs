using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lakeside_Film_Site.Models.ViewModels
{
    public class ViewFilmVM
    {
        public Film selectedfilm { get; set; }
        public List<FilmReviewVM> reviewlist { get; set; }
    }
}