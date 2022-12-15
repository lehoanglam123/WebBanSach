using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbansach.Models;

namespace Webbansach.Controllers
{
    public class QuanLiDonHangController : Controller
    {
        // GET: QuanLiDonHang
       QuanLiBanSachModel db = new QuanLiBanSachModel();
        public ActionResult Index()
        {
            return View(db.DonHangs.ToList());
        }

        //Tính tổng số tiền hóa đơn
        private double TongTien(int MaDonHang)
        {
            double dTongTien = 0;
            List<ChiTietDonHang> lstctdh = db.ChiTietDonHangs.Where(n => n.MaDonHang == MaDonHang).ToList();            
            if (lstctdh != null)
            {
                dTongTien = (double)lstctdh.Sum(n => n.ThanhTien);
            }
            return dTongTien;
        }

        public ActionResult HienThiDonHang(int MaDonHang)
        {
            DonHang dh = db.DonHangs.SingleOrDefault(n => n.MaDonHang == MaDonHang);
            ViewBag.TongTien = TongTien(MaDonHang);
            List<ChiTietDonHang> lstctdh = db.ChiTietDonHangs.Where(n => n.MaDonHang == MaDonHang).ToList();            
            if (dh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dh);
        }

        public ActionResult ChiTietDonHang(int MaDonHang)
        {
            ViewBag.TongTien = TongTien(MaDonHang);
            ViewBag.MaDH = MaDonHang;
            List<ChiTietDonHang> lstctdh = db.ChiTietDonHangs.Where(n => n.MaDonHang == MaDonHang).ToList();
            return View(lstctdh);
        }

        //Cập nhật tình trạng đơn hàng
        [HttpGet]
        public ActionResult CapNhat(int MaDonHang)
        {
            ViewBag.MaDH = MaDonHang;
            DonHang dh = db.DonHangs.SingleOrDefault(n => n.MaDonHang == MaDonHang);
            if (dh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dh);
        }

        [HttpPost]
        public ActionResult CapNhat(DonHang DH)
        {
            if(ModelState.IsValid)
            {
                db.Entry(DH).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}