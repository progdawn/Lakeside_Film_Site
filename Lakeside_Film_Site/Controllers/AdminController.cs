using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lakeside_Film_Site.Models;
using System.Data;
using System.IO;
using Lakeside_Film_Site.Models.ViewModels;
using Lakeside_File_Site.Models;

namespace Lakeside_Film_Site.Controllers
{
    public class AdminController : Controller
    {
        SqlConnection dbcon = new SqlConnection(ConfigurationManager.ConnectionStrings["LakeSideDB"].ConnectionString.ToString());

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MemberList()
        {
            List<Member> memberList;
            try
            {
                dbcon.Open();
                memberList = Member.GetMemberList(dbcon, "");
                dbcon.Close();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            return View(memberList);
        }

        public ActionResult MemberCreate()
        {
            Member mbr = new Member();
            mbr.Avatar = "noname.jpg";
            mbr.Email = "enter value";
            mbr.MemberName = "enter value";
            mbr.PWD = "P@ssword01";
            try
            {
                dbcon.Open();
                int intresult = Member.CUDMember(dbcon, "create", mbr);
                dbcon.Close();
                return RedirectToAction("MemberList", "Admin");
            }
            catch (Exception ex)
            {
                @ViewBag.errormsg = ex.Message;
                if (dbcon != null && dbcon.State == ConnectionState.Open) dbcon.Close();
                return View("error");
            }
        }

        public ActionResult Edit(int? id)
        {
            try
            {
                dbcon.Open();
                Member mem = Member.GetMemberSingle(dbcon, Convert.ToInt32(id));
                dbcon.Close();

                return View(mem);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }

            ViewBag.errormsg = "Invalid data in the Edit Page";
            return View("Error");
        }

        [HttpPost]
        public ActionResult Edit(Member mem, HttpPostedFileBase uploadfile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (uploadfile != null && uploadfile.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(uploadfile.FileName);
                        var path = Path.Combine(
                        Server.MapPath("~/Content/Images/Members"), fileName);
                        uploadfile.SaveAs(path);
                        mem.Avatar = fileName;
                    }
                    if (ModelState.IsValid)
                    {
                        try
                        {
                            dbcon.Open();
                            int intresult = Member.CUDMember(dbcon, "update", mem);
                            dbcon.Close();
                            return RedirectToAction("MemberList", "Admin");
                        }
                        catch (Exception ex) { throw new Exception(ex.Message); }
                    }
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
            }
            ViewBag.errmsg = "Data validation error in Edit method";
            return View("Error");
        }

        public ActionResult FilmList()
        {
            try
            {
                dbcon.Open();
                List<Film> filmlist = Film.GetFilmList(dbcon, "select * from films");
                dbcon.Close();
                return View(filmlist);
            }
            catch (Exception ex)
            {
                @ViewBag.errormsg = ex.Message;
                if (dbcon != null && dbcon.State == ConnectionState.Open) dbcon.Close();
                return View("error");
            }
        }

        public ActionResult FilmCreate()
        {
            Film film = new Film();
            film.Title = "a new film";
            film.YearMade = 0;
            film.Link = "xx";
            film.Imagefile = "xx";
            film.Resources = "zz";
            film.Synopsis = "xx";
            try
            {
                dbcon.Open();
                int intresult = Film.CUFilm(dbcon, "create", film);
                dbcon.Close();
                return RedirectToAction("FilmList", "Admin");
            }
            catch (Exception ex)
            {
                @ViewBag.errormsg = ex.Message;
                if (dbcon != null && dbcon.State == ConnectionState.Open) dbcon.Close();
                return View("error");
            }
        }
        public ActionResult FilmDelete(int id)
        {
            try
            {
                dbcon.Open();
                // int intresult = Film.FilmDelete(dbcon, id);
                dbcon.Close();
                return RedirectToAction("FilmList", "Admin");
            }
            catch (Exception ex)
            {
                @ViewBag.errormsg = ex.Message;
                if (dbcon != null && dbcon.State == ConnectionState.Open) dbcon.Close();
                return View("error");
            }
        }

        public ActionResult FilmEdit(int id)
        {
            try
            {
                List<Film> filmlist = new List<Film>();
                FilmEditVM filmvm = new FilmEditVM();
                dbcon.Open();
                filmlist = Film.GetFilmList(dbcon,
                    "select * from films where filmid = " + id);
                filmvm.film = filmlist.FirstOrDefault();
                filmvm.FilmCatList = CheckModelVM.GetCheckModelList(dbcon, id);
                dbcon.Close();
                return View(filmvm);
            }
            catch (Exception ex)
            {
                @ViewBag.errormsg = ex.Message;
                if (dbcon != null && dbcon.State == ConnectionState.Open) dbcon.Close();
                return View("error");
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult FilmEdit(FormCollection fc, HttpPostedFileBase UploadFile)
        {
            Film film = new Film();
            TryUpdateModel<Film>(film, fc);

            if (UploadFile != null)
            {
                var fileName = Path.GetFileName(UploadFile.FileName);
                var filePath = Server.MapPath("/Content/Images/Films");
                string savedFileName = Path.Combine(filePath, fileName);
                UploadFile.SaveAs(savedFileName);
                film.Imagefile = fileName;
            }
            if (ModelState.IsValid)
            {
                try
                {
                    dbcon.Open();
                    int intresult = Film.CUFilm(dbcon, "update", film);
                    FilmCategory.UpdateCategories(dbcon, fc);
                    dbcon.Close();
                }
                catch (Exception ex)
                {
                    @ViewBag.errormsg = ex.Message;
                    if (dbcon != null && dbcon.State == ConnectionState.Open) dbcon.Close();
                    return View("error");
                }
            }
            return RedirectToAction("FilmList", "Admin");
        }
    }
}