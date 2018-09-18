
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication9.Models;
using Newtonsoft.Json;
namespace WebApplication9.Controllers
{
    public class StoreMobileAppController : Controller
    {
        // GET: StoreMobileApp
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            Store S = new Store();
            S.Username = Request.Params["Username"];
            S.Password = Request.Params["Password"];
            if (S.Authenticate())
            {
                return Content("STORE");
            }
            else
            {
                Salesman SM = new Salesman();
                SM.Username = Request.Params["Username"];
                SM.Password = Request.Params["Password"];
                if (SM.Authenticate())
                {
                    return Content("SALES");
                }
                else
                {
                    return Content("FAIL");
                }
            }
        }

        public ActionResult Locationupdate()
        {
            
                Salesman SM = new Salesman();
                SM.Username = Request.Params["un"];
                SM.Password = Request.Params["pw"];
            if (SM.Authenticate())
            {
                SM.GPS = Request.Params["lat"]+","+Request.Params["lon"];
                SM.Update();

                return Content("SUCCESS");

            }
            return Content("FAIL");
        
        }


        public ActionResult NewDemand()
        {
            Store S = new Store();
            S.Username = Request.Params["Username"];
            S.Password = Request.Params["Password"];
            if (S.Authenticate())
            {
                Demand D = new Demand();
                D.CreateDate = DateTime.Now;
                D.DemandReportID = 0;
                D.ProductID = Convert.ToInt32(Request.Params["ProductID"]);
                D.Quantity = Convert.ToInt32(Request.Params["Quantity"]);
                D.Status = "SUBMITTED";
                D.StoreID = S.StoreID;

                Product P = new Product();
                P.ProductID = D.ProductID;
                P.SelectById();
                D.Price = P.Price;

                D.Insert();

                return Content("SUCCESS");
            }
            else
            {
                return Content("FAIL");
            }
        }

        public ActionResult MyDemands()
        {
            Store S = new Store();
            S.Username = Request.Params["Username"];
            S.Password = Request.Params["Password"];
            if (S.Authenticate())
            {
                Demand D = new Demand();
                List<Demand> lstD = D.SelectByStoreID(S.StoreID);
                return Json(lstD, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Content("FAIL");
            }
        }

        public ActionResult MySupplies()
        {
            Store S = new Store();
            S.Username = Request.Params["Username"];
            S.Password = Request.Params["Password"];
            if (S.Authenticate())
            {
                Supply s = new Supply();
                List<Supply> lstS = s.SelectByStoreID(S.StoreID);
                return Json(lstS, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Content("FAIL");
            }
        }

        public ActionResult MyPayment()
        {
            Store S = new Store();
            S.Username = Request.Params["Username"];
            S.Password = Request.Params["Password"];
            if (S.Authenticate())
            {
                Payment P = new Payment();
                List<Payment> lstP = P.SelectByStoreID(S.StoreID);
                return Json(lstP, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Content("FAIL");
            }
        }

        

        public ActionResult StoreHistory()
        {
            int sid = Convert.ToInt32(Request.Params["StoreID"]);   
            Payment P = new Payment();
            List<Payment> lstP = P.SelectByStoreID(sid);
            return Json(lstP, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PendingPayment()
        {
            int sid = Convert.ToInt32(Request.Params["StoreID"]);
            Store s = new Store();
            int ans = s.FindPendingAmount(sid);
            return Content(ans + "");
        }



        public ActionResult GetProducts()
        {
      
                Product P = new Product();
            List<Product> lstP = P.SelectAllList();
                return Json(lstP, JsonRequestBehavior.AllowGet);
         
        }
       
        public ActionResult GetStores()
        {

            Store S = new Store();
            List<Store> lstP = S.SelectAllList();
            return Json(lstP, JsonRequestBehavior.AllowGet);

        }

        public ActionResult NewPayment()
        {
            Salesman S = new Salesman();
            S.Username = Request.Params["Username"];
            S.Password = Request.Params["Password"];
            if (S.Authenticate())
            {
                Payment P = new Payment();
                
                P.StoreID = Convert.ToInt32(Request.Params["StoreID"]);
                P.SalesmanID = S.SalesmanID;
                P.DistributorID = S.DistributorID;
                P.Amount = Convert.ToInt32(Request.Params["Amount"]);
                P.CreateDate = DateTime.Now;


                P.Insert();

                return Content("SUCCESS");
            }
            else
            {
                return Content("FAIL");
            }
        }

        public ActionResult RecievedPayment()
        {
            Salesman S = new Salesman();
            S.Username = Request.Params["Username"];
            S.Password = Request.Params["Password"];
            if (S.Authenticate())
            {
                Payment P = new Payment();
                List<Payment> lstP = P.SelectTodayBySalesmanID(S.SalesmanID);
                return Json(lstP, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Content("FAIL");
            }
        }

        public ActionResult SupplyItems()
        {
            Salesman S = new Salesman();
            S.Username = Request.Params["Username"];
            S.Password = Request.Params["Password"];
            if (S.Authenticate())
            {
                Supply s = new Supply();
                List<Supply> lstS = s.PendingSelectBySalesmanID(S.SalesmanID);
               
                return Content(JsonConvert.SerializeObject(lstS));
            }
            else
            {
                return Content("FAIL");
            }
        }

        public ActionResult Delivered()
        {
            Salesman S = new Salesman();
            S.Username = Request.Params["Username"];
            S.Password = Request.Params["Password"];
            if (S.Authenticate())
            {
                Supply s = new Supply();
                s.SupplyID = Convert.ToInt32(Request.Params["SupplyID"]);
                s.SelectByID();
                s.Status = "DELIVERED";
                s.Update();                

                return Content("Success");
            }
            else
            
                return Content("FAIL");
            }
        }



    }
