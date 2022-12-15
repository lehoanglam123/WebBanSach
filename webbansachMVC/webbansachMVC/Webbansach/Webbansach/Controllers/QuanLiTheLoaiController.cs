using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbansach.Models;

namespace Webbansach.Controllers
{
    public class QuanLiTheLoaiController : Controller
    {
       QuanLiBanSachModel db = new QuanLiBanSachModel();
        // GET: QuanLiTheLoai
        public ActionResult Index()
        {
            return View(db.TheLoais.ToList());
        }

        //Thêm thể loại sách
        [HttpGet]
        public ActionResult ThemTheLoai()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemTheLoai(TheLoai theloai)
        {
            if (ModelState.IsValid)
            {
                db.TheLoais.Add(theloai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        //Chỉnh sửa thông tin thể loại
        [HttpGet]
        public ActionResult ChinhSua(int MaTheLoai)
        {
            //Lấy đối tượng thể loại theo mã thể loại
            TheLoai theloai = db.TheLoais.SingleOrDefault(n => n.MaTheLoai == MaTheLoai);
            if (theloai == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(theloai);
        }

        [HttpPost]
        public ActionResult ChinhSua(TheLoai theloai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(theloai).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        //Hiển thị thông tin thể loại
        public ActionResult HienThi(int MaTheLoai)
        {
            //Lấy đối tượng thể loại theo mã thể loại
            TheLoai theloai = db.TheLoais.SingleOrDefault(n => n.MaTheLoai == MaTheLoai);
            if (theloai == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(theloai);
        }

        //Xóa thể loại
        [HttpGet]
        public ActionResult Xoa(int MaTheLoai)
        {
            TheLoai theloai = db.TheLoais.SingleOrDefault(n => n.MaTheLoai == MaTheLoai);
            if (theloai == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(theloai);
        }

        [HttpPost, ActionName("Xoa")]
        public ActionResult XacNhanXoa(int MaTheLoai)
        {
            TheLoai theloai = db.TheLoais.SingleOrDefault(n => n.MaTheLoai == MaTheLoai);
            if (theloai == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.TheLoais.Remove(theloai);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}