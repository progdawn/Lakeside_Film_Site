using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Lakeside_File_Site.Models;

namespace Lakeside_Film_Site.Models.ViewModels
{
    public class FilmListVM
    {
        public int selectedcatid { get; set; }
        public List<Film> films { get; set; }
        public IEnumerable<SelectListItem> catlist { get; set; }
    }
}