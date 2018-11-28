using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
namespace OWBCS
{
    public class LoginControllerSql
    {
        public static bool Insert(Login usr)
        {
            const string GET_INSERT = @"insert [tbLogin] (Username,Hash,CreatedBy,ModifiedBy,CreatedDate,ModifiedDate,Level,Lockout,LastLogin) values (@Username,@Hash,@CreatedBy,@ModifiedBy,@CreatedDate,@ModifiedDate,@Level,@Lockout,@LastLogin)";
            SqlCommand com = new SqlCommand(GET_INSERT);
            com.Parameters.Add(new SqlParameter("@Username", usr.Username));
            com.Parameters.Add(new SqlParameter("@Hash", usr.Hash));
            com.Parameters.Add(new SqlParameter("@Lockout", usr.Locked));
            com.Parameters.Add(new SqlParameter("@CreatedBy", usr.CreatedBy));
            com.Parameters.Add(new SqlParameter("@ModifiedBy", usr.ModifyBy));
            com.Parameters.Add(new SqlParameter("@Level", usr.Level));
            com.Parameters.Add(new SqlParameter("@ModifiedDate", usr.ModifiedDate));
            com.Parameters.Add(new SqlParameter("@CreatedDate", usr.CreatedDate));
            com.Parameters.Add(new SqlParameter("@LastLogin", usr.LastLogin));
            return SqlManager.ExecuteNonQuery(com);
        }
        public static List<Login> GetAll(string email)
        {
            const string GET_ALL = @"select Id,Username,Hash,CreatedBy,ModifiedBy,CreatedDate,ModifiedDate,Level,Lockout,LastLogin from tbLogin where Username=@email and Lockout=0";
            List<Login> ret = default(List<Login>);
            SqlCommand com = new SqlCommand(GET_ALL);
            com.Parameters.Add(new SqlParameter("@email", email));
            ret = SqlManager.Select<Login>(com);
            return ret;
        }

        public static bool Update(string email, string hash)
        {
            const string GET_UPDATE = @"update [tbLogin] set Hash=@hash where Username=@email";
            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@email", email));
            com.Parameters.Add(new SqlParameter("@hash", hash));
            return SqlManager.ExecuteNonQuery(com);
        }
        public static bool UpdateLog(int id)
        {
            const string GET_UPDATE = @"update [tbLogin] set LastLogin=GETDATE() where Id=@Id";
            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Id", id));

            return SqlManager.ExecuteNonQuery(com);
        }
        public static bool UpdateLockout(int id)
        {
            const string GET_UPDATE = @"update [tbLogin] set Lockout=1 where Id=@Id";
            SqlCommand com = new SqlCommand(GET_UPDATE);
            com.Parameters.Add(new SqlParameter("@Id", id));

            return SqlManager.ExecuteNonQuery(com);
        }
        public static bool Delete(Login usr)
        {
            const string GET_DELETE = @"delete [tbLogin] WHERE Id = @Id";

            SqlCommand com = new SqlCommand(GET_DELETE);
            com.Parameters.Add(new SqlParameter("@Id", usr.Id));

            return SqlManager.ExecuteNonQuery(com);
        }
        public static Login Get(string uid, string uhash)
        {
            Login ret = default(Login);
            try
            {
                const string GET_RECORD = @"SELECT Id,Username,Hash,CreatedBy,ModifiedBy,CreatedDate,ModifiedDate,Level,Lockout,LastLogin FROM [tbLogin] WHERE Username = @Username and Hash=@Hash and Lockout=0";


                SqlCommand com = new SqlCommand(GET_RECORD);
                com.Parameters.Add(new SqlParameter("@Username", uid));
                com.Parameters.Add(new SqlParameter("@Hash", uhash));
                ret = SqlManager.Select<Login>(com).First();

                return ret;
            }
            catch (InvalidOperationException)
            {
                return ret;
            }
        }


    }
}