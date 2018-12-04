using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OWBCS
{
    public class Savings : BaseProperty<Savings>
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public DateTime SavingsDate { get; set; }
        public decimal SavingsAmt { get; set; }
        public string SavingsId { get; set; }

        public Savings CreateObject(SqlDataReader reader)
        {
            Savings ret = new Savings();
            ret.Id = reader.GetInt32(0);
            ret.MemberId = reader.GetInt32(1);
            ret.SavingsDate = reader.GetDateTime(2);
            ret.SavingsAmt = reader.GetDecimal(3);
            ret.SavingsId = reader.GetString(4);
            return ret;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}