using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication9.Models
{
    public class Distributor
    {
        //public List<SqlParameter> lstParams;
                
        public int DistributorID { get; set; }
        public String Name { get; set; }
        public string Mobile { get; set; }
        public String Photo { get; set; }
        public String Username { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public DateTime CreateDate { get; set; }

        public int Insert()
                {
            string query = "Insert into Distributor values(@Name, @Mobile,@Address,@Username,@Password, @Photo,@CreateDate)";
                    List<SqlParameter> lstprms = new List<SqlParameter>();
                    lstprms.Add(new SqlParameter("@Name", this.Name));
                    lstprms.Add(new SqlParameter("@Mobile", this.Mobile));
                    lstprms.Add(new SqlParameter("@Photo", this.Photo));
                    lstprms.Add(new SqlParameter("@Username", this.Username));
                    lstprms.Add(new SqlParameter("@Password", this.Password));
                    lstprms.Add(new SqlParameter("@Address", this.Address));
                    lstprms.Add(new SqlParameter("@CreateDate", this.CreateDate));

                    int x = DataAccess.ModifyData(query, lstprms);
                    return x;
            //everything is almost perfect. the values are also coming at debug time.
                }
                public int Update()
                {
            string query = "UPDATE Distributor SET Name = @Name, Mobile = @Mobile, Address = @Address, Username = @Username, Password = @Password, Photo = @Photo, CreateDate = @CreateDate WHERE DistributorId = @DistributorID";

            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@Name", this.Name));
            lstParams.Add(new SqlParameter("@Mobile", this.Mobile));
            lstParams.Add(new SqlParameter("@Photo", this.Photo));
            lstParams.Add(new SqlParameter("@Username", this.Username));
            lstParams.Add(new SqlParameter("@Password", this.Password));
            lstParams.Add(new SqlParameter("@Address", this.Address));
            lstParams.Add(new SqlParameter("@CreateDate", this.CreateDate));
            lstParams.Add(new SqlParameter("@DistributorID", this.DistributorID));
            int x = DataAccess.ModifyData(query, lstParams);
                     return x;
                }
                public int Delete()
                {
                    String query = "Delete from Distributor where DistributorId=@DistributorID";
                    List<SqlParameter> lstprms = new List<SqlParameter>();
                    lstprms.Add(new SqlParameter("@DistributorID", this.DistributorID));
                    int x = DataAccess.ModifyData(query, lstprms);
                    return x;
                }
        public bool SelectByID()
        {
            string query = "SELECT * FROM Distributor WHERE DistributorId = @DistributorID";

            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@DistributorID", this.DistributorID));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {
                this.Name = dt.Rows[0]["Name"].ToString();
                this.Mobile = dt.Rows[0]["Mobile"].ToString();
                this.Photo = dt.Rows[0]["Photo"].ToString();
                this.Username = dt.Rows[0]["Username"].ToString();
                this.Password = dt.Rows[0]["Password"].ToString();
                this.Address = dt.Rows[0]["Address"].ToString();
                this.CreateDate = Convert.ToDateTime(dt.Rows[0]["CreateDate"]);
                this.DistributorID = Convert.ToInt32(dt.Rows[0]["DistributorID"]);

                return true;
            }
            else
            {
                return false;
            }


        }

        public bool Authenticate()
        {
            string query = "SELECT * FROM Distributor WHERE Username = @Username AND Password = @Password";

            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@Username", this.Username));
            lstParams.Add(new SqlParameter("@Password", this.Password));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {
            
                this.Name = dt.Rows[0]["Name"].ToString();
                this.Mobile = dt.Rows[0]["Mobile"].ToString();
                this.Photo = dt.Rows[0]["Photo"].ToString();
                this.Username = dt.Rows[0]["Username"].ToString();
                this.Password = dt.Rows[0]["Password"].ToString();
                this.Address = dt.Rows[0]["Address"].ToString();
                this.CreateDate = Convert.ToDateTime(dt.Rows[0]["CreateDate"]);
                this.DistributorID = Convert.ToInt32(dt.Rows[0]["DistributorID"]);
                return true;
            }
            else
            {
                return false;
            }


        }

        public DataTable SelectAll()
        {
            string query = "SELECT * FROM Distributor";

            List<SqlParameter> lstParams = new List<SqlParameter>();

            return DataAccess.SelectData(query, lstParams);
        }
               
    }
}
  
  
  