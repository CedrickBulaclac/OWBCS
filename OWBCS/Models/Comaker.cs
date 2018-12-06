using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OWBCS
{
    public class Comaker : BaseProperty<Comaker>
    {
        public int Id { get; set; }
        public string LoanId { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string MemberId { get; set; }
        public string Status { get; set; }
        public Comaker CreateObject(SqlDataReader reader)
        {
            Comaker ret = new Comaker();
            ret.Id = reader.GetInt32(0);
            ret.LoanId = reader.GetString(1);
            ret.Name = reader.GetString(2);
            ret.ContactNo = reader.GetString(3);
            ret.MemberId = reader.GetString(4);
            ret.Status = reader.GetString(5);
            return ret;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}