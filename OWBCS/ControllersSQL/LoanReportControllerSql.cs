using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OWBCS
{
    public class LoanReportControllerSql
    {
        public static List<LoanReport> GetAll(int id)
        {
            const string GET_ALL = "select Fname+' '+Mname+' '+Lname as Fullname,l.Id,l.MemberId,l.LoanId,Status,LoanAmt,CreatedDate,ApprovalDate,InterestRate,Terms,TotalPaymentwInterest,ApprovedBy from tbMember m inner join tbLoan l on m.Id=l.MemberId where l.Id=@Id and c.Status='Pending'";
            List<LoanReport> ret = new List<LoanReport>();
            SqlCommand cmd = new SqlCommand(GET_ALL);
            cmd.Parameters.Add(new SqlParameter("Id", id));
            ret = SqlManager.Select<LoanReport>(cmd);
            return ret;
        }
    }
}