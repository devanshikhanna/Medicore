using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace WebApplication9.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public String Name { get; set; }
        public String Photo { get; set; }

        public int InsertCategory()
        {
            String query = "Insert into Category values(@Name, @Photo)";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@Name", this.Name));
            lstprms.Add(new SqlParameter("@Photo", this.Photo));

            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public int UpdateCategory()
        {
            string query = "Update Category set Name=@Name,Photo=@Photo where CategoryId=@CategoryId";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@Name", this.Name));
            lstprms.Add(new SqlParameter("@Photo", this.Photo));
            lstprms.Add(new SqlParameter("@CategoryId", this.CategoryId));
            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public int DeleteCategory()
        {
            String query = "Delete from Category where CategoryId=@CategoryId";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@CategoryId", this.CategoryId));
            int x = DataAccess.ModifyData(query, lstprms);
            return x;
        }
        public DataTable SelectAllCategory()
        {
            String query = "Select * from Category";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstprms);
            return dt;
        }
        public List<Category> SelectAllCategoryList()
        {
            String query = "Select * from Category";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            DataTable dt = DataAccess.SelectData(query, lstprms);
            List<Category> lstCat = new List<Category>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Category C = new Category();
                C.CategoryId = Convert.ToInt32(dt.Rows[i]["CategoryId"]);
                C.Name = dt.Rows[i]["Name"].ToString();
                C.Photo = dt.Rows[0]["Photo"].ToString();
                lstCat.Add(C);
            }
            return lstCat;
        }
        public void SelectCategoryById()
        {
            String query = "Select * from Category where CategoryId=@CategoryId";
            List<SqlParameter> lstprms = new List<SqlParameter>();
            lstprms.Add(new SqlParameter("@CategoryId", this.CategoryId));
            DataTable dt = DataAccess.SelectData(query, lstprms);
            if (dt.Rows.Count > 0)
            {
                this.Name = dt.Rows[0]["Name"].ToString();
                this.Photo = dt.Rows[0]["Photo"].ToString();
            }
        }

    }
}
    