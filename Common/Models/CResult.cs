using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Models
{
    public class CResult
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public object Data { get; set; }
    }

  
}