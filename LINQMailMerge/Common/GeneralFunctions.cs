using Aspose.Words;
using LINQMailMerge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINQMailMerge.Common
{
    public static class GeneralFunctions
    {
        public static bool SendEmail(Customer cus, string doc, string productName)
        {
            try
            {
                // MailMessage instance
                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

                // use MailMessage properties like specify sender, recipient and message
                msg.To.Add(new System.Net.Mail.MailAddress(cus.Email));

                msg.From = new System.Net.Mail.MailAddress(WebUtility.senderEmail);

                msg.Subject = productName +" has arrived. Order now before we run out.";

                msg.Attachments.Add(new System.Net.Mail.Attachment(doc));

                var client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new System.Net.NetworkCredential(WebUtility.senderEmail, WebUtility.emailPassword),
                    EnableSsl = true
                };
                client.Send(msg);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}