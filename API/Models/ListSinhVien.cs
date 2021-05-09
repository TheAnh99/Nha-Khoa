using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class ListSinhVien
    {
        public static List<ListSinhVien> sinhvien = new List<ListSinhVien>() { 
            new ListSinhVien(){StudentCode = "SV001", StudentName ="Hoang The Anh", Email="Anh@gmail.com",Mobile="012345678",Address="YB"},
            new ListSinhVien(){StudentCode = "SV002", StudentName ="Nguyen Quang Huy", Email="Huy@gmail.com",Mobile="096986874",Address="HN"},
            new ListSinhVien(){StudentCode = "SV003", StudentName ="Dai Trinh Loi", Email="Loi@gmail.com",Mobile="090986875",Address="BN"},
            new ListSinhVien(){StudentCode = "SV004", StudentName ="Giang Van Thai", Email="Thai@gmail.com",Mobile="098687573",Address="HY"},
            new ListSinhVien(){StudentCode = "SV005", StudentName ="Nguyen Thi Thuy", Email="Thuy@gmail.com",Mobile="097867543",Address="VP"},
            new ListSinhVien(){StudentCode = "SV006", StudentName ="Hoang Thi Mai", Email="Mai@gmail.com",Mobile="096785754",Address="PT"},
            new ListSinhVien(){StudentCode = "SV007", StudentName ="Chau Tinh Tri", Email="Tri@gmail.com",Mobile="098675343",Address="TN"},
            new ListSinhVien(){StudentCode = "SV008", StudentName ="Phan Huy Anh", Email="Anh@gmail.com",Mobile="097544322",Address="ND"},
            new ListSinhVien(){StudentCode = "SV009", StudentName ="Nguyen Van Nam", Email="Nam@gmail.com",Mobile="097567654",Address="NB"},
            new ListSinhVien(){StudentCode = "SV0010", StudentName ="Hoang Van Manh", Email="Manh@gmail.com",Mobile="09867543",Address="HN"}
        } ;
        /// <summary>
        /// Mã sinh viên
        /// </summary>
        public string StudentCode {get; set;}
        /// <summary>
        /// Tên sinh viên
        /// </summary>
        public string StudentName { get; set;}
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }

    }
}