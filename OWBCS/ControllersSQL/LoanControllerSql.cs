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
            const string GET_ALL = "select Id,MemberId,LoanId,Status,LoanAmt,Convert(varchar(11),CreatedDate,100) as CreatedDate,Convert(varchar(11),ApprovalDate,100) as ApprovalDate,InterestRate,Terms,TotalPaymentwInterest,ApprovedBy from tbLoan";
            List<Loan> ret = new List<Loan>();
            SqlCommand cmd = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Loan>(cmd);
            return ret;
        }
        public static List<Loan> GetAll(string id)
        {
            const string GET_ALL = "select Id,MemberId,LoanId,Status,LoanAmt,Convert(varchar(11),CreatedDate,100) as CreatedDate,Convert(varchar(11),ApprovalDate,100) as ApprovalDate,InterestRate,Terms,TotalPaymentwInterest,ApprovedBy from tbLoan where MemberId=@MemberId and Status='Pending'";
            List<Loan> ret = new List<Loan>();        
            SqlCommand cmd = new SqlCommand(GET_ALL);
            cmd.Parameters.Add(new SqlParameter("MemberId", id));
            ret = SqlManager.Select<Loan>(cmd);
            return ret;
        }
        public static List<Loan> GetStatus(string status)
        {
            const string GET_ALL = "select Id,MemberId,LoanId,Status,LoanAmt,Convert(varchar(11),CreatedDate,100) as CreatedDate,Convert(varchar(11),ApprovalDate,100) as ApprovalDate,InterestRate,Terms,TotalPaymentwInterest,ApprovedBy from tbLoan where Status=@Status";
            List<Loan> ret = new List<Loan>();
            SqlCommand cmd = new SqlCommand(GET_ALL);
            cmd.Parameters.Add(new SqlParameter("Status", status));
            ret = SqlManager.Select<Loan>(cmd);
            return ret;
        }
        public static Loan Get(string id)
        {
            const string GET_ALL = "select Id,MemberId,LoanId,Status,LoanAmt,Convert(varchar(11),CreatedDate,100) as CreatedDate,Convert(varchar(11),ApprovalDate,100) as ApprovalDate,InterestRate,Terms,TotalPaymentwInterest,ApprovedBy from tbLoan where MemberId=@MemberId and Status='Pending'";
            Loan ret = new Loan();
            SqlCommand cmd = new SqlCommand(GET_ALL);
            cmd.Parameters.Add(new SqlParameter("MemberId", id));
            ret = SqlManager.Select<Loan>(cmd).First();
            return ret;
        }
        public static bool UpdateStatus(Loan loan)
        {

            const string insert = "update tbLoan set Status=@Status, ApprovedBy=@ApprovedBy, ApprovedDate=@ApprovedDate  where Id=@Id";
            SqlCommand ret = new SqlCommand(insert);
            ret.Parameters.Add(new SqlParameter("Id", loan.Id));
            ret.Parameters.Add(new SqlParameter("Status", loan.Status));
            ret.Parameters.Add(new SqlParameter("ApprovedBy", loan.ApprovedBy));
            ret.Parameters.Add(new SqlParameter("ApprovedDate", loan.ApprovedBy));
            return SqlManager.ExecuteNonQuery(ret);
        }

        public static bool Insert(Loan loan)
        {
            const string insert = "insert [tbLoan] (MemberId,LoanId,Status,LoanAmt,CreatedDate,ApprovalDate,InterestRate,Terms,TotalPaymentwInterest,ApprovedBy) values (@MemberId ,@LoanId,@Status,@LoanAmt,@CreatedDate,@ApprovalDate,@InterestRate,@Terms,@TotalPaymentwInterest,@ApprovedBy) ";
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
            ret.Parameters.Add(new SqlParameter("ApprovedBy", loan.ApprovedBy));
            return SqlManager.ExecuteNonQuery(ret);
        }
    }
}