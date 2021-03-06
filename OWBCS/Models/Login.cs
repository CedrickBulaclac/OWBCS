﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OWBCS
{
    public class Login : BaseProperty<Login>
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Hash { get; set; }
        public int Locked { get; set; }
        public string CreatedBy { get; set; }
        public string ModifyBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int Level { get; set; }
        public DateTime LastLogin { get; set; }
      
        public Login CreateObject(SqlDataReader reader)
        {
            Login ret = new Login();
            ret.Id = reader.GetInt32(0);
            ret.Username = reader.GetString(1);
            ret.Hash = reader.GetString(2);
            ret.CreatedBy = reader.GetString(3);
            ret.ModifyBy = reader.GetString(4);
            ret.CreatedDate = reader.GetDateTime(5);
            ret.ModifiedDate = reader.GetDateTime(6);
            ret.Level = Convert.ToInt32(reader.GetValue(7));
            ret.Locked = Convert.ToInt32(reader.GetValue(8));
            ret.LastLogin = reader.GetDateTime(9);            
            return ret;
        }

        public void Reset()
        {
            this.Id = 0;
            this.Username = string.Empty;
            this.CreatedBy = string.Empty;
            this.Locked = 0;
            this.ModifyBy = string.Empty;
            this.CreatedDate = DateTime.Today;
            this.ModifiedDate = DateTime.Today;
            this.LastLogin = DateTime.Today;
            this.Level = 0;
            this.Hash = string.Empty;
        }
    }
}