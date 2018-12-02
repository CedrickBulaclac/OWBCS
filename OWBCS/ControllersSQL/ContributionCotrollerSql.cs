using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OWBCS
{
    public class ContributionCotrollerSql
    {
        public static List<Contribution> GetAll()
        {
            const string GET_ALL = "select Id,MemberId,DateContributed,ContributionAmt from tbContribution";
            List<Contribution> ret = new List<Contribution>();
            SqlCommand cmd = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Contribution>(cmd);
            return ret;
        }
        public static Contribution GetById(int id)
        {
            const string GET_ALL = "select Id,MemberId,DateContributed,ContributionAmt from tbContribution where Id=@Id";
            Contribution ret = new Contribution();
            SqlCommand cmd = new SqlCommand(GET_ALL);
            cmd.Parameters.Add(new SqlParameter("Id", id));
            ret = SqlManager.Select<Contribution>(cmd).First();
            return ret;
        }
        public static bool Insert(Contribution contribution)
        {
            const string insert = "insert [tbContribution] (Id,MemberId,DateContributed,ContributionAmt) values (@Id,@MemberId,@DateContributed,@ContributionAmt) ";
            SqlCommand ret = new SqlCommand(insert);
            ret.Parameters.Add(new SqlParameter("Id", contribution.Id));
            ret.Parameters.Add(new SqlParameter("MemberId", contribution.MemberId));
            ret.Parameters.Add(new SqlParameter("DateContributed", contribution.DateContributed));
            ret.Parameters.Add(new SqlParameter("ContributionAmt", contribution.ContributionAmt));
            return SqlManager.ExecuteNonQuery(ret);
        }

    }
}