using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Lakeside_Film_Site.Models
{
    public class Member
    {
        
    [Required, Key]
        public int MemberID { get; set; }
        public string Email { get; set; }
        public string PWD { get; set; }
        public string MemberName { get; set; }
        public string Avatar { get; set; }
        public int Admin { get; set; }
        public static Member GetMemberSingle(SqlConnection dbcon, int id)
        {
            Member obj = new Member();
            string strsql = "select * from Members where MemberID = " + id;
            SqlCommand cmd = new SqlCommand(strsql, dbcon);
            SqlDataReader myReader;
            myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {
                obj.MemberID = Convert.ToInt32(myReader["MemberID"].ToString());
                obj.Email = myReader["Email"].ToString();
                obj.PWD = myReader["PWD"].ToString();
                obj.MemberName = myReader["MemberName"].ToString();
                obj.Avatar = myReader["Avatar"].ToString();
                obj.Admin = Convert.ToInt32(myReader["Admin"].ToString());
            }
            myReader.Close();
            return obj;
        }
    }
}