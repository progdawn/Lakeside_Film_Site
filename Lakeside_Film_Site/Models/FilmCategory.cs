using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Lakeside.Models
{
    public class FilmCategory
    {
        [Required,Key,Column(Order = 1)]
        public int FilmID { get; set; }
        [Required, Key,Column(Order = 2)]
        public int CategoryID { get; set; }

        public static void UpdateCategories(SqlConnection dbcon,FormCollection fc)
        {
            int x, intcnt, catid, filmid;
            filmid = Convert.ToInt32(fc["FilmID"]);
           
            string strsql = "delete from filmcategories where filmid = " + filmid;
            var cmd = new SqlCommand(strsql,dbcon);
            intcnt = cmd.ExecuteNonQuery();
            for (x = 0; x < fc.Count; x++)
            {
                if (fc.Keys[x].Substring(0, 4) == "cat-")
                {
                    catid = Convert.ToInt32(fc.Keys[x].Substring(4));
                    if (fc[x].StartsWith("true"))
                    {
                        strsql = "insert into filmcategories values(" + filmid + "," + catid + ");";
                        cmd.CommandText = strsql;
                        intcnt = cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}