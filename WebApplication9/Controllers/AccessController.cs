using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication9.Models;

namespace WebApplication9.Controllers
{

    public class AccessController : Controller
    {
        public ActionResult StaffLogin()
        {
            Session["Staff"] = null;
            Session["Distributor"] = null;
            return View();
        }

        [HttpPost]
        public ActionResult StaffValidate(FormCollection collection)
        {
            Staff S = new Staff();
            S.Username = collection["Username"];
            S.Password = collection["Password"];
              if (S.Authenticate())
                   //where is this method/.
               {
                   Session["Staff"] = S;
                   return RedirectToAction("index", "Admin");
               }
               else
               {
                   return RedirectToAction("StaffLogin");
               }
            
        }


        public ActionResult DistributorLogin()
        {
            Session["Staff"] = null;
            Session["Distributor"] = null;
            return View();
        }
        
        [HttpPost]
        public ActionResult DistributorValidate(FormCollection collection)
        {
            Distributor D = new Distributor();
            D.Username = collection["Username"];
            D.Password = collection["Password"];
            if (D.Authenticate())
            {
                Session["Distributor"] = D;
                return RedirectToAction("index", "Distributor");
            }
            else
            {
                return RedirectToAction("DistributorLogin");
            }

        }

    }
}