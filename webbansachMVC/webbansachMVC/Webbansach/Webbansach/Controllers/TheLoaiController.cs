using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbansach.Models;

namespace Webbansach.Controllers
{
    public class TheLoaiController : Controller
    {
        // GET: TheLoai
        QuanLiBanSachModel db = new QuanLiBanSachModel();
        public PartialViewResult TheLoaiPartial()
        {
            var listTheLoai = db.TheLoais.Take(6).ToList();
            return PartialView(listTheLoai);
        }
        public PartialViewResult TheLoaiXemThemPartial()
        {
            var listTheLoaiXemThem = db.TheLoais.Where(n => n.MaTheLoai > 6).ToList();
            return PartialView(listTheLoaiXemThem);
        }
        public ViewResult SachTheoTheLoai(int MaTheLoai)
        {
            TheLoai tl = db.TheLoais.SingleOrDefault(n => n.MaTheLoai == MaTheLoai);
            if(tl == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<Sach> lstsach = db.Saches.Where(n => n.MaTheLoai == MaTheLoai).OrderBy(n => n.TenSach).ToList();
            if(lstsach.Count == 0)
            {
                ViewBag.Sach = "Không có sách thuộc thể loại:" + tl.TenTheLoai;
            }
            else
            {
                ViewBag.Sach = "Thể loại: " + tl.TenTheLoai;
            }
            return View(lstsach);
        }
    }
}