using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication9.Models;

namespace WebApplication9.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin

            

        [HttpGet]
        public ActionResult StaffDetail(int ID)
        {
            Staff s = new Staff();
            s.StaffID = ID;

            s.SelectByID();

            ViewBag.StaffUpdate = s;
            //view bag is nothing but a container
            DataTable dt = s.SelectAll();

            return View(dt);
        }
        
        public ActionResult index()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();


            return RedirectToAction("/Home");
        }
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult StaffList()
        {
            Staff s = new Staff();
            //s.StaffID = ID;
            //s.SelectByID();

            ViewBag.StaffUpdate = s;
            //view bag is nothing but a container
            DataTable dt = s.SelectAll();

            return View(dt);
        }


        [HttpPost]
        public ActionResult StaffDetail(FormCollection collection)
        {
            Staff S = new Staff();
            S.StaffID = Convert.ToInt32(collection["StaffID"]); //we passed the StaffID as a hidden value in Detail webpage


            //for performing insert operation. If there is no ID passed from the <EDIT> tag in detail webpage then 
            //it would go into the if loop .

            if (S.StaffID == 0)
            {
                S.Name = collection["Name"];
                S.Email = collection["Email"];
                S.Mobile = collection["Mobile"];
                S.Address = collection["Address"];
                S.City = collection["City"];
                S.Username = collection["Username"];
                S.Password = collection["Password"];
               // S.DepartmentID = Convert.ToInt32(collection["DepartmentID"]);
                S.Salary = Convert.ToInt32(collection["Salary"]);
                S.Status = Convert.ToString(collection["Status"] == "True");

                if (Request.Files["Photo"] != null)
                {
                    string path = "/Photos/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["Photo"].FileName;
                    Request.Files["Photo"].SaveAs(Server.MapPath(path));
                    //The MapPath method maps the specified relative or virtual path to the 
                    //corresponding physical directory on the server.
                    S.Photo = path;
                }

                S.Insert();
                return RedirectToAction("StaffList");

            }
            else
            {

                S.SelectByID();
                //here we selected the current values in database. and then we will repalce it with the values in the textboxes.
                S.Name = collection["Name"];
                S.Email = collection["Email"];
                S.Mobile = collection["Mobile"];
                S.Address = collection["Address"];
                S.City = collection["City"];
                S.Username = collection["Username"];
                S.Password = collection["Password"];
              //  S.DepartmentID = Convert.ToInt32(collection["DepartmentID"]);
                S.Salary = Convert.ToInt32(collection["Salary"]); ;
                S.Status = Convert.ToString(collection["Status"] == "True");


                if (Request.Files["Photo"].FileName.Length>2)
                {
                    string path = "/Photos/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["Photo"].FileName;
                    Request.Files["Photo"].SaveAs(Server.MapPath(path));
                    //The MapPath method maps the specified relative or virtual path to the 
                    //corresponding physical directory on the server.
                    
                }
                S.Update();
                return RedirectToAction("StaffList");
            }

        }

        public ActionResult StaffDelete(int ID)
        {
            Staff S = new Staff();
            S.StaffID = ID;
            S.Delete();

            return RedirectToAction("StaffList");
        }


        public ActionResult ProductDetail(int ID)
        {

            DataTable dtCategories = new Category().SelectAllCategory();


            ViewBag.dtCategories = dtCategories;


            Product p = new Product();
            p.ProductID = ID;
            p.SelectById();

            ViewBag.ProductUpdate = p;
            //view bag is nothing but a container
            DataTable dt = p.SelectAll();

            return View(dt);
       
        }

        public ActionResult ProductList()
        {
            Product p = new Product();
           

           
            //view bag is nothing but a container
            DataTable dt = p.SelectAll();

            return View(dt);

        }
        [HttpPost]
       
        public ActionResult ProductDetail(FormCollection collection)
        {
            Product P = new Product();
            P.ProductID = Convert.ToInt32(collection["ProductID"]); //we passed the StaffID as a hidden value in Detail webpage


            //for performing insert operation. If there is no ID passed from the <EDIT> tag in detail webpage then 
            //it would go into the if loop .

            if (P.ProductID == 0)
            {
                P.Name = collection["Name"];
                P.Price = Convert.ToSingle(collection["Price"]);
                P.Photo= collection["Photo"];
                P.Description = collection["Description"];
                P.CategoryID = Convert.ToInt32(collection["CategoryID"]);
                P.ProductCode = Convert.ToInt32(collection["ProductCode"]);


                if (Request.Files["Photo"] != null)
                {
                    string path = "/Photos/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["Photo"].FileName;
                    Request.Files["Photo"].SaveAs(Server.MapPath(path));
                    //The MapPath method maps the specified relative or virtual path to the 
                    //corresponding physical directory on the server.
                    P.Photo = path;
                }

                P.Insert();
                return RedirectToAction("ProductList");

            }
            else
            {

                P.SelectById();
                //here we selected the current values in database. and then we will repalce it with the values in the textboxes.
                P.Name = collection["Name"];
                P.Price = Convert.ToSingle(collection["Price"]);
                
                P.Description = collection["Description"];
                P.CategoryID = Convert.ToInt32(collection["CategoryID"]);
                P.ProductCode = Convert.ToInt32(collection["ProductCode"]);

                if (Request.Files["Photo"].FileName.Length>2)
                {
                    string path = "/Photos/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["Photo"].FileName;
                    Request.Files["Photo"].SaveAs(Server.MapPath(path));
                    //The MapPath method maps the specified relative or virtual path to the 
                    //corresponding physical directory on the server.
                    P.Photo = path;
                }
                P.Update();
                return RedirectToAction("ProductList");
            }

        }

        public ActionResult ProductDelete(int ID)
        {
            Product P = new Product();
            P.ProductID = ID;
            P.Delete();

            return RedirectToAction("ProductList");
        }

      

        public ActionResult CategoryDetail(int ID)
        {
            Category c = new Category();
            c.CategoryId = ID;
            c.SelectCategoryById();

            ViewBag.CategoryUpdate = c;
            //view bag is nothing but a container

            DataTable dt = c.SelectAllCategory();

            return View(c);
     
        }
        [HttpPost]
        public ActionResult CategoryDetail(FormCollection collection)
        {

            Category c = new Category();
            c.CategoryId = Convert.ToInt32(collection["CategoryId"]);


            //for performing insert operation. If there is no ID passed from the <EDIT> tag in detail webpage then 
            //it would go into the if loop .

            if (c.CategoryId == 0)
            {
                c.Name = collection["Name"];

                if (Request.Files["Photo"] != null)
                {
                    string path = "/Photos/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["Photo"].FileName;
                    Request.Files["Photo"].SaveAs(Server.MapPath(path));
                    c.Photo = path;
                }
                c.InsertCategory();
                return RedirectToAction("CategoryList");
            }
            else
            {

                c.SelectCategoryById();
                c.Name = collection["Name"];
                if (Request.Files["Photo"].FileName.Length >2)
                {
                    string path = "/Photos/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["Photo"].FileName;
                    Request.Files["Photo"].SaveAs(Server.MapPath(path));
                    c.Photo = path;

                }
                c.UpdateCategory();
                return RedirectToAction("CategoryList");

            }
        
        }


    
        public ActionResult CategoryList()
        {

            Category c = new Category();
           
            ViewBag.CategoryUpdate = c;
            //view bag is nothing but a container
            DataTable dt = c.SelectAllCategory();

            return View(dt);
        }

     
        public ActionResult CategoryDelete(int ID)
        {
            Category C = new Category();
            C.CategoryId = ID;
            C.DeleteCategory();

            return RedirectToAction("CategoryList");
        }

        [HttpGet]
        public ActionResult DistributorDetail(int ID)
        {
            Distributor d = new Distributor();
            d.DistributorID = ID;
            d.SelectByID();

            ViewBag.DistributorUpdate = d;
            //view bag is nothing but a container
            DataTable dt = d.SelectAll();

            return View(dt);
        }

        public ActionResult DistributorList()
        {
            Distributor d = new Distributor();
            //d.DistributorID = ID;
            //d.SelectByID();

            ViewBag.DistributorUpdate = d;
            //view bag is nothing but a container
            DataTable dt = d.SelectAll();

            return View(dt);
        }

        [HttpPost]
        public ActionResult DistributorDetail(FormCollection collection)
        {
            Distributor D = new Distributor();
            D.DistributorID = Convert.ToInt32(collection["DistributorID"]); //we passed the DistributorID as a hidden value in Detail webpage


            //for performing insert operation. If there is no ID passed from the <EDIT> tag in detail webpage then 
            //it would go into the if loop .

            if (D.DistributorID == 0)
            {
                D.Name = collection["Name"]; 
               
                D.Mobile = collection["Mobile"];
                D.Address = collection["Address"];
                D.Username = collection["Username"];
                D.Password = collection["Password"];
                //    D.CreateDate = Convert.ToDateTime(collection["CreateDate"]);
                D.CreateDate = DateTime.Now;
              
                if (Request.Files["Photo"] != null)
                {
                    string path = "/Photos/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["Photo"].FileName;
                    Request.Files["Photo"].SaveAs(Server.MapPath(path));
                    //The MapPath method maps the specified relative or virtual path to the 
                    //corresponding physical directory on the server.
                   D.Photo = path;
                }

                D.Insert();
                return RedirectToAction("DistributorList");

            }
            else
            {

                D.SelectByID();
                //here we selected the current values in database. and then we will repalce it with the values in the textboxes.
                D.Name = collection["Name"];
                D.Mobile = collection["Mobile"];
                D.Address = collection["Address"];
                D.Username = collection["Username"];
                D.Password = collection["Password"];
                // D.CreateDate = Convert.ToDateTime(collection["CreateDate"]);
                D.CreateDate = DateTime.Now;
                if (Request.Files["Photo"].FileName.Length>2)
                {
                    string path = "/Photos/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["Photo"].FileName;
                    Request.Files["Photo"].SaveAs(Server.MapPath(path));
                    //The MapPath method maps the specified relative or virtual path to the 
                    //corresponding physical directory on the server.
                    D.Photo = path;
                }

                // yaha bhi photo kaa daalna padega na? 
                //actually ye action edit and insert dono ke liye banaya gaya hai . :| soooo...if there is an id to vo else part me ayega
                //if not to v insert hi karega. if u want to make 2 things i.e edit and insert in same page then u can put everthing same as in if.



                D.Update();
                return RedirectToAction("DistributorList");
            }

        }

        public ActionResult DistributorDelete(int ID)
        {
            Distributor D = new Distributor();
            D.DistributorID = ID;
            D.Delete();

            return RedirectToAction("DistributorList");
        }
    }
}