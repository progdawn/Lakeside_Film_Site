using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lakeside_Film_Site.Models
{
    public class Review
    {
        [Required,Key,Column(Order = 1)]
        public int MemberID { get; set; }
        [Required,Key,Column(Order = 2)]
        public int FilmID { get; set; }
        [Required, Display(Name = "Review Date")] 
        public DateTime ReviewDate { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required,MaxLength(100), Display(Name = "Review Title")]
        [RegularExpression("^[^<>]{2,100}$", ErrorMessage = "Title is not valid")]
        public string ReviewTitle { get; set; }
        [Required, MaxLength(1000), Display(Name = "Full Review")]
        [RegularExpression("^[^<>]{2,1000}$", ErrorMessage = "Review is not valid")]
        public string FullReview { get; set; }

        public static Review GetReviewSingle(SqlConnection dbcon, int filmid,int mbrid)
        {
            Review obj = new Review();
            string strsql = "select * from Reviews where filmid = " + filmid + " and memberid = " + mbrid;
            SqlCommand cmd = new SqlCommand(strsql, dbcon);
            SqlDataReader myReader;
            myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                obj.MemberID = Convert.ToInt32(myReader["MemberID"].ToString());
                obj.FilmID = Convert.ToInt32(myReader["FilmID"].ToString());
                obj.ReviewDate = Convert.ToDateTime(myReader["ReviewDate"].ToString());
                obj.Rating = Convert.ToInt32(myReader["Rating"].ToString());
                obj.ReviewTitle = myReader["ReviewTitle"].ToString();
                obj.FullReview = myReader["FullReview"].ToString();
            }
            myReader.Close();
            return obj;
        }
        public static List<Review> GetReviewList(SqlConnection dbcon)
        {
            List<Review> itemlist = new List<Review>();
            string strsql = "select * from Reviews";
            SqlCommand cmd = new SqlCommand(strsql, dbcon);
            SqlDataReader myReader;
            myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                Review obj = new Review();
                obj.MemberID = Convert.ToInt32(myReader["MemberID"].ToString());
                obj.FilmID = Convert.ToInt32(myReader["FilmID"].ToString());
                obj.ReviewDate = Convert.ToDateTime(myReader["ReviewDate"].ToString());
                obj.Rating = Convert.ToInt32(myReader["Rating"].ToString());
                obj.ReviewTitle = myReader["ReviewTitle"].ToString();
                obj.FullReview = myReader["FullReview"].ToString();
                itemlist.Add(obj);
            }
            myReader.Close();
            return itemlist;
        }
        public static int CUDReview(SqlConnection dbcon, string CUDAction, Review obj)
        {
            SqlCommand cmd = new SqlCommand();
            if (CUDAction == "create")
            {
                cmd.CommandText = "insert into Reviews " +
                "Values (@MemberID,@FilmID,@ReviewDate,@Rating,@ReviewTitle,@FullReview)";
                cmd.Parameters.AddWithValue("@MemberID", SqlDbType.Int).Value = obj.MemberID;
                cmd.Parameters.AddWithValue("@FilmID", SqlDbType.Int).Value = obj.FilmID;
                cmd.Parameters.AddWithValue("@ReviewDate", SqlDbType.Date).Value = obj.ReviewDate;
                cmd.Parameters.AddWithValue("@Rating", SqlDbType.Int).Value = obj.Rating;
                cmd.Parameters.AddWithValue("@ReviewTitle", SqlDbType.VarChar).Value = obj.ReviewTitle;
                cmd.Parameters.AddWithValue("@FullReview", SqlDbType.VarChar).Value = obj.FullReview;
            }
            else if (CUDAction == "update")
            {
                cmd.CommandText = "update Reviews set ReviewDate = @ReviewDate, Rating = @Rating, " +
                                  "ReviewTitle = @ReviewTitle, FullReview = @FullReview " +
                                  "where MemberID = @MemberID and FilmID = @FilmID";
                cmd.Parameters.AddWithValue("@ReviewDate", SqlDbType.Date).Value = obj.ReviewDate;
                cmd.Parameters.AddWithValue("@Rating", SqlDbType.Int).Value = obj.Rating;
                cmd.Parameters.AddWithValue("@ReviewTitle", SqlDbType.VarChar).Value = obj.ReviewTitle;
                cmd.Parameters.AddWithValue("@FullReview", SqlDbType.VarChar).Value = obj.FullReview;
                cmd.Parameters.AddWithValue("@MemberID", SqlDbType.Int).Value = obj.MemberID;
                cmd.Parameters.AddWithValue("@FilmID", SqlDbType.Int).Value = obj.FilmID;
            }
            else if (CUDAction == "delete")
            {
                cmd.CommandText = "delete Reviews where MemberID = @MemberID and FilmID = @FilmID";
                cmd.Parameters.AddWithValue("@MemberID", SqlDbType.Int).Value = obj.MemberID;
                cmd.Parameters.AddWithValue("@FilmID", SqlDbType.Int).Value = obj.FilmID;
            }
            cmd.Connection = dbcon;
            int intResult = cmd.ExecuteNonQuery();
            cmd.Dispose();
            return intResult;
        }
    }
}