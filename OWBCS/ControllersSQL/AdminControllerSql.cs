using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
namespace OWBCS
{
    public class AdminControllerSql
    {
        public static List<Admin> GetAll()
        {
            const string GET_ALL = "SELECT [Id],[Fname],[Mname],[Lname],[Bdate] ,[Position],[Email],[Deleted],[Url],[LoginId] FROM [tbAdmin] where Deleted=0";
            List<Admin> ret = new List<Admin>();
            SqlCommand cmd = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Admin>(cmd);
            return ret;
        }
        public static List<Admin> GetAll(string email)
        {
            const string GET_ALL = "SELECT [Id],[Fname],[Mname],[Lname],[Bdate],[Position],[Email],[Deleted],[Url],[LoginId] FROM [tbAdmin] where Email=@Email and Deleted=0";
            List<Admin> ret = new List<Admin>();
            SqlCommand cmd = new SqlCommand(GET_ALL);
            cmd.Parameters.Add(new SqlParameter("Email", email));
            ret = SqlManager.Select<Admin>(cmd);
            return ret;
        }
        public static Admin GetById(int id)
        {
            const string GET_ALL = "SELECT [Id],[Fname],[Mname],[Lname],[Bdate],[Position],[Email],[Deleted],[Url],[LoginId] FROM [tbAdmin] where Id=@Id and Deleted=0";
            Admin ret = new Admin();
            SqlCommand cmd = new SqlCommand(GET_ALL);
            cmd.Parameters.Add(new SqlParameter("Id", id));
            ret = SqlManager.Select<Admin>(cmd).First();
            return ret;
        }
        public static bool UpdateLoginId(Admin admin)
        {
            const string insert = "update tbAdmin set LoginId=@LoginId where Id=@Id ";
            SqlCommand ret = new SqlCommand(insert);
            ret.Parameters.Add(new SqlParameter("LoginId", admin.LoginId));
            ret.Parameters.Add(new SqlParameter("Id", admin.Id));
            return SqlManager.ExecuteNonQuery(ret);
        }
        public static bool Update(Admin admin)
        {
            const string insert = "update [tbAdmin] set Fname=@Fname, Mname=@Mname, Lname=@Lname,Bdate=@Bdate,Position=@Position,Email=@Email where Id=@id ";
            SqlCommand ret = new SqlCommand(insert);
            ret.Parameters.Add(new SqlParameter("Fname", admin.Fname));
            ret.Parameters.Add(new SqlParameter("Mname", admin.Mname));
            ret.Parameters.Add(new SqlParameter("Lname", admin.Lname));
            ret.Parameters.Add(new SqlParameter("Bdate", admin.Bdate));
            ret.Parameters.Add(new SqlParameter("Position", admin.Position));
            ret.Parameters.Add(new SqlParameter("Email", admin.Email));
            ret.Parameters.Add(new SqlParameter("id", admin.Id));

            return SqlManager.ExecuteNonQuery(ret);
        }
        public static bool Delete(Admin admin)
        {
            const string insert = "update tbAdmin set Deleted=1 where Id=@Id ";
            SqlCommand ret = new SqlCommand(insert);
            ret.Parameters.Add(new SqlParameter("Id", admin.Id));
            return SqlManager.ExecuteNonQuery(ret);
        }
        public static bool Insert(Admin admin)
        {
            const string insert = "insert [tbAdmin] (Fname,Mname,Lname,Bdate,Position,Email,Deleted,Url,LoginId) values (@Fname,@Mname,@Lname,@Bdate,@Position,@Email,@Deleted,@Url,@LoginId) ";
            SqlCommand ret = new SqlCommand(insert);
            ret.Parameters.Add(new SqlParameter("Fname", admin.Fname));
            ret.Parameters.Add(new SqlParameter("Mname", admin.Mname));
            ret.Parameters.Add(new SqlParameter("Lname", admin.Lname));
            ret.Parameters.Add(new SqlParameter("Bdate", admin.Bdate));
            ret.Parameters.Add(new SqlParameter("Position", admin.Position));
            ret.Parameters.Add(new SqlParameter("Email", admin.Email));
            ret.Parameters.Add(new SqlParameter("Deleted", admin.Deleted));
            ret.Parameters.Add(new SqlParameter("Url", admin.Url));
            ret.Parameters.Add(new SqlParameter("LoginId", admin.LoginId));
            return SqlManager.ExecuteNonQuery(ret);
        }
    }
}