using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OWBCS
{
    public class WithdrawListControllerSql
    {
        public static List<WithdrawList> GetAll(string status)
        {
            const string GET_ALL = "select Id,MemberId,WithdrawAmt,Savings=(select SUM(ContributionAmt) from tbContribution where MemberId=w.MemberId),Status,ApprovedBy from tbWithdraw w where Status=@Status";
            List<WithdrawList> ret = new List<WithdrawList>();
            SqlCommand cmd = new SqlCommand(GET_ALL);
            cmd.Parameters.Add(new SqlParameter("Status", status));
            ret = SqlManager.Select<WithdrawList>(cmd);
            return ret;
        }
    }
}