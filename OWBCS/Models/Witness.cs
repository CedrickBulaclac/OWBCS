using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OWBCS
{
    public class Witness:BaseProperty<Witness>
    {
        public int Id { get; set; }
        public string LoanId { get; set; }
        public string WitnessName { get; set; }

        public Witness CreateObject(SqlDataReader reader)
        {
            Witness ret = new Witness();
            ret.Id = reader.GetInt32(0);
            ret.LoanId = reader.GetString(1);
            ret.WitnessName = reader.GetString(2);
            return ret;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}