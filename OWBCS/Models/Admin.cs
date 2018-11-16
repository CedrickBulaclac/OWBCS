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
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = false)]
        public DateTime Bdate { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string Deleted { get; set; }
        public string Url { get; set; }
        public int LoginId { get; set; }
        public string FormattedDate => Bdate.ToShortDateString();
        public Admin CreateObject(SqlDataReader reader)
        {
            Admin ret = new Admin();
            ret.Id = reader.GetInt32(0);
            ret.Fname = reader.GetString(1);
            ret.Mname = reader.GetString(2);
            ret.Lname = reader.GetString(3);
            ret.Bdate = reader.GetDateTime(4);
            ret.Position = reader.GetString(5);
            ret.Email = reader.GetString(6);
            ret.Deleted = reader.GetString(7);
            ret.Url = reader.GetString(8);        
            ret.LoginId = reader.GetInt32(9);        
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
            Email = string.Empty;
            Deleted = string.Empty;
            Url = string.Empty;
            LoginId = 0;
        }
    }
}