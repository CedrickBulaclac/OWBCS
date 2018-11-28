using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace OWBCS
{
    public class Admin : BaseProperty<Admin>
    {
        public int Id { get; set; }
        public string Fname { get; set; }
        public string Mname { get; set; }
        public string Lname { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Bdate { get; set; }
        public string Position { get; set; }
        public string EmailAddress { get; set; }
        public int Deleted { get; set; }
        public string Url { get; set; }
        public int LoginId { get; set; }
        public string ResidentialAddress { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        [DataType(DataType.PhoneNumber, ErrorMessage = "Provided phone number not valid")]
        public string ContactNo { get; set; }
        [DataType(DataType.PhoneNumber, ErrorMessage = "Provided phone number not valid")]
        public string EmergencyNo { get; set; }
        public string AdminId { get; set; }
        public string FormattedDate => Bdate.ToShortDateString();
        public Admin CreateObject(SqlDataReader reader)
        {
            Admin ret = new Admin();
            ret.Id = reader.GetInt32(0);
            ret.LoginId = reader.GetInt32(1);
            ret.AdminId = reader.GetString(2);
            ret.Fname = reader.GetString(3);
            ret.Mname = reader.GetString(4);
            ret.Lname = reader.GetString(5);
            ret.Position = reader.GetString(6);
            ret.ResidentialAddress = reader.GetString(7);
            ret.Gender = reader.GetString(8);
            ret.Bdate = reader.GetDateTime(9);         
            ret.MaritalStatus = reader.GetString(10);
            ret.EmailAddress = reader.GetString(11);
            ret.ContactNo = reader.GetString(12);
            ret.EmergencyNo = reader.GetString(13);
            ret.Deleted = reader.GetInt32(14);
            ret.Url = reader.GetString(15);
            return ret;
        }

        public void Reset()
        {
            Id = 0;
            Fname = string.Empty;
            Mname = string.Empty;
            Lname = string.Empty;
            Bdate = DateTime.Now;
            Position = string.Empty;
            EmailAddress = string.Empty;
            Deleted = 0;
            Url = string.Empty;
            LoginId = 0;
            ResidentialAddress = string.Empty;
            ContactNo = string.Empty;
            EmergencyNo = string.Empty ;
            Gender = "Male";
            MaritalStatus = "Single";
            AdminId = "";
        }
    }
}