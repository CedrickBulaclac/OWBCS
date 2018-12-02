using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OWBCS
{
    public class WitnessControllerSql
    {
        public static List<Witness> GetAll()
        {
            const string GET_ALL = "select Id,LoanId,WitnessName,ContactNo from tbWitness";
            List<Witness> ret = new List<Witness>();
            SqlCommand cmd = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Witness>(cmd);
            return ret;
        }
        public static bool Insert(Witness witness)
        {
            const string insert = "insert [tbWitness] (LoanId,WitnessName,ContactNo) values (@LoanId,@WitnessName,@ContactNo) ";
            SqlCommand ret = new SqlCommand(insert);
            ret.Parameters.Add(new SqlParameter("LoanId", witness.LoanId));
            ret.Parameters.Add(new SqlParameter("WitnessName", witness.WitnessName));
            ret.Parameters.Add(new SqlParameter("ContactNo", witness.ContactNo));
            return SqlManager.ExecuteNonQuery(ret);
        }
    }
}