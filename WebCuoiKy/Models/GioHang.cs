using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCuoiKy.Models;

namespace WebCuoiKy.Models
{
    public class GioHang
    {
        dbTraDataContext db = new dbTraDataContext();
        public int sMaSP { get; set; }
        public string sTenSP { get; set; }
        public string sAnhBia { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien { get { return iSoLuong * dDonGia; } }
        public GioHang(int ms)
        {
            sMaSP = ms;
            SanPham s = db.SanPhams.Single(n => n.MaSP == sMaSP);
            sTenSP = s.TenSP;
            sAnhBia = s.AnhBia;
            dDonGia = double.Parse(s.DonGia.ToString());
            iSoLuong = 1;
        }

    }
}