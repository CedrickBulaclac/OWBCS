﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
namespace OWBCS
{
    public static class SqlManager
    {
        public static List<T> Select<T>(SqlCommand command) where T : BaseProperty<T>, new()
        {
            List<T> ret = new List<T>();
            T item = default(T);
            string CON_STRING = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CON_STRING))
            {
                try
                {
                    con.Open();
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        command.Connection = con;
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                item = new T();
                                item = item.CreateObject(reader);
                                ret.Add(item);
                            }
                        }
                    }

                }
                catch
                {
                    throw;
                }
            }
            return ret;
        }

        public static bool ExecuteNonQuery(SqlCommand command)
        {
            bool ret = false;
            string CON_STRING = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            using (SqlConnection con = new SqlConnection(CON_STRING))
            {
                try
                {
                    con.Open();
                    if (con.State == System.Data.ConnectionState.Open)
                    {
                        command.Connection = con;
                        command.ExecuteNonQuery();
                        ret = true;
                    }

                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return ret;
        }
    }

}