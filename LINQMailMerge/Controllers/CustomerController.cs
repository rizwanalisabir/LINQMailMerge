using LINQMailMerge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LINQMailMerge.Controllers
{
    public class CustomerController : Controller
    {
        public ActionResult New(FormCollection fc)
        {
            List<Customer> cusList = new List<Customer>();
            using (dbEntities db = new dbEntities())
            {
                if (fc.Count > 0)
                {
                    // creating instance of customer model class
                    try
                    {
                        Customer cus = new Customer();
                        string name = fc["name"];
                        string email = fc["email"];
                        cus.Email = email;
                        cus.Name = name;
                        db.Customers.Add(cus);
                        db.SaveChanges();
                        ViewBag.Message = "Customer was successfully added";
                        return View(db.Customers.ToList());
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "Some error occurred while adding new customer " + ex.Message;
                        return View(db.Customers.ToList());
                    }
                }
                else
                {
                    return View(db.Customers.ToList());
                }
            }
        }
    }
}