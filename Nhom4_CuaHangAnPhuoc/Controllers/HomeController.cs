using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nhom4_CuaHangAnPhuoc.Models;


namespace Nhom4_CuaHangAnPhuoc.Controllers
{
    public class HomeController : Controller
    {
        QLQuanAoDataContext db = new QLQuanAoDataContext();
        // GET: Home
        public ActionResult TrangChu()
        {
            var lst = db.SANPHAMs.OrderBy(q => q.TENSP).ToList();
            return View(lst);
        }
    }
}