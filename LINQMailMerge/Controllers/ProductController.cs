using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LINQMailMerge.Models;

namespace LINQMailMerge.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Update(FormCollection fc)
        {
            using (dbEntities db = new dbEntities())
            {
                ProductInformation _product = db.ProductInformations.FirstOrDefault();
                if (fc.Count > 0)
                {
                    try
                    {
                        string productName = fc["productname"];
                        string productInformation = fc["productdescription"];
                        string company = fc["company"];
                        string senderDesignation = fc["senderdesignation"];
                        string date = fc["date"];
                        _product.ProductDescription = productInformation;
                        _product.ProductName = productName;
                        _product.LetterFrom = company;
                        _product.SenderDesignation = senderDesignation;
                        _product.SentDate = Convert.ToDateTime(date);
                        db.SaveChanges();
                        ViewBag.Message = "Product information was successfully updated";
                        return View(db.ProductInformations.FirstOrDefault());
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "Some error occurred while updating product information " + ex.Message;
                        return View(db.ProductInformations.FirstOrDefault());
                    }
                }
                else
                {
                    return View(_product);
                }
            }
        }
    }
}