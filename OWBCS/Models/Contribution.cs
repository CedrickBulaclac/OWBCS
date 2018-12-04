using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OWBCS
{
    public class Contribution:BaseProperty<Contribution>
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public DateTime DateContributed { get; set; }
        public decimal ContributionAmt { get; set; }
        public string ContributionId { get; set; }
        public Contribution CreateObject(SqlDataReader reader)
        {
            Contribution ret = new Contribution();
            ret.Id = reader.GetInt32(0);
            ret.MemberId = reader.GetInt32(1);
            ret.DateContributed = reader.GetDateTime(2);
            ret.ContributionAmt = reader.GetDecimal(3);
            ret.ContributionId = reader.GetString(4);
            return ret;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}