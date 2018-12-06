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
            const string GET_ALL = "select Id,MemberId,SavingsDate,SavingsAmt,SavingsId,CreatedBy from tbSavings where Deleted=0";
            List<Savings> ret = new List<Savings>();
            SqlCommand cmd = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Savings>(cmd);
            return ret;
        }
        public static List<Savings> GetAll(int id)
        {
            const string GET_ALL = "select Id,MemberId,SavingsDate,SavingsAmt,SavingsId,CreatedBy from tbSavings where MemberId=@MemberId and Deleted=0 ";
            List<Savings> ret = new List<Savings>();
            SqlCommand cmd = new SqlCommand(GET_ALL);
            cmd.Parameters.Add(new SqlParameter("MemberId", id));
            ret = SqlManager.Select<Savings>(cmd);
            return ret;
        }
        public static Savings GetById(int id)
        {
            const string GET_ALL = "select Id,MemberId,SavingsDate,SavingsAmt,SavingsId,CreatedBy from tbSavings where Id=@Id and Deleted=0";
            Savings ret = new Savings();
            SqlCommand cmd = new SqlCommand(GET_ALL);
            cmd.Parameters.Add(new SqlParameter("Id", id));
            ret = SqlManager.Select<Savings>(cmd).First();
            return ret;
        }

        public static bool UpdateStatus(int id)
        {
            const string insert = "update tbSavings set Deleted=1 where Id=@Id";
            SqlCommand ret = new SqlCommand(insert);
            ret.Parameters.Add(new SqlParameter("Id", id));
            return SqlManager.ExecuteNonQuery(ret);
        }

        public static bool Insert(Savings savings)
        {
            const string insert = "insert [tbSavings] (MemberId,SavingsDate,SavingsAmt,SavingsId,CreatedBy,Deleted) values (@MemberId,@SavingsDate,@SavingsAmt,@SavingsId,@CreatedBy,@Deleted) ";
            SqlCommand ret = new SqlCommand(insert);
            ret.Parameters.Add(new SqlParameter("CreatedBy", savings.CreatedBy));
            ret.Parameters.Add(new SqlParameter("MemberId", savings.MemberId));
            ret.Parameters.Add(new SqlParameter("SavingsDate", savings.SavingsDate));
            ret.Parameters.Add(new SqlParameter("SavingsAmt", savings.SavingsAmt));
            ret.Parameters.Add(new SqlParameter("SavingsId", savings.SavingsId));
            ret.Parameters.Add(new SqlParameter("Deleted", savings.Deleted));
            return SqlManager.ExecuteNonQuery(ret);
        }

    }
}