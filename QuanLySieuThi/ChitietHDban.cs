using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLySieuThi
{
    public class ChitietHDban
    {
        public string SPma { get; set; }
        public string Tensanpham { get; set; }
        public string Soluong { get; set; }
        public string Dongiaban { get; set; }
        public string Thanhtien { get; set; }
        public ChitietHDban(string masp, string ten, string SL, string dongia, string TT)
        {
            SPma = masp;
            Tensanpham = ten;
            Soluong = SL;
            Dongiaban = dongia;
            Thanhtien = TT;
        }
    }
}
