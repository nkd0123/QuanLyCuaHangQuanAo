using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nhom4_CuaHangAnPhuoc.Models;

namespace Nhom4_CuaHangAnPhuoc.Controllers
{
    public class TaiKhoanController : Controller
    {
        // GET: TaiKhoan
        QLQuanAoDataContext db = new QLQuanAoDataContext();
        public ActionResult DangKy()
        {


            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection f, KHACHHANG kh)
        {

            var hoten = f["HoTenKh"];
            var tendn = f["TenDN"];
            var matkhau = f["password"];
            var nhaplaimatkhau = f["RePass"];
            var sdt = f["SDT"];

            var email = f["email"];
            var diachi = f["diachi"];

   
            if (string.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Ho Ten Khach Hang khong duoc de trong";
            }
            if (string.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Phai Nhap Ten Dang Nhap";

            }
            if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Phai Nhap Mat Khau";
            }
            if (string.IsNullOrEmpty(nhaplaimatkhau))
            {
                ViewData["Loi4"] = "Phai Nhap Lai Mat Khau";
            }
            if (string.IsNullOrEmpty(sdt))
            {
                ViewData["Loi5"] = "Phai Nhap So Dien Thoai";
            }
            if (string.IsNullOrEmpty(email))
            {
                ViewData["Loi6"] = "Phai Nhap Email";
            }
            else
            {
                kh.TENKH = hoten;
                kh.TAIKHOAN = tendn;
                kh.MATKHAU = matkhau;
                kh.SDT = sdt;

                kh.EMAIL = email;
                kh.DIACHI = diachi;
                db.KHACHHANGs.InsertOnSubmit(kh);
                db.SubmitChanges();
                return RedirectToAction("DangNhap");
            }
            return this.DangKy();
        }

        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)

        {
            var tenDN = f["TenDN"];
            var matkhau = f["password"];

            if (string.IsNullOrEmpty(tenDN))
            {
                ViewData["Loi1"] = "Phai Nhap Ten Dang Nhap";
            }
            if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phai Nhap Mat Khau";
            }
            else
            {
                KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.TAIKHOAN == tenDN && n.MATKHAU == matkhau);


                if (kh != null)
                {
                    ViewBag.ThongBao = "Chuc Mung Dang Nhap Thanh Cong";
                    Session["TaiKhoan"] = kh;
                    return RedirectToAction("GioHang", "GioHang");
                }
                else
                {
                    ViewBag.ThongBao = "Ten Dang Nhap Hoac Mat Khau Khong Dung";
                }
            }
            return View();
        }
    }
}