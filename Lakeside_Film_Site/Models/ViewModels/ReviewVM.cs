using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lakeside_Film_Site.Models;

namespace Lakeside_Film_Site.Models.ViewModels
{
    public class ReviewVM
    {
        public Review review {get; set;}
        public IEnumerable<SelectListItem> filmlist { get; set; }
        public int SelectedFilmId {get; set;}
    }
}