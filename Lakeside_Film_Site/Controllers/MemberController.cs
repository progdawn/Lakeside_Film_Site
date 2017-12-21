using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Lakeside_Film_Site.Models;
using System.IO;
using Lakeside_Film_Site.Models.ViewModels;

namespace Lakeside_Film_Site.Controllers
{
    public class MemberController : Controller
    {
        SqlConnection dbcon = new SqlConnection(ConfigurationManager.ConnectionStrings["LakeSideDB"].ConnectionString.ToString());

        public ActionResult MyProfile()
        {
            try
            {
                int mbrid = (int)Session["memberid"];
                dbcon.Open();
                Member mbr = Member.GetMemberSingle(dbcon, mbrid);
                dbcon.Close();
                return View(mbr);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public ActionResult Create()
        {
            Member mem = new Member();
            mem.Avatar = "nopic.jpg";
            return View(mem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Member mem, HttpPostedFileBase uploadfile)
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
                            int mbrid = (int)Session["memberid"];
                            mem.MemberID = mbrid;
                            int intresult = Member.CUDMember(dbcon, "create", mem);
                            dbcon.Close();
                            return RedirectToAction("Index");
                        }
                        catch (Exception ex) { throw new Exception(ex.Message); }
                    }
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
            }
            ViewBag.errmsg = "Data validation error in Edit method";
            return View("Error");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult MyProfile(Member mem, HttpPostedFileBase uploadfile)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            if (uploadfile != null && uploadfile.ContentLength > 0)
        //            {
        //                var fileName = Path.GetFileName(uploadfile.FileName);
        //                var path = Path.Combine(
        //                Server.MapPath("~/Content/Images/Members"), fileName);
        //                uploadfile.SaveAs(path);
        //                mem.Avatar = fileName;
        //            }
        //            if (ModelState.IsValid)
        //            {
        //                try
        //                {
        //                    dbcon.Open();
        //                    int intresult = Member.CUDMember(dbcon, "update", mem);
        //                    dbcon.Close();
        //                    return RedirectToAction("Index");
        //                }
        //                catch (Exception ex) { throw new Exception(ex.Message); }
        //            }
        //        }
        //        catch (Exception ex) { throw new Exception(ex.Message); }
        //    }
        //    ViewBag.errmsg = "Data validation error in Edit method";
        //    return View("Error");
        //}

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
        [ValidateAntiForgeryToken]
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
                            return RedirectToAction("Index");
                        }
                        catch (Exception ex) { throw new Exception(ex.Message); }
                    }
                }
                catch (Exception ex) { throw new Exception(ex.Message); }
            }
            ViewBag.errmsg = "Data validation error in Edit method";
            return View("Error");
        }

        public ActionResult Delete(int? id)
        {
            try
            {
                dbcon.Open();
                Member mem = Member.GetMemberSingle(dbcon, Convert.ToInt32(id));
                dbcon.Close();
                if (mem.MemberID.ToString() != null)
                    return View(mem);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            ViewBag.errormsg = "Invalid data in the Delete Page";
            return View("Error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id, FormCollection fc)
        {
            Member mem = new Models.Member();
            mem.MemberID = Convert.ToInt32(id);

            try
            {
                dbcon.Open();
                int intresult = Member.CUDMember(dbcon, "delete", mem);
                dbcon.Close();
                return RedirectToAction("Index");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public ActionResult MyReview(int? id)
        {
            int mbrid = (int)Session["memberid"];
            try
            {
                ReviewVM rvm = new ReviewVM();
                if (dbcon.State == ConnectionState.Closed) dbcon.Open();
                string sqlwhere = " select * from films order by title";
                rvm.filmlist = Film.GetFilmList(dbcon, sqlwhere).Select(u => new SelectListItem
                { Text = u.Title, Value = u.FilmID.ToString() });
                if (id != null) rvm.SelectedFilmId = Convert.ToInt32(id.ToString());
                else rvm.SelectedFilmId = Convert.ToInt32(rvm.filmlist.ToList()[0].Value);
                if (rvm.filmlist.Count(x => x.Value == rvm.SelectedFilmId.ToString()) == 1)
                {
                    rvm.review = Review.GetReviewSingle(dbcon, rvm.SelectedFilmId, mbrid);
                    dbcon.Close();
                    return View(rvm);
                }
                @ViewBag.errormsg = "Invalid parameter";
            }
            catch (Exception ex) { @ViewBag.errormsg = ex.Message; }
            if (dbcon != null && dbcon.State == ConnectionState.Open) dbcon.Close();
            return View("error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MyReview(Review review, string btnaction)
        {
            if (ModelState.IsValid)
            {
                review.MemberID = (int)Session["memberid"];
                review.ReviewDate = DateTime.Now;
                try
                {
                    if (dbcon.State == ConnectionState.Closed) dbcon.Open();
                    btnaction = btnaction.ToLower();
                    int intresult = Review.CUDReview(dbcon, btnaction, review);
                }
                catch (Exception ex)
                {
                    @ViewBag.errormsg = ex.Message;
                    if (dbcon != null && dbcon.State == ConnectionState.Open) dbcon.Close();
                    return View("error");
                }
            }
            return RedirectToAction("MyReview", "Member", new { id = review.FilmID });
        }
    }
}