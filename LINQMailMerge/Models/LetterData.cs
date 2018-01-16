using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LINQMailMerge.Models
{
    public class LetterData
    {
        public string ReceiverName { get; set; }
        public string ReceiverEmail { get; set; }
        public string SenderName { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string SenderDesignation { get; set; }
        public DateTime SentDate { get; set; }
    }
}