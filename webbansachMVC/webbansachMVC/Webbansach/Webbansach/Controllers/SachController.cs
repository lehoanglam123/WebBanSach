using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbansach.Models;

namespace Webbansach.Controllers
{
    public class SachController : Controller
    {
        QuanLiBanSachModel db = new QuanLiBanSachModel();
        // GET: Sach
        public PartialViewResult SachPartial()
        {
            var listSach = db.Saches.Take(6).ToList();
            return PartialView(listSach);
        }
        //Xem chi tiết
        public ViewResult XemChiTiet(int MaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == MaSach);
            TheLoai tl = db.TheLoais.SingleOrDefault(n => n.MaTheLoai == sach.MaTheLoai);
            NhaXuatBan nxb = db.NhaXuatBans.SingleOrDefault(n => n.MaNXB == sach.MaNXB);
            TacGia tg = db.TacGias.SingleOrDefault(n => n.MaTacGia == sach.MaTacGia);
            if (sach == null)
            {
                //Trang báo lỗi
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                ViewBag.TenTacGia = tg.TenTacGia;
                ViewBag.TenTheLoai = tl.TenTheLoai;
                ViewBag.TenNXB = nxb.TenNXB;
            }
            return View(sach);
        }

        //Tìm kiếm sách
        [HttpPost]
        public ActionResult KetQuaTimKiem(FormCollection f)
        {
            string TuKhoa = f["txtTimKiem"].ToString();
            List<Sach> lstKQ = db.Saches.Where(n => n.TenSach.Contains(TuKhoa)).ToList();
            if(lstKQ.Count == 0)
            {
                ViewBag.KetQua = "Không tìm thấy sản phẩm với từ khóa: " + TuKhoa;
            }
            else
            {
                ViewBag.KetQua = "Sách với từ khóa: " + TuKhoa;
            }    
            return View(lstKQ);
        }

    }
}