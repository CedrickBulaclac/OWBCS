using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OWBCS
{
    public class WithdrawControllerSql
    {
        public static List<Withdraw> GetAll()
        {
            const string GET_ALL = "select Id,MemberId,WithdrawDate,WithdrawAmt,WithdrawId,Status,ApprovedBy from tbWithdraw";
            List<Withdraw> ret = new List<Withdraw>();
            SqlCommand cmd = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Withdraw>(cmd);
            return ret;
        }
        public static List<Withdraw> GetAllPending()
        {
            const string GET_ALL = "select Id,MemberId,WithdrawDate,WithdrawAmt,WithdrawId,Status,ApprovedBy from tbWithdraw where Status='Pending'";
            List<Withdraw> ret = new List<Withdraw>();
            SqlCommand cmd = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Withdraw>(cmd);
            return ret;
        }
        public static List<Withdraw> GetAll(int id)
        {
            const string GET_ALL = "select Id,MemberId,WithdrawDate,WithdrawAmt,WithdrawId,Status,ApprovedBy from tbWithdraw where MemberId=@MemberId and Status='Approved'";
            List<Withdraw> ret = new List<Withdraw>();
            SqlCommand cmd = new SqlCommand(GET_ALL);
            cmd.Parameters.Add(new SqlParameter("MemberId", id));
            ret = SqlManager.Select<Withdraw>(cmd);
            return ret;
        }
        public static List<Withdraw> Check(int id)
        {
            const string GET_ALL = "select Id,MemberId,WithdrawDate,WithdrawAmt,WithdrawId,Status,ApprovedBy from tbWithdraw where MemberId=@MemberId and Status='Pending'";
            List<Withdraw> ret = new List<Withdraw>();
            SqlCommand cmd = new SqlCommand(GET_ALL);
            cmd.Parameters.Add(new SqlParameter("MemberId", id));
            ret = SqlManager.Select<Withdraw>(cmd);
            return ret;
        }
        public static bool UpdateStatus(Withdraw withdraw)
        {
            const string insert = "update tbWithdraw set Status=@Status, ApprovedBy=@ApprovedBy where Id=@Id";
            SqlCommand ret = new SqlCommand(insert);
            ret.Parameters.Add(new SqlParameter("Id", withdraw.Id));
            ret.Parameters.Add(new SqlParameter("Status", withdraw.Status));
            ret.Parameters.Add(new SqlParameter("ApprovedBy", withdraw.ApprovedBy));
            return SqlManager.ExecuteNonQuery(ret);
        }
        public static bool Insert(Withdraw withdraw)
        {
            const string insert = "insert [tbWithdraw] (MemberId,WithdrawDate,WithdrawAmt,WithdrawId,Status,ApprovedBy) values (@MemberId,@WithdrawDate,@WithdrawAmt,@WithdrawId,@Status,@ApprovedBy) ";
            SqlCommand ret = new SqlCommand(insert);
            ret.Parameters.Add(new SqlParameter("WithdrawId", withdraw.WithdrawId));
            ret.Parameters.Add(new SqlParameter("MemberId", withdraw.MemberId));
            ret.Parameters.Add(new SqlParameter("WithdrawDate", withdraw.WithdrawDate));
            ret.Parameters.Add(new SqlParameter("WithdrawAmt", withdraw.WithdrawAmt));
            ret.Parameters.Add(new SqlParameter("Status", withdraw.Status));
            ret.Parameters.Add(new SqlParameter("ApprovedBy", withdraw.ApprovedBy));
            return SqlManager.ExecuteNonQuery(ret);
        }
    }
}