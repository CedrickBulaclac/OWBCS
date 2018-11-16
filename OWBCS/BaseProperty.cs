using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
namespace OWBCS
{
    public interface BaseProperty<T>
    {
        T CreateObject(SqlDataReader reader);
        void Reset();
    }
}