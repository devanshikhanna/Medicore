using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;


namespace WebApplication9.Models
{
    public class Staff
    {


        public int StaffID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Photo { get; set; }
      //  public int DepartmentID { get; set; }
        public float Salary { get; set; }
        public string Status { get; set; }



        public int Insert()
        {

            string query = @"INSERT INTO Staff VALUES(@Name, @Email, @Mobile, @Address, @City, @Username, @Password,@Photo,
                            @Salary,@Status)";

            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@Name", this.Name));
            lstParams.Add(new SqlParameter("@Email", this.Email));
            lstParams.Add(new SqlParameter("@Mobile", this.Mobile));
            lstParams.Add(new SqlParameter("@Address", this.Address));
            lstParams.Add(new SqlParameter("@City", this.City));
            lstParams.Add(new SqlParameter("@Username", this.Username));
            lstParams.Add(new SqlParameter("@Password", this.Password));
            lstParams.Add(new SqlParameter("@Photo", this.Photo));
        //    lstParams.Add(new SqlParameter("@DepartmentID", this.DepartmentID));
            lstParams.Add(new SqlParameter("@Salary", this.Salary));
          lstParams.Add(new SqlParameter("@Status", this.Status));

            return DataAccess.ModifyData(query, lstParams);
        }

        public int Update()
        {
            string query = @"UPDATE Staff SET Name = @Name, Email = @Email, Mobile = @Mobile, Address = @Address, City=@City,
                            Username = @Username, Password = @Password, Photo = @Photo ,Salary=@Salary , Status=@Status
                            WHERE StaffID = @StaffID";

            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@Name", this.Name));
            lstParams.Add(new SqlParameter("@Email", this.Email));
            lstParams.Add(new SqlParameter("@Mobile", this.Mobile));
            lstParams.Add(new SqlParameter("@Address", this.Address));
            lstParams.Add(new SqlParameter("@City", this.City));
            lstParams.Add(new SqlParameter("@Username", this.Username));
            lstParams.Add(new SqlParameter("@Password", this.Password));
            lstParams.Add(new SqlParameter("@Photo", this.Photo));
           // lstParams.Add(new SqlParameter("@DepartmentID", this.DepartmentID));
            lstParams.Add(new SqlParameter("@Salary", this.Salary));
            lstParams.Add(new SqlParameter("@Status", this.Status));
            lstParams.Add(new SqlParameter("@StaffID", this.StaffID));

            return DataAccess.ModifyData(query, lstParams);
        }

        public int Delete()
        {
            string query = "DELETE Staff WHERE StaffID = @StaffID";
            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@StaffID", this.StaffID));
            return DataAccess.ModifyData(query, lstParams);
        }

        public bool SelectByID()
        {
            string query = "SELECT * FROM Staff WHERE StaffID = @StaffID";

            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@StaffID", this.StaffID));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {
                this.Name = dt.Rows[0]["Name"].ToString();
                this.Email = dt.Rows[0]["Email"].ToString();
                this.Mobile = dt.Rows[0]["Mobile"].ToString();
                this.Address = dt.Rows[0]["Address"].ToString();
                this.City = dt.Rows[0]["City"].ToString();
                this.Username = dt.Rows[0]["Username"].ToString();
                this.Password = dt.Rows[0]["Password"].ToString();
                this.Photo = dt.Rows[0]["Photo"].ToString();
              //  this.DepartmentID = Convert.ToInt16(dt.Rows[0]["DepartmentID"].ToString());
                this.Salary = Convert.ToInt32(dt.Rows[0]["Salary"].ToString());
                this.StaffID = Convert.ToInt32(dt.Rows[0]["StaffID"].ToString());

                return true;
            }
            else
            {
                return false;
            }


        }

        public bool Authenticate()
        {
            string query = "SELECT * FROM Staff WHERE Username = @Username AND Password = @Password";

            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@Username", this.Username));
            lstParams.Add(new SqlParameter("@Password", this.Password));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {

                this.Name = dt.Rows[0]["Name"].ToString();
                this.Email = dt.Rows[0]["Email"].ToString();
                this.Mobile = dt.Rows[0]["Mobile"].ToString();
                this.Address = dt.Rows[0]["Address"].ToString();
                this.City = dt.Rows[0]["City"].ToString();
                this.Username = dt.Rows[0]["Username"].ToString();
                this.Password = dt.Rows[0]["Password"].ToString();
                this.Photo = dt.Rows[0]["Photo"].ToString();
                this.Salary = Convert.ToInt32(dt.Rows[0]["Salary"].ToString());
                this.StaffID = Convert.ToInt32(dt.Rows[0]["StaffID"].ToString());

                return true;
            }
            else
            {
                return false;
            }


        }

        public DataTable SelectAll()
        {
            string query = "SELECT * FROM Staff";

            List<SqlParameter> lstParams = new List<SqlParameter>();

            return DataAccess.SelectData(query, lstParams);
        }

    }
}