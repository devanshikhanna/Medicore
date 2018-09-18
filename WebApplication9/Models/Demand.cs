using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication9.Models
{
    public class Demand
    {
        public int DemandID { get; set; }
        public int StoreID { get; set; }
        public DateTime CreateDate { get; set; }
        public string Status { get; set; }
        public int DemandReportID { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public int ProductID { get; set; }


        public string ProductName;

        public int Insert()
        {



            string query = "Insert into Demand values(@StoreID,@CreateDate,@Status,@DemandReportID,@Quantity,@Price,@ProductID)";
            List<SqlParameter> lstprms = new List<SqlParameter>();

            lstprms.Add(new SqlParameter("@StoreID", this.StoreID));
            lstprms.Add(new SqlParameter("@CreateDate", this.CreateDate));
            lstprms.Add(new SqlParameter("@Status", this.Status));
            lstprms.Add(new SqlParameter("@DemandReportID", this.DemandReportID));
            lstprms.Add(new SqlParameter("@Quantity", this.Quantity));
            lstprms.Add(new SqlParameter("@Price", this.Price));
            lstprms.Add(new SqlParameter("@ProductID", this.ProductID));

            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public int Update()
        {
            String query = "Update Demand set @DemandID,@StoreID,@CreateDate,@Status,@DemandReportID,@ProductID,@Quantity,@Price where DemandID=@DemandID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@StoreID", this.StoreID));
            lstprms.Add(new SqlParameter("@CreateDate", this.CreateDate));
            lstprms.Add(new SqlParameter("@Status", this.Status));
            lstprms.Add(new SqlParameter("@DemandReortID", this.DemandReportID));
            lstprms.Add(new SqlParameter("@Quantity", this.Quantity));
            lstprms.Add(new SqlParameter("@Price", this.Price));
            lstprms.Add(new SqlParameter("@ProductID", this.ProductID));

            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public int Delete()
        {
            String query = "Delete * from Demand where DemandID=@DemandID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@DemandID", this.DemandID));
            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public DataTable SelectAll()
        {
            string query = "SELECT * FROM Demand";

            List<SqlParameter> lstParams = new List<SqlParameter>();

            return DataAccess.SelectData(query, lstParams);
        }

        public bool SelectByID()
        {
            string query = "SELECT * FROM Demand WHERE DemandID = @DemandID";

            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@Demand", this.DemandID));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {
                this.StoreID = Convert.ToInt32(dt.Rows[0]["StoreID"]);
                this.Status = dt.Rows[0]["Status"].ToString();
                this.DemandReportID = Convert.ToInt32(dt.Rows[0]["DemandReportID"]);
                this.Quantity = Convert.ToInt32(dt.Rows[0]["Quantity"]);
                this.Price = Convert.ToSingle(dt.Rows[0]["Price"]);
                this.CreateDate = Convert.ToDateTime(dt.Rows[0]["CreateDate"]);
                this.ProductID = Convert.ToInt32(dt.Rows[0]["ProductID"]);
                this.DemandID = Convert.ToInt32(dt.Rows[0]["DemandID"]);
                return true;
            }
            else
            {
                return false;
            }


        }

        public List<Demand> SelectByStoreID(int StoreID)
        {
            string query = "SELECT D.*,P.Name FROM Demand AS D " +
               " INNER JOIN Product AS P ON   P.ProductID = D.ProductID  WHERE StoreID = @StoreID";

            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@StoreID", StoreID));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            List<Demand> lstDemand = new List<Demand>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Demand D = new Demand();

                D.StoreID = Convert.ToInt32(dt.Rows[i]["StoreID"]);
                D.Status = dt.Rows[i]["Status"].ToString();
                D.DemandReportID = Convert.ToInt32(dt.Rows[i]["DemandReportID"]);
                D.Quantity = Convert.ToInt32(dt.Rows[i]["Quantity"]);
                D.Price = Convert.ToSingle(dt.Rows[i]["Price"]);
                D.CreateDate = Convert.ToDateTime(dt.Rows[i]["CreateDate"]);
                D.ProductID = Convert.ToInt32(dt.Rows[i]["ProductID"]);
                D.DemandID = Convert.ToInt32(dt.Rows[i]["DemandID"]);
                D.ProductName = dt.Rows[i]["Name"].ToString();

                lstDemand.Add(D);
            }

            return lstDemand;
        }


        public DataTable Search(DateTime From, DateTime To, int DistributorID, string Status)
        {
            String query = @"Select demand.*, store.name as 'storename', product.name as 'productname'
                                from Demand
                                    inner join store on store.storeid = demand.storeid
                                    inner join product on product.productid = demand.productid
                                WHERE (store.DistributorID = @DistributorID)
                                    AND (demand.status = @Status)
                                    AND (Demand.CreateDate BETWEEN @From AND @To)";

            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@DistributorID", DistributorID));
            lstprms.Add(new SqlParameter("@Status", Status));
            lstprms.Add(new SqlParameter("@From", From));
            lstprms.Add(new SqlParameter("@To", To));

            DataTable dt = DataAccess.SelectData(query, lstprms);
            return dt;
        }

    }

    //outof scope
}
