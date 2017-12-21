using Lakeside_File_Site.Models;
using Lakeside_Film_Site.Models;
using Lakeside_Film_Site.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lakeside_Film_Site.Controllers
{
    public class FilmController : Controller
    {
        SqlConnection dbcon = new SqlConnection(ConfigurationManager.ConnectionStrings["LakeSideDB"].ConnectionString.ToString());

        // GET: Film
        public ActionResult FilmList(int id = 0)
        {
            FilmListVM filmvm = new FilmListVM();
            try
            {
                dbcon.Open();
                filmvm.catlist = Category.GetCategoriesDDList(dbcon);
                if (id == 0) id = Convert.ToInt32(filmvm.catlist.ToList()[0].Value);
                //if (filmvm.catlist.Count(a => a.Value == id.ToString()) == 1)
                if (id >= 1 && id <= 4)
                {
                    filmvm.selectedcatid = id;
                    string sqlcmd = "select films.* from films,FilmCategories Where " +
                    "films.FilmID = FilmCategories.filmid " +
                    "and categoryid = " + filmvm.selectedcatid;
                    filmvm.films = Film.GetFilmList(dbcon, sqlcmd);
                    dbcon.Close();
                    return View(filmvm);
                }
                dbcon.Close();
                @ViewBag.errormsg = "Invalid data in FilmList module";
            }
            catch (Exception ex)
            {
                @ViewBag.errormsg = ex.Message;
                if (dbcon != null && dbcon.State == ConnectionState.Open) dbcon.Close();
            }
            return View("error");
        }
    }
}