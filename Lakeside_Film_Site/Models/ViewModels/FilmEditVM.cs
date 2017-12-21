using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lakeside_File_Site.Models;

namespace Lakeside_Film_Site.Models.ViewModels
{
    public class FilmEditVM
    {
        public Film film { get; set; }
        public List<CheckModelVM> FilmCatList { get; set; }
    }
}