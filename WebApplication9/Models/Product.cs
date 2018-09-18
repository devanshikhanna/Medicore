using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication9.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public String Name { get; set; }
        public float Price { get; set; }
        public String Photo { get; set; }
        public String Description { get; set; }
        public int CategoryID { get; set; }
        public int ProductCode { get; set; }

        public int Insert()
        {



            string query = "Insert into Product values(@Name,@Photo,@Description,@CategoryID,@Price, @ProductCode)";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@Name", this.Name));
            lstprms.Add(new SqlParameter("@Price", this.Price));
            lstprms.Add(new SqlParameter("@Photo", this.Photo));
            lstprms.Add(new SqlParameter("@Description", this.Description));
            lstprms.Add(new SqlParameter("@CategoryID", this.CategoryID));
            lstprms.Add(new SqlParameter("@ProductCode", this.ProductCode));

            int x = DataAccess.ModifyData(query,lstprms);
            return x;
        }
        public int Update()
        {
            String query = "Update Product set Name=@Name, Price=@Price, Photo=@Photo, Description=@Description, CategoryID=@CategoryID, ProductCode=@ProductCode where ProductID=@ProductID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@ProductID", this.ProductID));
            lstprms.Add(new SqlParameter("@Name", this.Name));
            lstprms.Add(new SqlParameter("@Price", this.Price));
            lstprms.Add(new SqlParameter("@Photo", this.Photo));
            lstprms.Add(new SqlParameter("@Description", this.Description));
            lstprms.Add(new SqlParameter("@CategoryID", this.CategoryID));
            lstprms.Add(new SqlParameter("@ProductCode", this.ProductCode));
            int x = DataAccess.ModifyData(query,lstprms);
            return x;
        }
        public int Delete()
        {
            String query = "Delete from Product where ProductID=@ProductID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@ProductID", this.ProductID));
            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public DataTable SelectAll()
        {
            string query = "SELECT * FROM Product";

            List<SqlParameter> lstParams = new List<SqlParameter>();

            return DataAccess.SelectData(query, lstParams);
        }
        public bool SelectById()
        {
            String query = "Select * from Product where ProductID=@ProductID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@ProductID", this.ProductID));
            DataTable dt = DataAccess.SelectData(query, lstprms);
            if (dt.Rows.Count > 0)
            {
                this.Name = dt.Rows[0]["Name"].ToString();
                this.Price = Convert.ToSingle(dt.Rows[0]["Price"]);
                this.Photo = dt.Rows[0]["Photo"].ToString();
                this.Description = dt.Rows[0]["Description"].ToString();
                this.CategoryID = Convert.ToInt32(dt.Rows[0]["CategoryID"]);
                this.ProductCode = Convert.ToInt32(dt.Rows[0]["ProductCode"]);
                return true;
            }
            else
            {
                return false;
            }
        }
        public DataTable SelectByCategoryID(int CategoryID)
        {
            String query = "Select * from Product WHERE CategoryID=@CategoryID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@CategoryID", CategoryID));

            return DataAccess.SelectData(query, lstprms);
        }

        public List<Product> SelectListByCategoryID(int CategoryID)
        {
            String query = "Select * from Product WHERE CategoryID=@CategoryID";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@CategoryID", CategoryID));
            DataTable dt = DataAccess.SelectData(query, lstprms);

            List<Product> lstProduct = new List<Product>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Product P = new Product();
                P.ProductID = Convert.ToInt32(dt.Rows[i]["ProductID"]);
                P.Name = dt.Rows[i]["Name"].ToString();
                P.Price = Convert.ToSingle(dt.Rows[i]["Price"]);
                P.Photo = dt.Rows[i]["Photo"].ToString();
                P.Description = dt.Rows[i]["Description"].ToString();
                P.CategoryID = Convert.ToInt32(dt.Rows[i]["CategoryID"]);
                P.ProductCode = Convert.ToInt32(dt.Rows[i]["ProductCode"]);
                lstProduct.Add(P);
            }

            return lstProduct;
        }
        public List<Product> SelectAllList()
        {
            String query = "Select * from Product";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstprms);

            List<Product> lstProduct = new List<Product>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Product P = new Product();
                P.ProductID = Convert.ToInt32(dt.Rows[i]["ProductID"]);
                P.Name = dt.Rows[i]["Name"].ToString();
                P.Price = Convert.ToSingle(dt.Rows[i]["Price"]);
                P.Photo = dt.Rows[i]["Photo"].ToString();
                P.Description = dt.Rows[i]["Description"].ToString();
                P.CategoryID = Convert.ToInt32(dt.Rows[i]["CategoryID"]);
                P.ProductCode = Convert.ToInt32(dt.Rows[i]["ProductCode"]);
                lstProduct.Add(P);
            }

            return lstProduct;
        }

    }
}