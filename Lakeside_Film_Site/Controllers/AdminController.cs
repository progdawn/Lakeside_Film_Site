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
    }
}