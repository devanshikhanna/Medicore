using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication9.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int StoreID { get; set; }
        public int SalesmanID { get; set; }
        public int DistributorID { get; set; }
        public float Amount { get; set; }
        public DateTime CreateDate { get; set; }

        public string CreateDateString;
        public string StoreName;
        public int Insert()
        {

            string query = "Insert into Payment values(@StoreID,@SalesmanID,@DistributorID,@Amount,@CreateDate)";
            List<SqlParameter> lstprms = new List<SqlParameter>();

            lstprms.Add(new SqlParameter("@StoreID", this.StoreID));
            lstprms.Add(new SqlParameter("@SalesmanID", this.SalesmanID));
            lstprms.Add(new SqlParameter("@DistributorID", this.DistributorID));
            lstprms.Add(new SqlParameter("@Amount", this.Amount));
            lstprms.Add(new SqlParameter("@CreateDate", this.CreateDate));

            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public int Update()
        {
            String query = "Update DemandReport set @StoreID, @SalesmanID, @DistributorID, @Amount, @CreateDate where PaymentID=@PaymentID";
            List<SqlParameter> lstprms = new List<SqlParameter>();

            lstprms.Add(new SqlParameter("@StoreID", this.StoreID));
            lstprms.Add(new SqlParameter("@SalesmanID", this.SalesmanID));
            lstprms.Add(new SqlParameter("@DistributorID", this.DistributorID));
            lstprms.Add(new SqlParameter("@Amount", this.Amount));
            lstprms.Add(new SqlParameter("@CreateDate", this.CreateDate));

            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public int Delete()
        {
            String query = "Delete * from PaymentID where PaymentID=@PaymentID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@PaymentID", this.PaymentID));
            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public DataTable SelectAll()
        {
            string query = "SELECT * FROM Payment";

            List<SqlParameter> lstParams = new List<SqlParameter>();

            return DataAccess.SelectData(query, lstParams);
        }

        public bool SelectByID()
        {
            string query = "SELECT * FROM Payment WHERE PaymentID = @PaymentID";

            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@PaymentID", this.PaymentID));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {
                this.StoreID = Convert.ToInt32(dt.Rows[0]["StoreID"]);
                this.SalesmanID = Convert.ToInt32(dt.Rows[0]["SalesmanID"]);
                this.DistributorID = Convert.ToInt32(dt.Rows[0]["DistributorID"]);
                this.Amount = Convert.ToInt32(dt.Rows[0]["Amount"]);
                this.CreateDate = Convert.ToDateTime(dt.Rows[0]["CreateDate"]);
                this.CreateDateString = this.CreateDate.ToString("dd MMM, yyyy HH:mm");
                this.PaymentID = Convert.ToInt32(dt.Rows[0]["PaymentID"]);

                return true;
            }
            else
            {
                return false;
            }
        }

        public List<Payment> SelectByStoreID(int StoreID)
        {
            string query = "SELECT * FROM Payment WHERE StoreID = @StoreID order by PaymentID desc";

            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@StoreID", StoreID));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            List<Payment> lstPayment = new List<Payment>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Payment P = new Payment();

                P.PaymentID = Convert.ToInt32(dt.Rows[i]["PaymentID"]);
                P.SelectByID();
                lstPayment.Add(P);
            }

            return lstPayment;

        }

        public List<Payment> SelectBySalesmanID(int SalesmanID)
        {
            string query = "SELECT * FROM Payment WHERE SalesmanID = @SalesmanID";

            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@SalesmanID", SalesmanID));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            List<Payment> lstPayment = new List<Payment>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Payment P = new Payment();
                P.PaymentID = Convert.ToInt32(dt.Rows[i]["PaymentID"]);
                P.SelectByID();
                lstPayment.Add(P);
            }

            return lstPayment;

        }


        public List<Payment> SelectTodayBySalesmanID(int SalesmanID)
        {
            string query = @"SELECT P.*, S.Storename FROM 
                                        Payment as P
                                        inner join Store as S on S.StoreID = P.StoreID
                WHERE P.SalesmanID = @SalesmanID and (P.CreateDate between @From and @To)";

            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@SalesmanID", SalesmanID));
            lstParams.Add(new SqlParameter("@From", DateTime.Today));
            lstParams.Add(new SqlParameter("@To", DateTime.Today.AddDays(1).AddMilliseconds(-1)));


            DataTable dt = DataAccess.SelectData(query, lstParams);

            List<Payment> lstPayment = new List<Payment>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Payment P = new Payment();
                P.PaymentID = Convert.ToInt32(dt.Rows[i]["PaymentID"]);
                P.SelectByID();
                P.StoreName = dt.Rows[i]["Storename"].ToString();
                lstPayment.Add(P);
            }

            return lstPayment;

        }


        public DataTable Search(DateTime From, DateTime To, int DistributorID, int StoreID, int SalesmanID)
        {
            String query = @"Select Payment.*, store.name as 'StoreName', Salesman.name as 'SalesmanName'
                                from Payment
                                    inner join store on store.storeid = Payment.storeid
                                    inner join Salesman on Salesman.salesmanid = Payment.salesmanid
                                WHERE (Payment.DistributorID = @DistributorID)
                                    AND (Payment.StoreID = @StoreID OR @StoreID = 0)
                                    AND (Payment.SalesmanID = @SalesmanID OR @SalesmanID = 0)
                                    AND (Payment.CreateDate BETWEEN @From AND @To)";

            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@DistributorID", DistributorID));
            lstprms.Add(new SqlParameter("@StoreID", StoreID ));
            lstprms.Add(new SqlParameter("@SalesmanID", SalesmanID));
            lstprms.Add(new SqlParameter("@From", From));
            lstprms.Add(new SqlParameter("@To", To));

            DataTable dt = DataAccess.SelectData(query, lstprms);
            return dt;
        }
    }
}