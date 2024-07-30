using Nhom4_CuaHangAnPhuoc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nhom4_CuaHangAnPhuoc.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        QLQuanAoDataContext db = new QLQuanAoDataContext();

        public ActionResult GioRong()
        {
            return View();
        }
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;

            }

            return lstGioHang;
        }
        public ActionResult ThemGioHang(string ms, string strURL)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang SanPham = lstGioHang.Find(sp => sp.iMaSP == ms);
            if (SanPham == null)
            {
                SanPham = new GioHang(ms);
                lstGioHang.Add(SanPham);
                return Redirect(strURL);
            }
            else
            {
                SanPham.iSoLuong++;
                return Redirect(strURL);
            }
        }

        private int TongSoLuong()
        {
            int tsl = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                tsl = lstGioHang.Sum(sp => sp.iSoLuong);
            }
            return tsl;
        }

        private double TongThanhTien()
        {
            double tt = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                tt += lstGioHang.Sum(sp => sp.dThanhTien);
            }
            return tt;
        }

        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("GioRong", "GioHang");
            }

            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongThanhTien = TongThanhTien();

            return View(lstGioHang);
        }

        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            return View();
        }

        public ActionResult XoaGioHang(string MaSP)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.Single(s => s.iMaSP == MaSP);
            if (sp != null)
            {
                lstGioHang.RemoveAll(s => s.iMaSP == MaSP);
                return RedirectToAction("GioHang", "GioHang");
            }
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("GioRong", "GioHang");
            }

            return RedirectToAction("GioHang", "GioHang");
        }

        public ActionResult XoaGioHang_All()
        {
            List<GioHang> lstGioHang = LayGioHang();

            if (lstGioHang.Count > 0)
            {

                lstGioHang.Clear();
                return RedirectToAction("GioRong", "GioHang");
            }
            return RedirectToAction("GioHang", "GioHang");
        }

        public ActionResult CapNhapGioHang(string MaSP, FormCollection f)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.Single(s => s.iMaSP == MaSP);
            if (sp != null)
            {
                sp.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }

            return RedirectToAction("GioHang", "GioHang");
        }

        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "TaiKhoan");
            }

            if (Session["TaiKhoan"] == null)
            {
                return RedirectToAction("ShowProductAoThun", "Product");
            }

            List<GioHang> lsiGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongThanhTien();
            return View(lsiGioHang);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection f)
        {
            HOADON dh = new HOADON();
            KHACHHANG kh = (KHACHHANG)Session["TaiKhoan"];
            List<GioHang> gh = LayGioHang();
            dh.MAKH = kh.MAKH;
            dh.NGAYDAT = DateTime.Now;
            var ngayGiao = string.Format("{0:MM/dd/yyyy}", f["NgayGiao"]);
            dh.NGAYGIAO = DateTime.Parse(ngayGiao);
            dh.TINHTRANGGIAOHANG = false;
            dh.DATHANHTOAN = false;
            db.HOADONs.InsertOnSubmit(dh);
            db.SubmitChanges();

            foreach (var item in gh)
            {
                CT_HOADON ctdh = new CT_HOADON();
                ctdh.MAHD = dh.MAHD;
                ctdh.MASP = item.iMaSP;
                ctdh.SOLUONG = item.iSoLuong;
                ctdh.DONGIA = (decimal)item.sDonGia;
                db.CT_HOADONs.InsertOnSubmit(ctdh);
            }
            db.SubmitChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XacNhanDonHang", "GioHang");
        }

        public ActionResult XacNhanDonHang()
        {
            return View();
        }

    }
}