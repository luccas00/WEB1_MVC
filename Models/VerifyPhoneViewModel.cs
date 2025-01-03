using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LuccasCorp.Models
{
    public class VerifyPhoneViewModel
    {
        public string PhoneNumber { get; set; }
        public string Code { get; set; }

        public string UserId { get; set; }
    }
}