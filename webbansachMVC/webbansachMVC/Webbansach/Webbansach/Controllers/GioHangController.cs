using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webbansach.Models;

namespace Webbansach.Controllers
{
    public class GioHangController : Controller
    {
        QuanLiBanSachModel db = new QuanLiBanSachModel();
        //Lấy giỏ hàng
        #region Giỏ hàng
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["Giohang"] as List<GioHang>;
            if(lstGioHang == null)
            {
                //Nếu giỏ hàng chưa có thì tạo giỏ hàng mới
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        //Thêm giỏ hàng
        public ActionResult ThemGioHang(int iMaSach, string strURL)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSach);
            if (Session["KhachHang"] == null || Session["KhachHang"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }
            else 
          
            if(sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy session Giỏ Hàng
            List<GioHang> lstGioHang = LayGioHang();
            GioHang SanPham = lstGioHang.Find(n => n.iMaSach == iMaSach);
            if (SanPham == null)
            {
                SanPham = new GioHang (iMaSach);
                lstGioHang.Add(SanPham);
                return Redirect(strURL);
            }
            else
            {
                SanPham.iSoLuong++;
                return Redirect(strURL);
            }
        }
        //Cập nhật giỏ hàng(số lượng)
        public ActionResult CapNhatGioHang(int iMaSach, FormCollection f)
        {   
            //Kiểm tra mã sách
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSach);
            //Nếu không có sách theo mã thì lỗi 404
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy giỏ hàng từ session
            List<GioHang> lstGioHang = LayGioHang();
            //Kiểm tra sản phẩm có tồn tại trong session[GioHang]
            GioHang SanPham = lstGioHang.SingleOrDefault(n => n.iMaSach == iMaSach);
            //Nếu có sản phẩm thì sửa số lượng
            if(SanPham != null)
            {
                SanPham.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        //Xóa giỏ hàng
        public ActionResult XoaGioHang(int iMaSach)
        {
            //Kiểm tra mã sách
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSach);
            //Nếu không có sách theo mã thì lỗi 404
            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy giỏ hàng từ session
            List<GioHang> lstGioHang = LayGioHang();
            GioHang SanPham = lstGioHang.SingleOrDefault(n => n.iMaSach == iMaSach);
            //Nếu có sản phẩm thì sửa số lượng
            if (SanPham != null)
            {
                lstGioHang.RemoveAll(n => n.iMaSach == SanPham.iMaSach);
            }
            if(lstGioHang.Count == 0)
            {
                return RedirectToAction("Index","Home");
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult GioHang()
        {
            if(Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }
        //Tính tổng số lượng sách giỏ hàng
        private int TongSL()
        {
            int iTongSL = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if(lstGioHang != null)
            {
                iTongSL = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSL;    
        }

        //Tính tổng số tiền
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.ThanhTien);
            }
            return dTongTien;
        }

        //Tạo partial giỏ hàng
        public ActionResult GioHangPartial()
        {
            if(TongSL() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSach = TongSL();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }

        //Xây dựng view cho người dùng sửa giỏ hàng
        public ActionResult SuaGioHang()
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }
        #endregion
        #region Đặt hàng
        // Chức năng đặt hàng
        [HttpPost]
        public ActionResult DatHang()
        {
            //Kiểm tra đăng nhập
            if(Session["KhachHang"] == null || Session["KhachHang"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }
            //Kiểm tra giỏ hàng
            if(Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //Thêm đơn hàng
            DonHang ddh = new DonHang();
            KhachHang kh = (KhachHang)Session["KhachHang"];
            List<GioHang> gh = LayGioHang();
            ddh.MaKH = kh.MaKH;
            ddh.NgayDat = DateTime.Now;
            db.DonHangs.Add(ddh);
            db.SaveChanges();
            //Thêm chi tiết đơn hàng
            foreach(var item in gh)
            {
                ChiTietDonHang ctDH = new ChiTietDonHang();
                ctDH.MaDonHang = ddh.MaDonHang;
                ctDH.MaSach = item.iMaSach;
                ctDH.SoLuong = item.iSoLuong;
                ctDH.ThanhTien = (int)item.ThanhTien;
                db.ChiTietDonHangs.Add(ctDH);
            }
            db.SaveChanges();
            Session["GioHang"] = null;
            TempData["ThongBao"] = "Đặt hàng thành công";
            return RedirectToAction("LichSuMuaHang", "KhachHang", new { @MaKH = Session["MaKH"] });
        }
        #endregion
    }
}