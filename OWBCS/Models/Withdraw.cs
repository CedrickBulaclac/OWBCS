using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OWBCS
{
    public class Withdraw:BaseProperty<Withdraw>
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string Status { get; set; }
        public DateTime WithdrawDate { get; set; }
        public decimal WithdrawAmt { get; set; }
        public string WithdrawId { get; set; }
        public string ApprovedBy { get; set; }

        public Withdraw CreateObject(SqlDataReader reader)
        {
            Withdraw ret = new Withdraw();
            ret.Id = reader.GetInt32(0);
            ret.MemberId = reader.GetInt32(1);            
            ret.WithdrawDate = reader.GetDateTime(2);
            ret.WithdrawAmt = reader.GetDecimal(3);
            ret.WithdrawId = reader.GetString(4);
            ret.Status = reader.GetString(5);
            ret.ApprovedBy = reader.GetString(6);
            return ret;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}