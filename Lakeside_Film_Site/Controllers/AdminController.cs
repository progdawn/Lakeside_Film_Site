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
    public class AdminController : Controller
    {
        SqlConnection dbcon = new SqlConnection(ConfigurationManager.ConnectionStrings["LakeSideDB"].ConnectionString.ToString());

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
    }
}