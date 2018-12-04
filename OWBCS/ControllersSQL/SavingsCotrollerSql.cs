using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OWBCS
{
    public class SavingsCotrollerSql
    {
        public static List<Savings> GetAll()
        {
            const string GET_ALL = "select Id,MemberId,SavingsDate,SavingsAmt,SavingsId from tbSavings";
            List<Savings> ret = new List<Savings>();
            SqlCommand cmd = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Savings>(cmd);
            return ret;
        }
        public static List<Savings> GetAll(int id)
        {
            const string GET_ALL = "select Id,MemberId,SavingsDate,SavingsAmt,SavingsId from tbSavings where MemberId=@MemberId ";
            List<Savings> ret = new List<Savings>();
            SqlCommand cmd = new SqlCommand(GET_ALL);
            cmd.Parameters.Add(new SqlParameter("MemberId", id));
            ret = SqlManager.Select<Savings>(cmd);
            return ret;
        }
        public static Savings GetById(int id)
        {
            const string GET_ALL = "select Id,MemberId,SavingsDate,SavingsAmt,SavingsId from tbSavings where Id=@Id";
            Savings ret = new Savings();
            SqlCommand cmd = new SqlCommand(GET_ALL);
            cmd.Parameters.Add(new SqlParameter("Id", id));
            ret = SqlManager.Select<Savings>(cmd).First();
            return ret;
        }
        public static bool Insert(Savings savings)
        {
            const string insert = "insert [tbSavings] (MemberId,SavingsDate,SavingsAmt,SavingsId) values (@MemberId,@SavingsDate,@SavingsAmt,@SavingsId) ";
            SqlCommand ret = new SqlCommand(insert);
            ret.Parameters.Add(new SqlParameter("Id", savings.Id));
            ret.Parameters.Add(new SqlParameter("MemberId", savings.MemberId));
            ret.Parameters.Add(new SqlParameter("SavingsDate", savings.SavingsDate));
            ret.Parameters.Add(new SqlParameter("SavingsAmt", savings.SavingsAmt));
            ret.Parameters.Add(new SqlParameter("SavingsId", savings.SavingsId));
            return SqlManager.ExecuteNonQuery(ret);
        }

    }
}