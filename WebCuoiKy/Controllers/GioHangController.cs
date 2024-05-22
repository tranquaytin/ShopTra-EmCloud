using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCuoiKy.Models;
using System.IO;
using System.Configuration;

namespace WebCuoiKy.Controllers
{
    public class GioHangController : Controller
    {
        dbTraDataContext db = new dbTraDataContext();
        public ActionResult Bill()
        {
            return View();
        }
        public ActionResult GioHang()
        {
            List<GioHang> lstGioHang = LayGioHang();
            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }
        public ActionResult ThemGioHang(int ms, string url)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.Find(n => n.sMaSP == ms);
            if (sp == null)
            {
                sp = new GioHang(ms);
                lstGioHang.Add(sp);
            }
            else
            {
                sp.iSoLuong++;
            }
            return Redirect(url);
        }
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                iTongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }
            return iTongSoLuong;
        }
        private double TongTien()
        {
            double dTongTien = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                dTongTien = lstGioHang.Sum(n => n.dThanhTien);
            }
            return dTongTien;
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        public ActionResult XoaGioHang(int smasp)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.SingleOrDefault(n => n.sMaSP == smasp);
            if (sp != null)
            {
                lstGioHang.RemoveAll(n => n.sMaSP == smasp);
                if (lstGioHang.Count == 0)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult CapNhatGioHang(int smasp, FormCollection f)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sp = lstGioHang.SingleOrDefault(n => n.sMaSP == smasp);
            if (sp != null)
            {
                sp.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaAllGioHang()
        {
            List<GioHang> lstGioHang = LayGioHang();
            lstGioHang.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "KhachHang");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        [HttpPost]
        public ActionResult DatHang(string ten, string phone, string address, string email)
        {
            ;

            DonHangDat ddh = new DonHangDat();
            KhachHang kh = (KhachHang)Session["TaiKhoan"];
            ddh.TenKH = ten;
            ddh.DienThoai = phone;
            ddh.Email = email;
            ddh.DiaChi = address;
            List<GioHang> lstGioHang = LayGioHang();
            ddh.MaKH = kh.MaKH;
            ddh.TenKH = kh.TenKH;
            ddh.DiaChi = kh.DiaChi;
            ddh.DienThoai = kh.DienThoai;
            ddh.Email = kh.Email;
            ddh.NgayDat = DateTime.Now;
            ddh.TinhTrangGiaoHang = 1;
            ddh.DaThanhToan = false;
            db.DonHangDats.InsertOnSubmit(ddh);
            db.SubmitChanges();
            double total = 0;
            foreach (var item in lstGioHang)
            {
                ChiTietDonHang ctdh = new ChiTietDonHang();
                ctdh.MaDH = ddh.MaDH;
                ctdh.MaSP = item.sMaSP;
                ctdh.SoLuong = item.iSoLuong;
                ctdh.DonGia = (decimal)item.dDonGia;
                db.ChiTietDonHangs.InsertOnSubmit(ctdh);

                total += (item.dDonGia)*(item.iSoLuong);
            }
            db.SubmitChanges();
            Session["GioHang"] = null;

            string content = System.IO.File.ReadAllText(Server.MapPath("~/Views/GioHang/Bill.cshtml"));

            content = content.Replace("{{CustomerName}}", ten);
            content = content.Replace("{{Phone}}", phone);
            content = content.Replace("{{Email}}", email);
            content = content.Replace("{{Address}}", address);
            content = content.Replace("{{Total}}", total.ToString());

            var toEmail = ConfigurationManager.AppSettings["ToEmailAdress"].ToString();

            new MailHelper().SendMail(email, "Đơn Hàng Mới Từ EmCloud", content);
            new MailHelper().SendMail(toEmail, "Đơn Hàng Mới Từ EmCloud", content);

            return RedirectToAction("XacNhanDatHang", "GioHang");
        }
        public ActionResult ChiTietDonHang()
        {
            ChiTietDonHang ddh = new ChiTietDonHang();
            List<GioHang> lstGioHang = LayGioHang();
            if (lstGioHang.Count == 0)
            {
                ChiTietDonHang ctdh = new ChiTietDonHang();
                ctdh.MaDH = ddh.MaDH;
                ctdh.MaSP = ddh.MaSP;
                ctdh.SoLuong = ddh.SoLuong;
                ctdh.DonGia = ddh.DonGia;
                db.ChiTietDonHangs.InsertOnSubmit(ctdh);
            }
            ViewBag.TongSoLuong = TongSoLuong();
            ViewBag.TongTien = TongTien();
            return View(lstGioHang);
        }
        public ActionResult XacNhanDatHang()
        {
            return View();
        }

        
    }
}