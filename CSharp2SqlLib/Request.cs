using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp2SqlLib
{
    public class Request
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string RejectionReason { get; set; }
        public string DeliveryMode { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }
        public int UserId { get; set; }




    }
}
