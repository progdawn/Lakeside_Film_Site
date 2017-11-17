﻿using System;
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

        public static int CUDMember(SqlConnection dbcon, string CUDAction, Member obj)
        {
            SqlCommand cmd = new SqlCommand();
            if (CUDAction == "create")
            {
                cmd.CommandText = "insert into Members " +
                "Values (@MemberID, @Email, @PWD, " +
                    "@MemberName, @Avatar, " +
                    "@Admin)";

                cmd.Parameters.AddWithValue("@MemberID", SqlDbType.Int).Value = obj.MemberID;
                cmd.Parameters.AddWithValue("@Email", SqlDbType.VarChar).Value = obj.Email;
                cmd.Parameters.AddWithValue("@PWD", SqlDbType.NVarChar).Value = obj.PWD;
                cmd.Parameters.AddWithValue("@MemberName", SqlDbType.VarChar).Value = obj.MemberName;
                cmd.Parameters.AddWithValue("@Avatar", SqlDbType.VarChar).Value = obj.Avatar;
                cmd.Parameters.AddWithValue("@Admin", SqlDbType.Int).Value = obj.Admin;
            }
            else if (CUDAction == "update")
            {
                cmd.CommandText = "update Members set MemberID = @MemberID," +
                    "Email = @Email," +
                    "PWD = @PWD," +
                    "MemberName = @MemberName," +
                    "Avatar = @Avatar," +
                    "Admin = @Admin " +
                "Where MemberID = @MemberID";
                cmd.Parameters.AddWithValue("@MemberID", SqlDbType.Int).Value = obj.MemberID;
                cmd.Parameters.AddWithValue("@Email", SqlDbType.VarChar).Value = obj.Email;
                cmd.Parameters.AddWithValue("@PWD", SqlDbType.NVarChar).Value = obj.PWD;
                cmd.Parameters.AddWithValue("@MemberName", SqlDbType.VarChar).Value = obj.MemberName;
                cmd.Parameters.AddWithValue("@Avatar", SqlDbType.VarChar).Value = obj.Avatar;
                cmd.Parameters.AddWithValue("@Admin", SqlDbType.Int).Value = obj.Admin;
            }
            else if (CUDAction == "delete")
            {
                cmd.CommandText = "delete Members where MemberID = @MemberID";
                cmd.Parameters.AddWithValue("@MemberID", SqlDbType.VarChar).Value = obj.MemberID;
            }
            cmd.Connection = dbcon;
            int intResult = cmd.ExecuteNonQuery();
            cmd.Dispose();
            return intResult;
        }
    }
}