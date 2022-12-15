using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbansach.Models;

namespace Webbansach.Controllers
{
    public class KhachHangController : Controller
    {
        // GET: KhachHang
        QuanLiBanSachModel db = new QuanLiBanSachModel();
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(KhachHang kh)
        {
            KhachHang kh1 = db.KhachHangs.SingleOrDefault(n => n.TaiKhoan.Equals(kh.TaiKhoan));
            string nlMatKhau = Request.Form["nlMatKhau"];
            if (ModelState.IsValid)
            {
                if (kh1 != null)
                {
                    ModelState.AddModelError("", "Tên tài khoản đã tồn tại. Hãy thử lại");
                }
                else if (kh.MatKhau.Equals(nlMatKhau))
                {
                    db.KhachHangs.Add(kh);
                    db.SaveChanges();
                    Session["TenTaiKhoan"] = kh.TaiKhoan;
                    Session["KhachHang"] = kh;
                    ModelState.AddModelError("", "Đăng kí thành công");
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    ModelState.AddModelError("", "Mật Khẩu nhập lại không đúng");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        

        [HttpPost]
        public ActionResult DangNhap(KhachHang kh)
        {
            //sTaiKhoan = f.Get("txttaikhoan").ToString();
            //sMatKhau = f.Get("txtmatkhau").ToString();
            string sTaiKhoan = Request.Form["txttaikhoan"];
            string sMatKhau = Request.Form["txtmatkhau"];
            if (sTaiKhoan != null && sMatKhau != null) {
                kh = db.KhachHangs.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan && n.MatKhau == sMatKhau);
                if (kh != null)
                {
                    if(kh.TaiKhoan.ToString().Equals("admin"))
                    {
                        return RedirectToAction("Index", "QuanLiSach");
                    }
                    else
                    {
                        ViewBag.thongbao = "Đăng nhập thành công";
                        Session["TenTaiKhoan"] = kh.TaiKhoan;
                        Session["MaKH"] = kh.MaKH;
                        Session["KhachHang"] = kh;
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ViewBag.thongbao = "Tên tài khoản hoặc mật khẩu không đúng";
                }
                return View();
            }
            return View();
        }

        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("Index","Home");
        }

        public ActionResult KhachHangPartial()
        {            
            return PartialView();
        }

        public ActionResult LichSuMuaHang(int MaKH)
        {

            List<DonHang> dh = db.DonHangs.Where(n => n.MaKH == MaKH).ToList();
            if(dh == null) 
            {
                ViewBag.ThongBao = "Bạn chưa mua hàng ở BookStort";
            }
            else
            {
                ViewBag.ThongBao = "Lịch sử mua hàng";
            }
            return View(dh);
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

        public ActionResult ChiTietMuaHang(int MaDonHang)
        {
            ViewBag.MaKH = Session["MaKH"];
            ViewBag.TongTien = TongTien(MaDonHang);
            ViewBag.MaDH = MaDonHang;
            List<ChiTietDonHang> lstctdh = db.ChiTietDonHangs.Where(n => n.MaDonHang == MaDonHang).ToList();
            return View(lstctdh);
        }

    }
}