using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbansach.Models;

namespace Webbansach.Controllers
{
    public class QuanLiTacGiaController : Controller
    {
       QuanLiBanSachModel db = new QuanLiBanSachModel();
        // GET: QuanLiTacGia
        public ActionResult Index()
        {
            return View(db.TacGias.ToList());
        }

        //Thêm tác giả
        [HttpGet]
        public ActionResult ThemTacGia()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemTacGia(TacGia tacgia)
        {
            if (ModelState.IsValid)
            {
                db.TacGias.Add(tacgia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        //Chỉnh sửa thông tin tác giả
        [HttpGet]
        public ActionResult ChinhSua(int MaTacGia)
        {
            //Lấy đối tượng tác giả theo mã tác giả
            TacGia tacgia = db.TacGias.SingleOrDefault(n => n.MaTacGia == MaTacGia);
            if (tacgia == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(tacgia);
        }

        [HttpPost]
        public ActionResult ChinhSua(TacGia tacgia)
        {           
            if (ModelState.IsValid)
            {
                db.Entry(tacgia).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        //Hiển thị thông tin tác giả
        public ActionResult HienThi(int MaTacGia)
        {
            //Lấy đối tượng tác giả theo mã tác giả
            TacGia tacgia = db.TacGias.SingleOrDefault(n => n.MaTacGia == MaTacGia);
            if (tacgia == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(tacgia);
        }

        //Xóa thông tin tác giả
        [HttpGet]
        public ActionResult Xoa(int MaTacGia)
        {
            //Lấy đối tượng tác giả theo mã tác giả
            TacGia tacgia = db.TacGias.SingleOrDefault(n => n.MaTacGia == MaTacGia);
            if (tacgia == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(tacgia);
        }

        [HttpPost, ActionName("Xoa")]
        public ActionResult XacNhanXoa(int MaTacGia)
        {
            //Lấy đối tượng tác giả theo mã tác giả
            TacGia tacgia = db.TacGias.SingleOrDefault(n => n.MaTacGia == MaTacGia);
            if (tacgia == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.TacGias.Remove(tacgia);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}