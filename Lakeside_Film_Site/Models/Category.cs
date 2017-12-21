using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Lakeside_Film_Site.Models
{
    public class Category
    {
        [Required, Key]
        public int Categoryid { get; set; }
        [Required, MaxLength(20)]
        public string CategoryName { get; set; }

        public static IEnumerable<SelectListItem> GetCategoriesDDList(SqlConnection dbcon)
        {
            IList<SelectListItem> ddlist = new List<SelectListItem>();
            string strsql = "select * from categories";
            SqlCommand cmd = new SqlCommand(strsql, dbcon);
            SqlDataReader myReader;
            myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                ddlist.Add(new SelectListItem()
                {
                    Text = myReader["CategoryName"].ToString(),
                    Value = myReader["Categoryid"].ToString()
                });
            }
            myReader.Close();
            return ddlist;
        }
    }
}