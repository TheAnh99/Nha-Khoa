using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Common.Models
{
    public class SanPham
    {
        public int ID { get; set; }
        public string Tensp { get; set; }
        public double Gia { get; set; }
        public string MoTaSP { get; set; }

        public string Anh { get; set; }
    }
}