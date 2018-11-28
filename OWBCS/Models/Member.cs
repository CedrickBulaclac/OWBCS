using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OWBCS
{
    public class Member : BaseProperty<Member>
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Mname { get; set; }
        public string Lname { get; set; }
        public int Deleted { get; set; }
        public DateTime Bdate { get; set; }
        public string Email { get; set; }
        public int LoginId { get; set; }
        public string Url { get; set; }
        public decimal Salary { get; set; }

        public Member CreateObject(SqlDataReader reader)
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}