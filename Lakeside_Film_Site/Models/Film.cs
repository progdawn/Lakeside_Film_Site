using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lakeside.Models
{
    public class Film
    {
        [Required,Key]
        public int FilmID { get; set; }
        [Required,MaxLength(100)]
        public string Title { get; set; }
        [Required, MaxLength(100)]
        public string Link { get; set; }
        [Required]
        public int YearMade { get; set; }
        [Required, MaxLength(30)]
        public string Imagefile { get; set; }
        [Required, MaxLength(1500)]
        public string Synopsis { get; set; }
        [AllowHtml]
        [Required, MaxLength(1000)]
        public string Resources { get; set; }

        public static Film GetFilmSingle(SqlConnection dbcon, int filmid)
        {
            Film obj = new Film();
            string sqlcmd = "select * from films where filmid = " + filmid;
            SqlCommand cmd = new SqlCommand(sqlcmd, dbcon);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr["FilmID"] != DBNull.Value) obj.FilmID = Convert.ToInt32(rdr["FilmID"].ToString());
                if (rdr["Title"] != DBNull.Value) obj.Title = rdr["Title"].ToString();
                if (rdr["Link"] != DBNull.Value) obj.Link = rdr["Link"].ToString();
                if (rdr["YearMade"] != DBNull.Value) obj.YearMade = Convert.ToInt32(rdr["YearMade"].ToString());
                if (rdr["ImageFile"] != DBNull.Value) obj.Imagefile = rdr["Imagefile"].ToString();
                if (rdr["Synopsis"] != DBNull.Value) obj.Synopsis = rdr["Synopsis"].ToString();
                if (rdr["Resources"] != DBNull.Value) obj.Resources = rdr["Resources"].ToString();
            }
            rdr.Close();
            return obj;
        }

        public static List<Film> GetFilmList(SqlConnection dbcon,string sqlcmd)
        {
            List<Film> itemlist = new List<Film>();
            SqlCommand cmd = new SqlCommand(sqlcmd, dbcon);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Film obj = new Film();
                if (rdr["FilmID"] != DBNull.Value) obj.FilmID = Convert.ToInt32(rdr["FilmID"].ToString());
                if (rdr["Title"] != DBNull.Value) obj.Title = rdr["Title"].ToString();
                if (rdr["Link"] != DBNull.Value) obj.Link = rdr["Link"].ToString();
                if (rdr["YearMade"] != DBNull.Value) obj.YearMade = Convert.ToInt32(rdr["YearMade"].ToString());
                if (rdr["ImageFile"] != DBNull.Value) obj.Imagefile = rdr["Imagefile"].ToString();
                if (rdr["Synopsis"] != DBNull.Value) obj.Synopsis = rdr["Synopsis"].ToString();
                if (rdr["Resources"] != DBNull.Value) obj.Resources = rdr["Resources"].ToString();
                itemlist.Add(obj);
            }
            rdr.Close();
            return itemlist;
        }
        public static int CUFilm(SqlConnection dbcon, string CUDAction, Film obj)
        {
            SqlCommand cmd = new SqlCommand();
            if (CUDAction == "create")
            {
                cmd.CommandText = "insert into Films " +
                "(Title,Link,YearMade,Imagefile,Synopsis,Resources) " +
                "Values (@Title,@Link,@YearMade,@Imagefile,@Synopsis,@Resources)";
                cmd.Parameters.AddWithValue("@Title", SqlDbType.VarChar).Value = obj.Title;
                cmd.Parameters.AddWithValue("@Link", SqlDbType.VarChar).Value = obj.Link;
                cmd.Parameters.AddWithValue("@YearMade", SqlDbType.Int).Value = obj.YearMade;
                cmd.Parameters.AddWithValue("@Imagefile", SqlDbType.VarChar).Value = obj.Imagefile;
                cmd.Parameters.AddWithValue("@Synopsis", SqlDbType.VarChar).Value = obj.Synopsis;
                cmd.Parameters.AddWithValue("@Resources", SqlDbType.VarChar).Value = obj.Resources;
            }
            else if (CUDAction == "update")
            {
                cmd.CommandText = "update Films set Title = @Title, Link = @Link, "+
                                  "YearMade = @YearMade, Imagefile = @Imagefile, " +
                                  "Synopsis = @Synopsis, Resources = @Resources " +
                                  "Where FilmID = @FilmID";
                cmd.Parameters.AddWithValue("@Title", SqlDbType.VarChar).Value = obj.Title;
                cmd.Parameters.AddWithValue("@Link", SqlDbType.VarChar).Value = obj.Link;
                cmd.Parameters.AddWithValue("@YearMade", SqlDbType.Int).Value = obj.YearMade;
                cmd.Parameters.AddWithValue("@Imagefile", SqlDbType.VarChar).Value = obj.Imagefile;
                cmd.Parameters.AddWithValue("@Synopsis", SqlDbType.VarChar).Value = obj.Synopsis;
                cmd.Parameters.AddWithValue("@Resources", SqlDbType.VarChar).Value = obj.Resources;
                cmd.Parameters.AddWithValue("@FilmID", SqlDbType.Int).Value = obj.FilmID;
            }
            cmd.Connection = dbcon;
            int intResult = cmd.ExecuteNonQuery();
            cmd.Dispose();
            return intResult;
        }

        public static int FilmDelete(SqlConnection dbcon, int id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "delete reviews where FilmID = @FilmID";
            cmd.Parameters.AddWithValue("@FilmID", SqlDbType.Int).Value = id;
            cmd.Connection = dbcon;
            int intResult = cmd.ExecuteNonQuery();

            cmd.CommandText = "delete filmcategories where FilmID = @FilmID";
            intResult = cmd.ExecuteNonQuery();

            cmd.CommandText = "delete films where FilmID = @FilmID";
            intResult = cmd.ExecuteNonQuery();
            cmd.Dispose();
            return 1;
        }
    }
}