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
            const string GET_ALL = "select Id,LoanId,Name,ContactNo,MemberId from tbComaker";
            List<Comaker> ret = new List<Comaker>();
            SqlCommand cmd = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Comaker>(cmd);
            return ret;
        }
        public static bool Insert(Comaker comaker)
        {
            const string insert = "insert [tbComaker] (Id,LoanId,Name,ContactNo,MemberId) values (@Id,@LoanId,@Name,@ContactNo,@MemberId) ";
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