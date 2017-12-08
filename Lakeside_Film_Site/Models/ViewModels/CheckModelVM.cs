using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Lakeside_Film_Site.Models.ViewModels
{
    public class CheckModelVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }

        //    public static List<CheckModelVM>GetCheckModelList(SqlConnection dbcon, int id)
        //    {
        //        List<CheckModelVM> itemlist = new List<CheckModelVM>();
        //        string strsql = "select * from checkmodelview1 where filmid = " +
        //        id + " order by 2";
        //        SqlCommand cmd = new SqlCommand(strsql, dbcon);
        //        SqlDataReader rdr;
        //        rdr = cmd.ExecuteReader();
        //        while (rdr.Read())
        //        {
        //            CheckModelVM obj = new CheckModelVM();
        //            obj.ID = Convert.ToInt32(rdr["Categoryid"].ToString());
        //            obj.Name = rdr["CategoryName"].ToString();
        //            if ((int)rdr["Checked"] == 1) obj.Checked = true;
        //            else obj.Checked = false;
        //            itemlist.Add(obj);
        //        }
        //        rdr.Close();
        //        return itemlist;
        //    }
        //}

        //For programmers who ain't good at SQL
        public static List<CheckModelVM> GetCheckModelList2(SqlConnection dbcon, int id)
        {
            List<CheckModelVM> itemlist = new List<CheckModelVM>();
            string strsql = "select * from categories";
            SqlCommand cmd = new SqlCommand(strsql, dbcon);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                CheckModelVM obj = new CheckModelVM();
                obj.ID = Convert.ToInt32(rdr["Categoryid"].ToString());
                obj.Name = rdr["CategoryName"].ToString();
                obj.Checked = false;
                itemlist.Add(obj);
            }
            rdr.Close();
            strsql = "select * from filmcategories where filmid = " + id;
            cmd.CommandText = strsql;
            rdr = cmd.ExecuteReader();
            int catid = 0, intx = 0;
            while (rdr.Read())
            {
                catid = Convert.ToInt32(rdr["Categoryid"].ToString());
                intx = itemlist.FindIndex(c => c.ID == catid);
                itemlist[intx].Checked = true;
            }
            rdr.Close();
            cmd.Dispose();
            return itemlist;
        }
    }
}