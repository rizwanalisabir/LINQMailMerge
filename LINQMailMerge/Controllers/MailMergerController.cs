using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LINQMailMerge.Models;
using Aspose.Words.Reporting;
using Aspose.Words;
using System.Data;
using LINQMailMerge.Common;

namespace LINQMailMerge.Controllers
{
    public class MailMergerController : Controller
    {
        // GET: MailMerger
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Send()
        {
            var cusList = new List<Customer>();
            var newProduct = new ProductInformation();
            var letterDataList = new List<LetterData>();
            try
            {
                using (dbEntities db = new dbEntities())
                {
                    // geting list of customers
                    cusList = db.Customers.ToList();
                    // geting new product information
                    newProduct = db.ProductInformations.OrderByDescending(x => x.Id).Take(1).FirstOrDefault();
                }

                // sending letters to all customers
                foreach (var cus in cusList)
                {
                    LetterData ld = new LetterData();
                    ReportingEngine engine = new ReportingEngine();
                    DataSet dsMailMerge = new DataSet();
                    string dataDir = Server.MapPath("~/Document/Template/New Product Introduction Letter.docx");
                    // Loding doc template
                    Document doc = new Document(dataDir);
                    // building dataset
                    ld = new LetterData
                    {
                        ProductDescription = newProduct.ProductDescription,
                        ProductName = newProduct.ProductName,
                        ReceiverName = cus.Name,
                        SenderDesignation = WebUtility.senderDesignation,
                        SenderName = WebUtility.senderName,
                        SentDate = DateTime.Now
                    };

                    // Execute the build report.
                    engine.BuildReport(doc, ld, "ld");
                    string uniqueDoc = Server.MapPath("~/Document/Unique/" + cus.Name + ".docx");
                    // Save the document to disk.
                    doc.Save(uniqueDoc);
                    // Sending news letter using smtp
                    if (GeneralFunctions.SendEmail(cus, uniqueDoc, ld.ProductName))
                    {
                        return View("Success");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
            return View("About");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("Error");
            }
        }

        // newsletter template
        public ActionResult Template()
        {
            return View();
        }

        public ActionResult Success()
        {
            return View();
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}