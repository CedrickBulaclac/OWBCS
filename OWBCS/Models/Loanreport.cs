using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OWBCS
{
    public class LoanReport : BaseProperty<LoanReport>
    {
        public string Fullname { get; set; }
        public int Id { get; set; }
        public string MemberId { get; set; }
        public string LoanId { get; set; }
        public string Status { get; set; }
        public decimal LoanAmt { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ApprovalDate { get; set; }
        public int InterestRate { get; set; }
        public int Terms { get; set; }
        public decimal TotalPaymentwInterest { get; set; }
        public string ApprovedBy { get; set; }
        public LoanReport CreateObject(SqlDataReader reader)
        {
            LoanReport ret = new LoanReport();
            ret.Fullname = reader.GetString(0);
            ret.Id = reader.GetInt32(1);
            ret.MemberId = reader.GetString(2);
            ret.LoanId = reader.GetString(3);
            ret.Status = reader.GetString(4);
            ret.LoanAmt = reader.GetDecimal(5);
            ret.CreatedDate = reader.GetDateTime(6);
            ret.ApprovalDate = reader.GetDateTime(7);
            ret.InterestRate = reader.GetInt32(8);
            ret.Terms = reader.GetInt32(9);
            ret.TotalPaymentwInterest = reader.GetDecimal(10);
            ret.ApprovedBy = reader.GetString(11);
            return ret;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}