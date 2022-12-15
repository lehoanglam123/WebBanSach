using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbansach.Models;

namespace Webbansach.Controllers
{
    public class QuanLiNXBController : Controller
    {
        // GET: QuanLiNXB
        QuanLiBanSachModel db = new QuanLiBanSachModel();
        public ActionResult Index()
        {
            return View(db.NhaXuatBans.ToList());
        }
        //Thêm nhà xuất bản
        [HttpGet]
        public ActionResult ThemNXB()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ThemNXB(NhaXuatBan NXB)
        {
            if (ModelState.IsValid)
            {
                db.NhaXuatBans.Add(NXB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        //Chỉnh sửa thông tin nhà xuất bản
        [HttpGet]
        public ActionResult ChinhSua(int MaNXB)
        {
            //Lấy đối tượng tác giả theo mã nhà xuất bản
            NhaXuatBan NXB = db.NhaXuatBans.SingleOrDefault(n => n.MaNXB == MaNXB);
            if (NXB == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(NXB);
        }

        [HttpPost]
        public ActionResult ChinhSua(NhaXuatBan NXB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(NXB).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        //Hiển thị thông tin nhà xuất bản
        public ActionResult HienThi(int MaNXB)
        {
            //Lấy đối tượng tác giả theo mã nhà xuất bản
            NhaXuatBan NXB = db.NhaXuatBans.SingleOrDefault(n => n.MaNXB == MaNXB);
            if (NXB == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(NXB);
        }

        //Xóa thông tin tác giả
        [HttpGet]
        public ActionResult Xoa(int MaNXB)
        {
            //Lấy đối tượng tác giả theo mã nhà xuất bản
            NhaXuatBan NXB = db.NhaXuatBans.SingleOrDefault(n => n.MaNXB == MaNXB);
            if (NXB == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(NXB);
        }

        [HttpPost, ActionName("Xoa")]
        public ActionResult XacNhanXoa(int MaNXB)
        {
            //Lấy đối tượng tác giả theo mã nhà xuất bản
            NhaXuatBan NXB = db.NhaXuatBans.SingleOrDefault(n => n.MaNXB == MaNXB);
            if (NXB == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.NhaXuatBans.Remove(NXB);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}