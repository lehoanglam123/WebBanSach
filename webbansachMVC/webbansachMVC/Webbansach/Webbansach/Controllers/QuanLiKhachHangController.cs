using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbansach.Models;

namespace Webbansach.Controllers
{
    public class QuanLiKhachHangController : Controller
    {
        // GET: QuanLiKhachHang
        QuanLiBanSachModel db = new QuanLiBanSachModel();
        public ActionResult Index()
        {
            return View(db.KhachHangs.ToList());
        }

        //Xóa sách
        [HttpGet]
        public ActionResult Xoa(int MaKH)
        {
            KhachHang Kh = db.KhachHangs.SingleOrDefault(n => n.MaKH == MaKH);
            if (Kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(Kh);
        }

        [HttpPost, ActionName("Xoa")]
        public ActionResult XacNhanXoa(int MaKH)
        {
            KhachHang Kh = db.KhachHangs.SingleOrDefault(n => n.MaKH == MaKH);
            if (Kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.KhachHangs.Remove(Kh);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}