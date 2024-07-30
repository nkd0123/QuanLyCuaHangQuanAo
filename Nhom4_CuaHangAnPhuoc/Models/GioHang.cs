using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Nhom4_CuaHangAnPhuoc.Models
{
    public class GioHang
    {
        QLQuanAoDataContext db = new QLQuanAoDataContext();
        public string iMaSP { get; set; }
        public string sTenSP { set; get; }
        public string sAnhBia { set; get; }
        public double sDonGia { set; get; }
        public int iSoLuong { set; get; }
        public double dThanhTien
        {
            get { return iSoLuong * sDonGia; }
        }
        public GioHang(string maSP)
        {
           iMaSP = maSP;
            SANPHAM sp = db.SANPHAMs.Single(n => n.MASP == iMaSP);
            sTenSP = sp.TENSP;
      
            sAnhBia = sp.ANH;
            sDonGia = double.Parse(sp.GIA.ToString());
            iSoLuong = 1;
        }
    }

   
}