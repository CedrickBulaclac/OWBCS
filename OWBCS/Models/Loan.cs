using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OWBCS
{
    public class Loan : BaseProperty<Loan>
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string LoanId { get; set; }
        public string Status { get; set; }
        public decimal LoanAmt{get;set;}
        public DateTime CreatedDate { get; set; }
        public DateTime ApprovalDate { get; set; }
        public int InterestRate { get; set; }
        public int Terms { get; set; }
        public decimal TotalPaymentwInterest { get; set; }

        public Loan CreateObject(SqlDataReader reader)
        {
            Loan ret = new Loan();
            ret.Id = reader.GetInt32(0);
            ret.MemberId = reader.GetInt32(1);
            ret.LoanId = reader.GetString(2);
            ret.Status = reader.GetString(3);
            ret.LoanAmt = reader.GetDecimal(4);
            ret.CreatedDate = reader.GetDateTime(5);
            ret.ApprovalDate = reader.GetDateTime(6);
            ret.InterestRate = reader.GetInt32(7);
            ret.Terms = reader.GetInt32(8);
            ret.TotalPaymentwInterest = reader.GetDecimal(9);
            return ret;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}