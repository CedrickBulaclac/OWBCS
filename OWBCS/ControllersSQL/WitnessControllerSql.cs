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
            const string GET_ALL = "select Id,LoanId,WitnessName from tbWitness";
            List<Witness> ret = new List<Witness>();
            SqlCommand cmd = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Witness>(cmd);
            return ret;
        }
        public static List<Witness> GetAll(string lid)
        {
            const string GET_ALL = "select Id,LoanId,WitnessName from tbWitness where LoanId=@LoanId";
            List<Witness> ret = new List<Witness>();
            SqlCommand cmd = new SqlCommand(GET_ALL);
            cmd.Parameters.Add(new SqlParameter("LoanId", lid));
            ret = SqlManager.Select<Witness>(cmd);
            return ret;
        }
        public static bool Insert(Witness witness)
        {
            const string insert = "insert [tbWitness] (LoanId,WitnessName) values (@LoanId,@WitnessName) ";
            SqlCommand ret = new SqlCommand(insert);
            ret.Parameters.Add(new SqlParameter("LoanId", witness.LoanId));
            ret.Parameters.Add(new SqlParameter("WitnessName", witness.WitnessName));
            return SqlManager.ExecuteNonQuery(ret);
        }
    }
}