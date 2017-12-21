using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Lakeside_Film_Site.Models.ViewModels
{
    public class FilmReviewVM
    {
        public int FilmId { get; set; }
        public string Avatar { get; set; }
        public DateTime ReviewDate { get; set; }
        public int Rating { get; set; }
        public string ReviewTitle { get; set; }
        public string FullReview { get; set; }
        public string MemberName { get; set; }

        public static List<FilmReviewVM> GetFilmReviewVMList(SqlConnection dbcon, int filmid)
        {
            List<FilmReviewVM> itemlist = new List<FilmReviewVM>();
            string strsql = "select * from filmreviews where filmid = " + filmid;
            SqlCommand cmd = new SqlCommand(strsql, dbcon);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                FilmReviewVM obj = new FilmReviewVM();
                obj.FilmId = Convert.ToInt32(rdr["FilmId"].ToString());
                obj.Avatar = rdr["Avatar"].ToString();
                obj.ReviewDate = Convert.ToDateTime(rdr["ReviewDate"].ToString());
                obj.Rating = Convert.ToInt32(rdr["Rating"].ToString());
                obj.ReviewTitle = rdr["ReviewTitle"].ToString();
                obj.FullReview = rdr["FullReview"].ToString();
                obj.MemberName = rdr["MemberName"].ToString();
                itemlist.Add(obj);
            }
            rdr.Close();
            return itemlist;
        }
    }
}