using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lakeside_Film_Site.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (Session["memberid"] == null)
                Session["memberid"] = 1;
            ViewBag.memberid = Session["memberid"];
            return View();
        }

        [HttpPost]
        public ActionResult SetMemberid(int memberid)
        {
            Session["memberid"] = memberid;
            return RedirectToAction("index");
        }

        public ActionResult ShowSessions()
        {
            string strx = "Here are the sessions variables:<br>";
            foreach (string s1 in Session.Keys)
            {
                if (Session[s1] != null)
                    strx = strx + s1 + " = " + Session[s1].ToString() + "<br>";
            }
            ViewBag.SessionValues = strx;
            return View();
        }
    }
}