using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication9.Models
{
    public class Store
    {
        

        public int StoreID { get; set; }
        
        public String Name { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public String Username { get; set; }
        public string Password { get; set; }
        public String Photo { get; set; }
        public int DistributorID { get; set; }
        public string StoreName { get; set; }
        public DateTime CreateDate { get; set; }


        public int Insert()
        {
            string query = "Insert into Store values(@Name, @Mobile, @Address, @Username, @Password, @Photo, @DistributorID, @StoreName, @CreateDate)";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@Name", this.Name));
            lstprms.Add(new SqlParameter("@Mobile", this.Mobile));
            lstprms.Add(new SqlParameter("@Address", this.Address));
            lstprms.Add(new SqlParameter("@Username", this.Username));
            lstprms.Add(new SqlParameter("@Password", this.Password));
            lstprms.Add(new SqlParameter("@Photo", this.Photo));
            lstprms.Add(new SqlParameter("@DistributorID", this.DistributorID));
            lstprms.Add(new SqlParameter("@StoreName", this.StoreName));
            lstprms.Add(new SqlParameter("@CreateDate", this.CreateDate));

            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public int Update()
        {
            string query = "UPDATE Store SET Name=@Name, Mobile=@Mobile, Address=@Address, Username=@Username, Password=@Password, Photo=@Photo,  DistributorID=@DistributorID, StoreName=@StoreName, CreateDate=@CreateDate WHERE StoreID=@StoreID";

            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@Name", this.Name));
            lstParams.Add(new SqlParameter("@Mobile", this.Mobile));
            lstParams.Add(new SqlParameter("@Address", this.Address));
            lstParams.Add(new SqlParameter("@Username", this.Username));
            lstParams.Add(new SqlParameter("@Password", this.Password));
            lstParams.Add(new SqlParameter("@Photo", this.Photo));
            lstParams.Add(new SqlParameter("@DistributorID", this.DistributorID));
            lstParams.Add(new SqlParameter("@StoreName", this.StoreName));
            lstParams.Add(new SqlParameter("@CreateDate", this.CreateDate));
            lstParams.Add(new SqlParameter("@StoreID", this.StoreID));

            int x = DataAccess.ModifyData(query, lstParams);
            return x;
        }
        public int Delete()
        {
            String query = "Delete from Store where StoreID=@StoreID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@StoreID", this.StoreID));
            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public bool SelectByID()
        {
            string query = "SELECT * FROM Store WHERE StoreID = @StoreID";

            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@StoreID", this.StoreID));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {
                this.StoreID = Convert.ToInt32(dt.Rows[0]["StoreID"]);

                this.Name = dt.Rows[0]["Name"].ToString();
                this.Mobile = dt.Rows[0]["Mobile"].ToString();
                this.Address = dt.Rows[0]["Address"].ToString();
                this.Username = dt.Rows[0]["Username"].ToString();
                this.Password = dt.Rows[0]["Password"].ToString();
                this.Photo = dt.Rows[0]["Photo"].ToString();
                this.DistributorID = Convert.ToInt32(dt.Rows[0]["DistributorID"]);
                this.StoreName = dt.Rows[0]["StoreName"].ToString();
                this.CreateDate = Convert.ToDateTime(dt.Rows[0]["CreateDate"]);
                this.StoreID = Convert.ToInt32(dt.Rows[0]["StoreID"]);
                return true;
            }
            else
            {
                return false;
            }


        }

        public bool Authenticate()
        {
            string query = "SELECT * FROM Store WHERE Username = @Username AND Password = @Password";

            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@Username", this.Username));
            lstParams.Add(new SqlParameter("@Password", this.Password));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {
                this.StoreID = Convert.ToInt32(dt.Rows[0]["StoreID"]);
        
                this.Name = dt.Rows[0]["Name"].ToString();
                this.Mobile = dt.Rows[0]["Mobile"].ToString();
                this.Address = dt.Rows[0]["Address"].ToString();
                this.Username = dt.Rows[0]["Username"].ToString();
                this.Password = dt.Rows[0]["Password"].ToString();
                this.Photo = dt.Rows[0]["Photo"].ToString();
                this.DistributorID = Convert.ToInt32(dt.Rows[0]["DistributorID"]);
                this.StoreName = dt.Rows[0]["StoreName"].ToString();
                this.CreateDate = Convert.ToDateTime(dt.Rows[0]["CreateDate"]);

                return true;
            }
            else
            {
                return false;
            }


        }

        public DataTable SelectAll()
        {
            string query = "SELECT * FROM Store";

            List<SqlParameter> lstParams = new List<SqlParameter>();

            return DataAccess.SelectData(query, lstParams);
        }

        public List<Store> SelectAllList()
        {
            String query = "Select * from Store";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstprms);

            List<Store> lstStore = new List<Store>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Store S = new Store();
                S.StoreID = Convert.ToInt32(dt.Rows[i]["StoreID"]);
                S.SelectByID();
                lstStore.Add(S);
            }

            return lstStore;
        }

       public  DataTable SelectAllByDistributorID(int distributorID)
        {
            string query = "SELECT * FROM Store where DistributorID = "+distributorID;

            List<SqlParameter> lstParams = new List<SqlParameter>();

            return DataAccess.SelectData(query, lstParams);
        }


        public int FindPendingAmount(int storeID)
        {
            int totaldue = 0;
            List<SqlParameter> lstParams = new List<SqlParameter>();

            try
            {
                string query = "SELECT sum(Quantity * Price) as Total FROM Supply where StoreID = " + storeID;


                DataTable dt = DataAccess.SelectData(query, lstParams);

                totaldue = Convert.ToInt32(dt.Rows[0]["Total"]);

            }
            catch(Exception)
            {
                totaldue = 0;
            }
            int totalpaid = 0;
            try
            {
                String query = "SELECT sum(Amount) as Total FROM Payment where StoreID = " + storeID;


                DataTable dt = DataAccess.SelectData(query, lstParams);

                totalpaid = Convert.ToInt32(dt.Rows[0]["Total"]);
            }
            catch (Exception)
            {
                totalpaid = 0;
            }
                int pending = totaldue - totalpaid;
            
            return pending;


        }
    }
}
  
  
  
    
   
