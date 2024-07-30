using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nhom4_CuaHangAnPhuoc.Models;


namespace Nhom4_CuaHangAnPhuoc.Controllers
{
    public class ProductController : Controller
    {
        QLQuanAoDataContext db = new QLQuanAoDataContext();
        // GET: Product
        public ActionResult ShowDetailProduct(string id)
        {
            var prt = db.SANPHAMs.Single(sp => sp.MASP == id);
            return View(prt);
        }

        public ActionResult ShowProductAoThun(string id)
        {
            var prt = db.SANPHAMs.OrderBy(sp => sp.TENSP).Where(sp => sp.MaDM == id).ToList();
            return View(prt);
        }
    }
}