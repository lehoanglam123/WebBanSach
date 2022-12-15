using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webbansach.Models
{
    public class GioHang
    {

        QuanLiBanSachModel db = new QuanLiBanSachModel();

        public int iMaSach { get; set; }

        public string sTenSach { get; set; }

        public string sHinhAnh { get; set; }

        public double dDonGia { get; set; }

        public int iSoLuong { get; set; }

        public double ThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }

        //Hàm tạo giỏ hàng 
        public GioHang(int MaSach)
        {
            iMaSach = MaSach;
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSach);
            sTenSach = sach.TenSach;
            sHinhAnh = sach.AnhBia;
            dDonGia = double.Parse(sach.Gia.ToString());
            iSoLuong = 1;
        }
    }
}