using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace Lakeside.Models
{
    public class CheckModelVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }
        public static List<CheckModelVM> GetCheckModelList(SqlConnection dbcon, int id)
        {
            List<CheckModelVM> itemlist = new List<CheckModelVM>();
            string strsql = "select * from checkmodelview1 where filmid = " + id + " order by 2";
            SqlCommand cmd = new SqlCommand(strsql, dbcon);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                CheckModelVM obj = new CheckModelVM();
                obj.ID = Convert.ToInt32(rdr["Categoryid"].ToString());
                obj.Name = rdr["CategoryName"].ToString();
                if ((int)rdr["Checked"] == 1) obj.Checked = true;
                else obj.Checked = false;
                itemlist.Add(obj);
            }
            rdr.Close();
            return itemlist;
        }
    }
}