using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OWBCS
{
    public class LoanControllerSql
    {
        public static List<Loan> GetAll()
        {
            const string GET_ALL = "select Id,MemberId,LoanId,Status,LoanAmt,CreatedDate,ApprovalDate,InterestRate,Terms,TotalPaymentwInterest from tbLoan";
            List<Loan> ret = new List<Loan>();
            SqlCommand cmd = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Loan>(cmd);
            return ret;
        }
        public static List<Loan> GetAll(string id)
        {
            const string GET_ALL = "select Id,MemberId,LoanId,Status,LoanAmt,CreatedDate,ApprovalDate,InterestRate,Terms,TotalPaymentwInterest from tbLoan where MemberId=@MemberId and Status='Pending'";
            List<Loan> ret = new List<Loan>();        
            SqlCommand cmd = new SqlCommand(GET_ALL);
            cmd.Parameters.Add(new SqlParameter("MemberId", id));
            ret = SqlManager.Select<Loan>(cmd);
            return ret;
        }
        public static bool Insert(Loan loan)
        {
            const string insert = "insert [tbLoan] (MemberId,LoanId,Status,LoanAmt,CreatedDate,ApprovalDate,InterestRate,Terms,TotalPaymentwInterest) values (@MemberId ,@LoanId,@Status,@LoanAmt,@CreatedDate,@ApprovalDate,@InterestRate,@Terms,@TotalPaymentwInterest) ";
            SqlCommand ret = new SqlCommand(insert);
            ret.Parameters.Add(new SqlParameter("MemberId", loan.MemberId));
            ret.Parameters.Add(new SqlParameter("LoanId", loan.LoanId));
            ret.Parameters.Add(new SqlParameter("Status", loan.Status));
            ret.Parameters.Add(new SqlParameter("LoanAmt", loan.LoanAmt));
            ret.Parameters.Add(new SqlParameter("CreatedDate", loan.CreatedDate));
            ret.Parameters.Add(new SqlParameter("ApprovalDate", loan.ApprovalDate));
            ret.Parameters.Add(new SqlParameter("InterestRate", loan.InterestRate));
            ret.Parameters.Add(new SqlParameter("Terms", loan.Terms));
            ret.Parameters.Add(new SqlParameter("TotalPaymentwInterest", loan.TotalPaymentwInterest));

            return SqlManager.ExecuteNonQuery(ret);
        }
    }
}