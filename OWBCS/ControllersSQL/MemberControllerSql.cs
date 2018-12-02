using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace OWBCS
{
    public class MemberControllerSql
    {
        public static List<Member> GetAll()
        {
            const string GET_ALL = "select Id,LoginId,EmployeeId,Salutation,Fname,Mname,Lname,Position,ResidentialAddress,Gender,Birthdate,MaritalStatus,EmailAddress,ContactNo,EmergencyContactNo,SalaryAmt,Deleted,Url from tbMember where Deleted=0";
            List<Member> ret = new List<Member>();
            SqlCommand cmd = new SqlCommand(GET_ALL);
            ret = SqlManager.Select<Member>(cmd);
            return ret;
        }
        public static Member GetById(int id)
        {
            const string GET_ALL = "select Id,LoginId,EmployeeId,Salutation,Fname,Mname,Lname,Position,ResidentialAddress,Gender,Birthdate,MaritalStatus,EmailAddress,ContactNo,EmergencyContactNo,SalaryAmt,Deleted,Url from tbMember where Id=@Id and Deleted=0";
            Member ret = new Member();
            SqlCommand cmd = new SqlCommand(GET_ALL);
            cmd.Parameters.Add(new SqlParameter("Id", id));
            ret = SqlManager.Select<Member>(cmd).First();
            return ret;
        }
        public static Member GetByLoginId(int id)
        {
            const string GET_ALL = "select Id,LoginId,EmployeeId,Salutation,Fname,Mname,Lname,Position,ResidentialAddress,Gender,Birthdate,MaritalStatus,EmailAddress,ContactNo,EmergencyContactNo,SalaryAmt,Deleted,Url from tbMember where LoginId=@Id and Deleted=0";
            Member ret = new Member();
            SqlCommand cmd = new SqlCommand(GET_ALL);
            cmd.Parameters.Add(new SqlParameter("Id", id));
            ret = SqlManager.Select<Member>(cmd).First();
            return ret;
        }
        public static bool Update(Member member)
        {
            const string insert = "update [tbMember] set Fname=@Fname, Mname=@Mname, Lname=@Lname,Birthdate=@Bdate,Position=@Position,EmailAddress=@Email,Gender=@Gender,ContactNo=@ContactNo,EmergencyContactNo=@EmergencyNo,MaritalStatus=@MaritalStatus,ResidentialAddress=@ResidentialAddress, SalaryAmt=@SalaryAmt where Id=@id ";
            SqlCommand ret = new SqlCommand(insert);
            ret.Parameters.Add(new SqlParameter("Fname", member.Fname));
            ret.Parameters.Add(new SqlParameter("Mname", member.Mname));
            ret.Parameters.Add(new SqlParameter("Lname", member.Lname));
            ret.Parameters.Add(new SqlParameter("Bdate", member.Birthdate));
            ret.Parameters.Add(new SqlParameter("Position", member.Position));
            ret.Parameters.Add(new SqlParameter("Email", member.EmailAddress));
            //ret.Parameters.Add(new SqlParameter("Deleted", member.Deleted));
            //ret.Parameters.Add(new SqlParameter("Url", member.Url));
            //ret.Parameters.Add(new SqlParameter("LoginId", member.LoginId));
            ret.Parameters.Add(new SqlParameter("ResidentialAddress", member.ResidentialAddress));
            ret.Parameters.Add(new SqlParameter("Gender", member.Gender));
            ret.Parameters.Add(new SqlParameter("ContactNo", member.ContactNo));
            ret.Parameters.Add(new SqlParameter("MaritalStatus", member.MaritalStatus));
           // ret.Parameters.Add(new SqlParameter("EmployeeId", member.EmployeeId));
            ret.Parameters.Add(new SqlParameter("EmergencyNo", member.EmergencyContactNo));
            ret.Parameters.Add(new SqlParameter("SalaryAmt", member.SalaryAmt));
            ret.Parameters.Add(new SqlParameter("id", member.Id));
            return SqlManager.ExecuteNonQuery(ret);
        }
        public static bool Delete(Member admin)
        {
            const string insert = "update tbMember set Deleted=1 where Id=@Id ";
            SqlCommand ret = new SqlCommand(insert);
            ret.Parameters.Add(new SqlParameter("Id", admin.Id));
            return SqlManager.ExecuteNonQuery(ret);
        }
        public static bool Insert(Member member)
        {
            const string insert = "insert [tbMember] (EmployeeId,Salutation,SalaryAmt,Fname,Mname,Lname,Position,ResidentialAddress,Gender,Birthdate,MaritalStatus,EmailAddress,ContactNo,EmergencyContactNo,Deleted,Url,LoginId) values (@EmployeeId,@Salutation,@SalaryAmt,@Fname,@Mname,@Lname,@Position,@ResidentialAddress,@Gender,@Bdate,@MaritalStatus,@Email,@ContactNo,@EmergencyContactNo,@Deleted,@Url,@LoginId) ";
            SqlCommand ret = new SqlCommand(insert);
            ret.Parameters.Add(new SqlParameter("Fname", member.Fname));
            ret.Parameters.Add(new SqlParameter("Mname", member.Mname));
            ret.Parameters.Add(new SqlParameter("Lname", member.Lname));
            ret.Parameters.Add(new SqlParameter("Bdate", member.Birthdate));
            ret.Parameters.Add(new SqlParameter("Position", member.Position));
            ret.Parameters.Add(new SqlParameter("Email", member.EmailAddress));
            ret.Parameters.Add(new SqlParameter("Deleted", member.Deleted));
            ret.Parameters.Add(new SqlParameter("Url", member.Url));
            ret.Parameters.Add(new SqlParameter("LoginId", member.LoginId));
            ret.Parameters.Add(new SqlParameter("ResidentialAddress", member.ResidentialAddress));
            ret.Parameters.Add(new SqlParameter("Gender", member.Gender));
            ret.Parameters.Add(new SqlParameter("ContactNo", member.ContactNo));
            ret.Parameters.Add(new SqlParameter("MaritalStatus", member.MaritalStatus));
            ret.Parameters.Add(new SqlParameter("EmployeeId", member.EmployeeId));
            ret.Parameters.Add(new SqlParameter("EmergencyContactNo", member.EmergencyContactNo));
            ret.Parameters.Add(new SqlParameter("SalaryAmt", member.SalaryAmt));
            ret.Parameters.Add(new SqlParameter("Salutation", member.Salutation));
            return SqlManager.ExecuteNonQuery(ret);
        }
    }
}