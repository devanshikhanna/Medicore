using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication9.Models
{
    public class Supply
    {
        public int SupplyID { get; set; }
        public int SalesmanID { get; set; }
        public int StoreID { get; set; }
        public int DistributorID { get; set; }
        public string Status { get; set; }
        public int Quantity { get; set; }
        public DateTime CreateDate { get; set; }
        public float Price { get; set; }
        public int ProductID { get; set; }


        public string ProductName;
        public string Storename;
        public string Mobile;
        public string Address;

        public int Insert()
        {



            string query = "Insert into Supply values(@SalesmanID,@DistributorID,@StoreID,@CreateDate,@ProductID,@Quantity,@Price,@Status)";
            List<SqlParameter> lstprms = new List<SqlParameter>();

            lstprms.Add(new SqlParameter("@SalesmanID", this.SalesmanID));
            lstprms.Add(new SqlParameter("@DistributorID", this.DistributorID));
            lstprms.Add(new SqlParameter("@StoreID", this.StoreID));
            lstprms.Add(new SqlParameter("@CreateDate", this.CreateDate));
            lstprms.Add(new SqlParameter("@ProductID", this.ProductID));
            lstprms.Add(new SqlParameter("@Quantity", this.Quantity));
            lstprms.Add(new SqlParameter("@Price", this.Price));
            lstprms.Add(new SqlParameter("@Status", this.Status));

            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public int Update()
        {
            String query = "Update Supply set SalesmanID =@SalesmanID,StoreID= @StoreID,CreateDate = @CreateDate,Status= @Status,DistributorID = @DistributorID,Quantity= @Quantity,Price= @Price,ProductID= @ProductID where SupplyID=@SupplyID";
            List<SqlParameter> lstprms = new List<SqlParameter>();

            lstprms.Add(new SqlParameter("@SalesmanID", this.SalesmanID));
            lstprms.Add(new SqlParameter("@StoreID", this.StoreID));
            lstprms.Add(new SqlParameter("@CreateDate", this.CreateDate));
            lstprms.Add(new SqlParameter("@Status", this.Status));
            lstprms.Add(new SqlParameter("@DistributorID", this.DistributorID));
            lstprms.Add(new SqlParameter("@Quantity", this.Quantity));
            lstprms.Add(new SqlParameter("@Price", this.Price));
            lstprms.Add(new SqlParameter("@ProductID", this.ProductID));
            lstprms.Add(new SqlParameter("@SupplyID", this.SupplyID));

            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public int Delete()
        {
            String query = "Delete from Supply where SupplyID=@SupplyID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@SupplyID", this.SupplyID));
            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public DataTable SelectAll()
        {
            string query = "SELECT * FROM Supply";

            List<SqlParameter> lstParams = new List<SqlParameter>();

            return DataAccess.SelectData(query, lstParams);
        }

        public bool SelectByID()
        {
            string query = "SELECT * FROM Supply WHERE SupplyID = @SupplyID";

            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@SupplyID", this.SupplyID));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {
                this.SalesmanID = Convert.ToInt32(dt.Rows[0]["SalesmanID"]);
                this.StoreID = Convert.ToInt32(dt.Rows[0]["StoreID"]);
                this.CreateDate = Convert.ToDateTime(dt.Rows[0]["CreateDate"]);
                this.Status = dt.Rows[0]["Status"].ToString();
                this.DistributorID = Convert.ToInt32(dt.Rows[0]["DistributorID"]);
                this.Quantity = Convert.ToInt32(dt.Rows[0]["Quantity"]);
                this.Price = Convert.ToSingle(dt.Rows[0]["Price"]);
                this.ProductID = Convert.ToInt32(dt.Rows[0]["ProductID"]);
                this.SupplyID = Convert.ToInt32(dt.Rows[0]["SupplyID"]);

                return true;
            }
            else
            {
                return false;
            }
        }
             public List<Supply> SelectByStoreID(int StoreID)
        {
            string query = @"SELECT S.*,P.Name FROM Supply AS S 
    INNER JOIN Product AS P ON   P.ProductID = S.ProductID  WHERE StoreID = @StoreID";

            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@StoreID", StoreID));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            List<Supply> lstSupply = new List<Supply>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Supply S = new Supply();

                 S.SalesmanID = Convert.ToInt32(dt.Rows[i]["SalesmanID"]);
                 S.StoreID = Convert.ToInt32(dt.Rows[i]["StoreID"]);
                 S.CreateDate = Convert.ToDateTime(dt.Rows[i]["CreateDate"]);
                S.Status = dt.Rows[0]["Status"].ToString();
                S.DistributorID = Convert.ToInt32(dt.Rows[i]["DistributorID"]);
                 S.Quantity = Convert.ToInt32(dt.Rows[i]["Quantity"]);
                 S.Price = Convert.ToSingle(dt.Rows[i]["Price"]);
                 S.ProductID = Convert.ToInt32(dt.Rows[i]["ProductID"]);
                 S.SupplyID = Convert.ToInt32(dt.Rows[i]["SupplyID"]);
                S.ProductName = dt.Rows[i]["Name"].ToString();
                lstSupply.Add(S);
            }

            return lstSupply;

        }

        public List<Supply> PendingSelectBySalesmanID(int salesmanID)
        {
            string query = @"SELECT S.*,St.*,P.Name as 'ProductName' FROM Supply AS S 
                                    INNER JOIN Product AS P ON P.ProductID = S.ProductID
                                    INNER JOIN Store AS St ON St.StoreId = S.StoreId
                                    WHERE SalesmanID = @SalesmanID and S.Status = 'PENDING'";

            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@SalesmanID", salesmanID));
            

            DataTable dt = DataAccess.SelectData(query, lstParams);

            List<Supply> lstSupply = new List<Supply>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Supply S = new Supply();

                S.SalesmanID = Convert.ToInt32(dt.Rows[i]["SalesmanID"]);
                S.StoreID = Convert.ToInt32(dt.Rows[i]["StoreID"]);
                S.CreateDate = Convert.ToDateTime(dt.Rows[i]["CreateDate"]);
                S.Status = dt.Rows[i]["Status"].ToString();
                S.DistributorID = Convert.ToInt32(dt.Rows[i]["DistributorID"]);
                S.Quantity = Convert.ToInt32(dt.Rows[i]["Quantity"]);
                S.Price = Convert.ToSingle(dt.Rows[i]["Price"]);
                S.ProductID = Convert.ToInt32(dt.Rows[i]["ProductID"]);
                S.SupplyID = Convert.ToInt32(dt.Rows[i]["SupplyID"]);
                S.ProductName = dt.Rows[i]["ProductName"].ToString();
                S.Storename = dt.Rows[i]["Storename"].ToString();
                S.Mobile = dt.Rows[i]["Mobile"].ToString();
                S.Address = dt.Rows[i]["Address"].ToString();

                lstSupply.Add(S);
            }

            return lstSupply;
        }

        public List<Supply> SelectBySalesmanID(int SalesmanID)
        {
            string query = @"SELECT S.*,St.*,P.Name as 'ProductName' FROM Supply AS S 
                                    INNER JOIN Product AS P ON P.ProductID = S.ProductID
                                    INNER JOIN Store AS St ON St.StoreId = S.StoreId
                                    WHERE SalesmanID = @SalesmanID";

            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@SalesmanID", SalesmanID));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            List<Supply> lstSupply = new List<Supply>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Supply S = new Supply();

                S.SalesmanID = Convert.ToInt32(dt.Rows[i]["SalesmanID"]);
                S.StoreID = Convert.ToInt32(dt.Rows[i]["StoreID"]);
                S.CreateDate = Convert.ToDateTime(dt.Rows[i]["CreateDate"]);
                S.Status = dt.Rows[i]["Status"].ToString();
                S.DistributorID = Convert.ToInt32(dt.Rows[i]["DistributorID"]);
                S.Quantity = Convert.ToInt32(dt.Rows[i]["Quantity"]);
                S.Price = Convert.ToSingle(dt.Rows[i]["Price"]);
                S.ProductID = Convert.ToInt32(dt.Rows[i]["ProductID"]);
                S.SupplyID = Convert.ToInt32(dt.Rows[i]["SupplyID"]);
                S.ProductName = dt.Rows[i]["ProductName"].ToString();
                S.Storename = dt.Rows[i]["Storename"].ToString();
                S.Mobile = dt.Rows[i]["Mobile"].ToString();
                S.Address = dt.Rows[i]["Address"].ToString();

                lstSupply.Add(S);
            }

            return lstSupply;

        }

        public DataTable Search(DateTime From, DateTime To, int DistributorID, string Status)
        {
            String query = @"Select Supply.*, store.name as 'storename', product.name as 'productname'
                                from Supply
                                    inner join store on store.storeid = Supply.storeid
                                    inner join product on product.productid = Supply.productid
                                WHERE (store.DistributorID = @DistributorID)
                                    AND (Supply.status = @Status)
                                    AND (Supply.CreateDate BETWEEN @From AND @To)";

            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@DistributorID", DistributorID));
            lstprms.Add(new SqlParameter("@Status", Status));
            lstprms.Add(new SqlParameter("@From", From));
            lstprms.Add(new SqlParameter("@To", To));

            DataTable dt = DataAccess.SelectData(query, lstprms);
            return dt;
        }
        
    }
}

 
