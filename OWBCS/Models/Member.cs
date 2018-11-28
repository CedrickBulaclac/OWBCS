using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace OWBCS
{
    public class Member : BaseProperty<Member>
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Mname { get; set; }
        public string Lname { get; set; }
        public int Deleted { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Birthdate { get; set; }
        public string EmailAddress { get; set; }
        public int LoginId { get; set; }
        public string EmployeeId { get; set; }
        public string Url { get; set; }
        public decimal SalaryAmt { get; set; }
        public string Position { get; set; }
        public string ResidentialAddress { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string ContactNo { get; set; }
        public string EmergencyContactNo { get; set; }
        public string Salutation { get; set; }
        public string FormattedDate => Birthdate.ToShortDateString();

        public Member CreateObject(SqlDataReader reader)
        {
            Member ret = new Member();
            ret.Id = reader.GetInt32(0);
            ret.LoginId = reader.GetInt32(1);
            ret.EmployeeId = reader.GetString(2);
            ret.Salutation = reader.GetString(3);
            ret.Fname = reader.GetString(4);
            ret.Mname = reader.GetString(5);
            ret.Lname = reader.GetString(6);
            ret.Position = reader.GetString(7);
            ret.ResidentialAddress = reader.GetString(8);
            ret.Gender = reader.GetString(9);
            ret.Birthdate = reader.GetDateTime(10);
            ret.MaritalStatus = reader.GetString(11);
            ret.EmailAddress = reader.GetString(12);
            ret.ContactNo = reader.GetString(13);
            ret.EmergencyContactNo = reader.GetString(14);
            ret.SalaryAmt = reader.GetDecimal(15);
            ret.Deleted = reader.GetInt32(16);
            ret.Url = reader.GetString(17);
            return ret;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}