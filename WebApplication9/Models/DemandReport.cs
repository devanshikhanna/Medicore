using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication9.Models
{
    public class DemandReport
    { 
        public int DemandReportID { get; set; }
        public int DistributorID { get; set; }
        public bool Status { get; set; }
        public String Details { get; set; }
        public DateTime CreateDate { get; set; }

        public int Insert()
        {



            string query = "Insert into DemandReport values(@DistributorID,@CreateDate,@Status,@Detail)";
            List<SqlParameter> lstprms = new List<SqlParameter>();

            lstprms.Add(new SqlParameter("@DistributorID", this.DistributorID));
            lstprms.Add(new SqlParameter("@CreateDate", this.CreateDate));
            lstprms.Add(new SqlParameter("@Status", this.Status));
            lstprms.Add(new SqlParameter("@Detail", this.Details));
           

            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public int Update()
        {
            String query = "Update DemandReport set @DistributorID, @CreateDate, @Status, @Detail where DemandReportID=@DemandReportID";
            List<SqlParameter> lstprms = new List<SqlParameter>();

            lstprms.Add(new SqlParameter("@DistributorID", this.DistributorID));
            lstprms.Add(new SqlParameter("@CreateDate", this.CreateDate));
            lstprms.Add(new SqlParameter("@Status", this.Status));
            lstprms.Add(new SqlParameter("@Detail", this.Details));
            lstprms.Add(new SqlParameter("@DemandReportID", this.DemandReportID));

            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public int Delete()
        {
            String query = "Delete * from DemandReport where DemandReportID=@DemandReportID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@DemandReportID", this.DemandReportID));
            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public DataTable SelectAll()
        {
            string query = "SELECT * FROM DemandReport";

            List<SqlParameter> lstParams = new List<SqlParameter>();

            return DataAccess.SelectData(query, lstParams);
        }

        public bool SelectByID()
        {
            string query = "SELECT * FROM DemandReport WHERE DemandReportID = @DemandReportID";

            List<SqlParameter> lstParams = new List<SqlParameter>();
            lstParams.Add(new SqlParameter("@DemandReportID", this.DemandReportID));
            DataTable dt = DataAccess.SelectData(query, lstParams);

            if (dt.Rows.Count > 0)
            {
                this.DistributorID = Convert.ToInt32(dt.Rows[0]["DistributorID"]);
                this.CreateDate = Convert.ToDateTime(dt.Rows[0]["CreateDate"]);
                this.Status = Convert.ToBoolean(dt.Rows[0]["Status"]);
                this.Details = dt.Rows[0]["Details"].ToString();
                this.DemandReportID = Convert.ToInt32(dt.Rows[0]["DemandReportID"]);

                return true;
            }
            else
            {
                return false;
            }
        }


        public DataTable Search(DateTime From, DateTime To, int DistributorID)
        {
            String query = @"Select DemandReport.*
                                from DemandReport
                                WHERE (DemandReport.DistributorID = @DistributorID)
                                    AND (DemandReport.CreateDate BETWEEN @From AND @To)";

            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@DistributorID", DistributorID));
            lstprms.Add(new SqlParameter("@From", From));
            lstprms.Add(new SqlParameter("@To", To));

            DataTable dt = DataAccess.SelectData(query, lstprms);
            return dt;
        }

    }

}