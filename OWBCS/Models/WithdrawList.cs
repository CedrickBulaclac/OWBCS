using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OWBCS
{
    public class WithdrawList : BaseProperty<WithdrawList>
    {
        public int Id { get; set; }
        public int MemberId {get;set;}
        public decimal WithdrawAmt { get; set; }
        public decimal Savings { get; set; }
        public string Status { get; set; }
        public string ApprovedBy { get; set; }
        public WithdrawList CreateObject(SqlDataReader reader)
        {
            WithdrawList ret = new WithdrawList();
            ret.Id = reader.GetInt32(0);
            ret.MemberId = reader.GetInt32(1);
            ret.WithdrawAmt = reader.GetDecimal(2);
            ret.Savings = reader.GetDecimal(3);
            ret.Status = reader.GetString(4);
            ret.ApprovedBy = reader.GetString(5);
            return ret;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}