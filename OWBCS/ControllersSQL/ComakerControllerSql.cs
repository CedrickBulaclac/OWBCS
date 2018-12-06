using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OWBCS
{
    public class ComakerControllerSql
    {
        public static List<Comaker> GetAll()
        {
            const string GET_ALL = "select Id,LoanId,Name,ContactNo,MemberId,Status from tbComaker";
            List<Comaker> ret = new List<Comaker>();
            SqlCommand cmd = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Comaker>(cmd);
            return ret;
        }
        public static List<Comaker> GetAll(string id)
        {
            const string GET_ALL = "select Id,LoanId,Name,ContactNo,MemberId,Status from tbComaker where LoanId=@LoanId";
            List<Comaker> ret = new List<Comaker>();
            SqlCommand cmd = new SqlCommand(GET_ALL);
            cmd.Parameters.Add(new SqlParameter("LoanId", id));
            ret = SqlManager.Select<Comaker>(cmd);
            return ret;
        }
        public static List<Comaker> GetCo(string id)
        {
            const string GET_ALL = "select Id,LoanId,Name,ContactNo,MemberId,Status from tbComaker where LoanId=@LoanId and Status='Approved' ";
            List<Comaker> ret = new List<Comaker>();
            SqlCommand cmd = new SqlCommand(GET_ALL);
            cmd.Parameters.Add(new SqlParameter("LoanId", id));
            ret = SqlManager.Select<Comaker>(cmd);
            return ret;
        }
        public static bool Update(string status,string id)
        {
            const string insert = "update tbComaker set Status=@Status where LoanId=@Id";
            SqlCommand ret = new SqlCommand(insert);
            ret.Parameters.Add(new SqlParameter("Id", id));
            ret.Parameters.Add(new SqlParameter("Status", status));
          
            return SqlManager.ExecuteNonQuery(ret);
        }
        public static bool Insert(Comaker comaker)
        {
            const string insert = "insert [tbComaker] (LoanId,Name,ContactNo,MemberId,Status) values (@LoanId,@Name,@ContactNo,@MemberId,'Pending') ";
            SqlCommand ret = new SqlCommand(insert);
            ret.Parameters.Add(new SqlParameter("Id", comaker.Id));
            ret.Parameters.Add(new SqlParameter("LoanId", comaker.LoanId));
            ret.Parameters.Add(new SqlParameter("Name", comaker.Name));
            ret.Parameters.Add(new SqlParameter("ContactNo", comaker.ContactNo));
            ret.Parameters.Add(new SqlParameter("MemberId", comaker.MemberId));
            return SqlManager.ExecuteNonQuery(ret);
        }
    }
}