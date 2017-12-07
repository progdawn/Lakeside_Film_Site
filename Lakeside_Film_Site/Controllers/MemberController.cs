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

        public ActionResult Edit(String id = "")
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

        public ActionResult Delete(String id)
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
        public ActionResult Delete(string id, FormCollection fc)
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
    }
}