using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Lakeside_Film_Site.Models;

namespace Lakeside_Film_Site.Controllers
{
    public class MemberController : Controller
    {
        SqlConnection dbcon = new SqlConnection(ConfigurationManager.
        ConnectionStrings["lakesidedb"].ConnectionString.ToString());
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
    }
}