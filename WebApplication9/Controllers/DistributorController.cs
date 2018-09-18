using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication9.Models;

namespace WebApplication9.Controllers
{
    public class DistributorController : Controller
    {
      
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["Distributor"] == null)
            {
                filterContext.Result = RedirectToAction("DistributorLogin", "Access");
            }
            else
            {
                base.OnActionExecuting(filterContext);
            }
        }


        [HttpGet]
        public ActionResult index()
        {
            return View();
        }
        public ActionResult SalesmanDetail(int ID)
        {

            DataTable dtDistributors= new Distributor().SelectAll();


            ViewBag.dtDistributors = dtDistributors;



            Salesman s = new Salesman();
            s.SalesmanID = ID;
            s.SelectByID();

            ViewBag.SalesmanUpdate = s;
            //view bag is nothing but a container
            DataTable dt = s.SelectAll();

            return View(dt);
        }

        public ActionResult SalesmanList()
        {
            Salesman s = new Salesman();
           

            ViewBag.SalesmanUpdate = s;
            //view bag is nothing but a container
            DataTable dt = s.SelectAll();
            //datatable must only be passsed when a table is defined on the view
            return View(dt);
        }
        [HttpPost]
        public ActionResult SalesmanDetail(FormCollection collection)
        {
            Salesman S = new Salesman();
            S.SalesmanID = Convert.ToInt32(collection["SalesmanID"]); //we passed the SalesmanID as a hidden value in Detail webpage


            //for performing insert operation. If there is no ID passed from the <EDIT> tag in detail webpage then 
            //it would go into the if loop .

            if (S.SalesmanID == 0)
            {
                S.Name = collection["Name"];
                S.Mobile = collection["Mobile"];
                S.Address = collection["Address"];
                S.Username = collection["Username"];
                S.Password = collection["Password"];
                S.DistributorID = Convert.ToInt32(collection["DistributorID"]);
                //S.CreateDate = Convert.ToDateTime(collection["CreateDate"]);
                S.CreateDate = DateTime.Now;

               //wait. lets check now.

                if (Request.Files["Photo"] != null)
                {
                    string path = "/Photos/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["Photo"].FileName;
                    Request.Files["Photo"].SaveAs(Server.MapPath(path));
                    //The MapPath method maps the specified relative or virtual path to the 
                    //corresponding physical directory on the server.
                    S.Photo = path;
                }

                S.Insert();
                return RedirectToAction("SalesmanList");

            }
            else
            {

                S.SelectByID();
                //here we selected the current values in database. and then we will repalce it with the values in the textboxes.

                S.Name = collection["Name"];
                S.Mobile = collection["Mobile"];
                S.Address = collection["Address"];
                S.Username = collection["Username"];
                S.Password = collection["Password"];
                S.DistributorID = Convert.ToInt32(collection["DistributorID"]);
                //S.CreateDate = Convert.ToDateTime(collection["CreateDate"]);
                S.CreateDate = DateTime.Now;


                if (Request.Files["Photo"].FileName.Length > 2)
                {
                    string path = "/Photos/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["Photo"].FileName;
                    Request.Files["Photo"].SaveAs(Server.MapPath(path));
                    //The MapPath method maps the specified relative or virtual path to the 
                    //corresponding physical directory on the server.
                    S.Photo = path;
                }
                S.Update();
                return RedirectToAction("SalesmanList");
            }

        }

        public ActionResult SalesmanDelete(int ID)
        {
            Salesman S = new Salesman();
            S.SalesmanID = ID;
            S.Delete();

            return RedirectToAction("SalesmanList");
        }

        [HttpGet]
        public ActionResult StoreDetail(int ID)
        {
            DataTable dtDistributors = new Distributor().SelectAll();


            ViewBag.dtDistributors = dtDistributors;


            Store s = new Store();
            s.StoreID = ID;
            s.SelectByID();

            ViewBag.StoreUpdate = s;
            //view bag is nothing but a container
            DataTable dt = s.SelectAll();

            return View(dt);
        }

        public ActionResult StoreList()
        {
            Store s = new Store();
           

            ViewBag.StoreUpdate = s;
            //view bag is nothing but a container
            DataTable dt = s.SelectAll();

            return View(dt);
        }
        [HttpPost]
        public ActionResult StoreDetail(FormCollection collection)
        {
            Store S = new Store();
            S.StoreID = Convert.ToInt32(collection["StoreID"]); //we passed the StoreID as a hidden value in Detail webpage


            //for performing insert operation. If there is no ID passed from the <EDIT> tag in detail webpage then 
            //it would go into the if loop .

            if (S.StoreID == 0)
            {
                S.Name = collection["Name"];
                S.Mobile = collection["Mobile"];
                S.Address = collection["Address"];
                S.Username = collection["Username"];
                S.Password = collection["Password"];
                S.StoreName = collection["StoreName"];
                S.DistributorID = Convert.ToInt32(collection["DistributorID"]);
                //S.CreateDate = Convert.ToDateTime(collection["CreateDate"]);
                S.CreateDate = DateTime.Now;

                if (Request.Files["Photo"] != null)
                {
                    string path = "/Photos/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["Photo"].FileName;
                    Request.Files["Photo"].SaveAs(Server.MapPath(path));
                    //The MapPath method maps the specified relative or virtual path to the 
                    //corresponding physical directory on the server.
                    S.Photo = path;
                }

                S.Insert();
                return RedirectToAction("StoreList");

            }
            else
            {

                S.SelectByID();
                //here we selected the current values in database. and then we will repalce it with the values in the textboxes.

                S.Name = collection["Name"];
                S.Mobile = collection["Mobile"];
                S.Address = collection["Address"];
                S.Username = collection["Username"];
                S.Password = collection["Password"];
                S.StoreName = collection["StoreName"];
                S.DistributorID = Convert.ToInt32(collection["DistributorID"]);
                S.CreateDate = DateTime.Now;


                if (Request.Files["Photo"].FileName.Length >2)
                {
                    string path = "/Photos/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["Photo"].FileName;
                    Request.Files["Photo"].SaveAs(Server.MapPath(path));
                    //The MapPath method maps the specified relative or virtual path to the 
                    //corresponding physical directory on the server.
                    S.Photo = path;
                }
                S.Update();
                return RedirectToAction("StoreList");
            }

        }

        public ActionResult StoreDelete(int ID)
        {
            Store S = new Store();
            S.StoreID = ID;
            S.Delete();

            return RedirectToAction("StoreList");
        }


        public ActionResult DemandReportDetail(int ID)
        {
            DataTable dtDistributors = new Distributor().SelectAll();


            ViewBag.dtDistributors = dtDistributors;


            DemandReport D = new DemandReport();
            D.DemandReportID = ID;
            D.SelectByID();

            ViewBag.DemandReportUpdate = D;
            //view bag is nothing but a container
            DataTable dt = D.SelectAll();

            return View(dt);
        }

        public ActionResult DemandReportList()
        {
            DemandReport d = new DemandReport();


            ViewBag.StoreUpdate = d;
            //view bag is nothing but a container
            DataTable dt = d.SelectAll();

            return View(dt);
        }
        [HttpPost]
        public ActionResult DemandReportDetail(FormCollection collection)
        {
            DemandReport D = new DemandReport();
            D.DemandReportID = Convert.ToInt32(collection["DemandReportID"]); //we passed the StoreID as a hidden value in Detail webpage


            //for performing insert operation. If there is no ID passed from the <EDIT> tag in detail webpage then 
            //it would go into the if loop .

            if (D.DemandReportID == 0)
            {
                

                D.DistributorID = Convert.ToInt32(collection["DistributorID"]);
                //S.CreateDate = Convert.ToDateTime(collection["CreateDate"]);
                D.CreateDate = DateTime.Now;
                D.Details = collection["Details"];
                D.Status = (collection["Status"] == "True");

               

                D.Insert();
                return RedirectToAction("DemandReportDetail/0");

            }
            else
            {

               D.SelectByID();
                //here we selected the current values in database. and then we will repalce it with the values in the textboxes.

                D.DistributorID = Convert.ToInt32(collection["DistributorID"]);
                //S.CreateDate = Convert.ToDateTime(collection["CreateDate"]);
                D.CreateDate = DateTime.Now;
                D.Details = collection["Details"];
                D.Status = (collection["Status"] == "True");

                D.Update();
                return RedirectToAction("DemandReportDetail/0");
            }

        }

        public ActionResult DemandReportDelete(int ID)
        {
            DemandReport D = new DemandReport();
            D.DemandReportID = ID;
            D.Delete();

            return RedirectToAction("DemandReportDetail/0");
        }

        public ActionResult DemandReportSearch()
        {
            DemandReport d = new DemandReport();
            DataTable dt = d.SelectAll();
            return View(dt);
        }

        [HttpPost]
        public ActionResult DemamdReportSearch(FormCollection collection)
        {
           
            ViewBag.From = collection["From"];
            ViewBag.To = collection["To"];

            DateTime from = DateTime.Today;
            if (!DateTime.TryParseExact(collection["From"], "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out from))
            {
                from = DateTime.Today;
            }
            DateTime to = DateTime.Today;
            if (!DateTime.TryParseExact(collection["To"], "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out to))
            {
                to = DateTime.Today;
            }
            Distributor Dist = (Distributor)Session["Distributor"];
            DemandReport d = new DemandReport();
            DataTable dt = d.Search(from, to, Dist.DistributorID);
            return View(dt);
        }
        
        public ActionResult DemandSearch()
        {
            Demand d = new Demand();
            DataTable dt = new DataTable(); //d.SelectAll();
            return View(dt);
        }


        [HttpPost]
        public ActionResult DemandSearch(FormCollection collection)
        {
            string Status = collection["Status"];
            ViewBag.From = collection["From"];
            ViewBag.To = collection["To"];

            DateTime from = DateTime.Today;
            if (!DateTime.TryParseExact(collection["From"], "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out from))
            {
                from = DateTime.Today;
            }
            DateTime to = DateTime.Today;
            if (!DateTime.TryParseExact(collection["To"], "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out to))
            {
                to = DateTime.Today;
            }
            Distributor Dist = (Distributor)Session["Distributor"];
            Demand d = new Demand();
            DataTable dt = d.Search(from, to, Dist.DistributorID, Status);

            return View(dt);
        }

        public ActionResult SupplyDetail(int ID)
        {
            Distributor d = (Distributor)Session["Distributor"];

            ViewBag.dtSalesman = new Salesman().SelectAllByDistributorID(d.DistributorID);
            ViewBag.dtStore = new Store().SelectAllByDistributorID(d.DistributorID);
            ViewBag.dtProduct = new Product().SelectAll();

         //   ViewBag.dtSalesman = dtSalesman;


            Supply S = new Supply();
            S.SupplyID = ID;
            S.SelectByID();

            ViewBag.SupplyUpdate = S;
            //view bag is nothing but a container
            DataTable dt = S.SelectAll();

            return View(dt);

         
        }


        [HttpPost]
        public ActionResult SupplyDetail(FormCollection collection)
        {

            Distributor Dist = (Distributor)Session["Distributor"];

            Supply S = new Supply();
            S.SupplyID = Convert.ToInt32(collection["SupplyID"]); //we passed the StoreID as a hidden value in Detail webpage


            //for performing insert operation. If there is no ID passed from the <EDIT> tag in detail webpage then 
            //it would go into the if loop .

            if (S.SupplyID == 0)
            {
                               
                S.SalesmanID = Convert.ToInt32(collection["SalesmanID"]);
                S.DistributorID = Dist.DistributorID;
                S.StoreID = Convert.ToInt32(collection["StoreID"]);
                //S.CreateDate = Convert.ToDateTime(collection["CreateDate"]);
                S.CreateDate = DateTime.Now;
                S.ProductID = Convert.ToInt32(collection["ProductID"]);
                Product p = new Product();
                p.ProductID = S.ProductID;
                p.SelectById();
            

                S.Quantity = Convert.ToInt32(collection["Quantity"]);
                S.Price = p.Price;
                S.Status = collection["Status"].ToString();
                
                S.Insert();
                return RedirectToAction("SupplySearch");

            }
            else
            {

                S.SelectByID();
                //here we selected the current values in database. and then we will repalce it with the values in the textboxes.

                S.SalesmanID = Convert.ToInt32(collection["SalesmanID"]);
                S.DistributorID = Dist.DistributorID;
                S.StoreID = Convert.ToInt32(collection["StoreID"]);
                //S.CreateDate = Convert.ToDateTime(collection["CreateDate"]);
                S.CreateDate = DateTime.Now;
                S.ProductID = Convert.ToInt32(collection["ProductID"]);
                S.Quantity = Convert.ToInt32(collection["Quantity"]);
                S.Price = Convert.ToSingle(collection["Price"]);
                S.Status = collection["Status"].ToString();


                S.Update();
                return RedirectToAction("SupplySearch");

            }

        }

        public ActionResult SupplySearch()
        {
            Supply S = new Supply();
            DataTable dt = new DataTable(); //d.SelectAll();
            return View(dt);
        }

        [HttpPost]
        public ActionResult SupplySearch(FormCollection collection)
        {
            string Status = collection["Status"];
            ViewBag.From = collection["From"];
            ViewBag.To = collection["To"];

            DateTime from = DateTime.Today;
            if (!DateTime.TryParseExact(collection["From"], "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out from))
            {
                from = DateTime.Today;
            }
            DateTime to = DateTime.Today;
            if (!DateTime.TryParseExact(collection["To"], "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out to))
            {
                to = DateTime.Today;
            }
            Distributor Dist = (Distributor)Session["Distributor"];
            Supply S = new Supply();
            DataTable dt = S.Search(from, to, Dist.DistributorID, Status);

            return View(dt);
        }


        public ActionResult PaymentSearch()
        {

            Distributor d = (Distributor)Session["Distributor"];


            DataTable dt1 = new Salesman().SelectAllByDistributorID(d.DistributorID);
            dt1.Rows.Add("0","All");
            dt1.DefaultView.Sort = "SalesmanId";
            ViewBag.dtSalesman = dt1;
            DataTable dt2 = new Store().SelectAllByDistributorID(d.DistributorID);
            dt2.Rows.Add("0", "All");
            dt2.DefaultView.Sort = "StoreId";
            ViewBag.dtStore = dt2;



            Payment P = new Payment();
            DataTable dt = new DataTable(); //d.SelectAll();
            return View(dt);
        }

        [HttpPost]
        public ActionResult PaymentSearch(FormCollection collection)
        {


            Distributor d = (Distributor)Session["Distributor"];


            DataTable dt1 = new Salesman().SelectAllByDistributorID(d.DistributorID);
            dt1.Rows.Add("0", "All");
            dt1.DefaultView.Sort = "SalesmanId";
            ViewBag.dtSalesman = dt1;
            DataTable dt2 = new Store().SelectAllByDistributorID(d.DistributorID);
            dt2.Rows.Add("0", "All");
            dt2.DefaultView.Sort = "StoreId";
            ViewBag.dtStore = dt2;



            int StoreID = Convert.ToInt32(collection["StoreID"]);
            int SalesmanID = Convert.ToInt32(collection["SalesmanID"]);
            ViewBag.StoreID = collection["StoreID"];
            ViewBag.SalesmanID = collection["SalesmanID"];
            ViewBag.From = collection["From"];
            ViewBag.To = collection["To"];

            DateTime from = DateTime.Today;
            if (!DateTime.TryParseExact(collection["From"], "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out from))
            {
                from = DateTime.Today;
            }
            DateTime to = DateTime.Today;
            if (!DateTime.TryParseExact(collection["To"], "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out to))
            {
                to = DateTime.Today;
            }
            Distributor Dist = (Distributor)Session["Distributor"];
            Payment p = new Payment();
            DataTable dt = p.Search(from, to, Dist.DistributorID, StoreID,SalesmanID);

            return View(dt);
        }

    }
}
