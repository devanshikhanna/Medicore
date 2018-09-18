using WebApplication9.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication9.Controllers
{
    public class StaffController : Controller
    {
        // acontrolleraname/methodname
      [HttpGet]
        public ActionResult Detail(int ID)
        {
            Staff s  = new Staff();
            s.StaffID = ID;
            s.SelectByID();
              
            ViewBag.StaffUpdate = s;
            //view bag is nothing but a container
            DataTable dt = s.SelectAll();

            Store st = new Store();
            s.SelectByID();
            //store details
            ViewBag.StoreUpdate = st;


            return View(dt);
        }


        [HttpPost]
        public ActionResult Detail(FormCollection collection)
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
                //S.DepartmentID = Convert.ToInt16(collection["DepartmentID"]);
                S.Salary = Convert.ToInt16(collection["Salary"]); ;
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
                return RedirectToAction("Detail/0");

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
                //S.DepartmentID = Convert.ToInt16(collection["DepartmentID"]);
                S.Salary = Convert.ToInt16(collection["Salary"]); ;
                S.Status = Convert.ToString(collection["Status"] == "True");


                if (Request.Files["Photo"] != null)
                {
                    string path = "/Photos/" + DateTime.Now.Ticks.ToString() + "_" + Request.Files["Photo"].FileName;
                    Request.Files["Photo"].SaveAs(Server.MapPath(path));
                    //The MapPath method maps the specified relative or virtual path to the 
                    //corresponding physical directory on the server.
                    S.Photo = path;
                }
                S.Update();
                return RedirectToAction("Detail/0");
            }

        }
        public ActionResult Delete(int ID)
        {
            Staff S = new Staff();
            S.StaffID = ID;
            S.Delete();

            return RedirectToAction("Detail/0");
        }

       
    }
    
}