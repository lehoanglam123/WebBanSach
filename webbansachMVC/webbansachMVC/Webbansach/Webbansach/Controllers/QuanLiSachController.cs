using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbansach.Models;

namespace Webbansach.Controllers
{
    public class QuanLiSachController : Controller
    {
        // GET: QuanLiSach
        QuanLiBanSachModel db = new QuanLiBanSachModel();
        public ActionResult Index()
        {
            return View(db.Saches.ToList());
        }

        //Thêm sách
        [HttpGet]
        public ActionResult ThemSach()
        {
            //Đưa dữ liệu vào dropdownlist
            ViewBag.MaTheLoai = new SelectList (db.TheLoais.ToList().OrderBy(n =>n.TenTheLoai),"MaTheLoai","TenTheLoai");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            ViewBag.MaTacGia = new SelectList(db.TacGias.ToList().OrderBy(n => n.TenTacGia), "MaTacGia", "TenTacGia");
            return View();
        }
        [HttpPost]
        public ActionResult ThemSach(Sach sach,HttpPostedFileBase fileUpload)
        {

            //Đưa dữ liệu vào dropdownlist
            ViewBag.MaTheLoai = new SelectList(db.TheLoais.ToList().OrderBy(n => n.TenTheLoai), "MaTheLoai", "TenTheLoai");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB");
            ViewBag.MaTacGia = new SelectList(db.TacGias.ToList().OrderBy(n => n.TenTacGia), "MaTacGia", "TenTacGia");
            //Kiểm tra đường dẫn ảnh bìa
            if(fileUpload == null)
            {
                ViewBag.ThongBao = "Chọn hình ảnh";
                return View();
            }
            //Thêm vào CSDL
            if (ModelState.IsValid)
            {
                //Lưu tên file
                var fileName = Path.GetFileName(fileUpload.FileName);
                //Lưu đường dẫn của file
                var path = Path.Combine(Server.MapPath("~/HinhAnh/Sach"), fileName);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                }
                else
                {
                    fileUpload.SaveAs(path);
                }
                sach.AnhBia = fileUpload.FileName;
                db.Saches.Add(sach);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        //Chỉnh sửa thông tin sách
        [HttpGet]
        public ActionResult ChinhSua(int MaSach)
        {
            //Lấy đối tượng sách theo mã sách
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if(sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Đưa dữ liệu vào dropdownlist
            ViewBag.MaTheLoai = new SelectList(db.TheLoais.ToList().OrderBy(n => n.TenTheLoai), "MaTheLoai", "TenTheLoai",sach.MaTheLoai);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB",sach.MaNXB);
            ViewBag.MaTacGia = new SelectList(db.TacGias.ToList().OrderBy(n => n.TenTacGia), "MaTacGia", "TenTacGia",sach.MaTacGia);
            return View(sach);
        }

        [HttpPost]
        public ActionResult ChinhSua(Sach sach)
        {
            //Đưa dữ liệu vào dropdownlist
            ViewBag.MaTheLoai = new SelectList(db.TheLoais.ToList().OrderBy(n => n.TenTheLoai), "MaTheLoai", "TenTheLoai", sach.MaTheLoai);
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.ToList().OrderBy(n => n.TenNXB), "MaNXB", "TenNXB", sach.MaNXB);
            ViewBag.MaTacGia = new SelectList(db.TacGias.ToList().OrderBy(n => n.TenTacGia), "MaTacGia", "TenTacGia", sach.MaTacGia);
            if (ModelState.IsValid)
            {
                db.Entry(sach).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        //Hiển thị sách
        public ActionResult HienThi(int MaSach)
        {
            //Lấy đối tượng sách theo mã sách
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }

        //Xóa sách
        [HttpGet]
        public ActionResult Xoa(int MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if(sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sach);
        }

        [HttpPost,ActionName("Xoa")]
        public ActionResult XacNhanXoa(int MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Saches.Remove(sach);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}