using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCuoiKy.Models;
using System.IO;
using PagedList.Mvc;
using PagedList;
using System.Security.Cryptography;
using System.Text;

namespace WebCuoiKy.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        dbTraDataContext db = new dbTraDataContext();
        // GET: Admin/Admin
        public ActionResult TrangChu()
        {
            var dh = from s in db.DonHangDats.ToList().OrderByDescending(n => n.MaDH) select s;
            return View(dh);
        }
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            var sTenDN = f["UserName"];
            var sMatKhau = f["Password"];
            ADMIN ad = db.ADMINs.SingleOrDefault(n => n.TenDN == sTenDN && n.MatKhau == sMatKhau);
            if (ad != null)
            {
                if (ad.Quyen == 1)
                {
                    Session["HoTenAdmin"] = ad.TenAD;
                    Session["Admin"] = ad;
                    return RedirectToAction("TrangChu", "Admin");
                }
                if (ad.Quyen == 2)
                {
                    return View("User");
                }    
            }
            else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
        
        public ActionResult User()
        {
            return View();
        }
        public ActionResult DangXuat()
        {
            Session.Remove("Admin");
            return RedirectToAction("DangNhap", "Admin");
        }

        public ActionResult NavPartial()
        {
            return PartialView();
        }
        public ActionResult NavLeftPartial()
        {
            return PartialView();
        }
        
        public ActionResult Slider(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 5;
            return View(db.Sliders.ToList().OrderBy(n => n.MaSL).ToPagedList(iPageNum, iPageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create( FormCollection f)
        {
            Slider sl = new Slider();
            sl.TieuDe = f["sTieuDe"];
            sl.MoTa = f["sMoTa"];
            sl.Anh = f["sHinhAnh"];
            sl.HienThi = int.Parse(f["sHienThi"]);
            sl.LoaiSlider = f["sLoaiSlider"];
            db.Sliders.InsertOnSubmit(sl);
            db.SubmitChanges();
            return RedirectToAction("Slider");
            
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var sl = db.Sliders.SingleOrDefault(n => n.MaSL == id);
            db.Sliders.DeleteOnSubmit(sl);
            db.SubmitChanges();
            return RedirectToAction("Slider");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var sl = db.Sliders.SingleOrDefault(n => n.MaSL == id);
            if (sl == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            ViewBag.MaSL = new SelectList(db.Sliders.ToList().OrderBy(n => n.TieuDe), "MaSL", "TieuDe", sl.MaSL);
            return View(sl);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f, int id)
        {
            var sl = db.Sliders.SingleOrDefault(n => n.MaSL == id);
            ViewBag.MaSL = new SelectList(db.Sliders.ToList().OrderBy(n => n.TieuDe), "MaSL", "TieuDe", sl.MaSL);
                sl.Anh = f["sHinhAnh"];
                sl.TieuDe = f["sTieuDe"];
                sl.MoTa = f["sMoTa"];
                sl.HienThi = int.Parse(f["sHienThi"]);
                sl.LoaiSlider = f["sLoaiSlider"];
                db.SubmitChanges();
                return RedirectToAction("Slider");
                return View(sl);
        }

        public ActionResult DonHang()
        {
            var dh = from s in db.DonHangDats.ToList().OrderByDescending(n => n.MaDH ) select s;
            return View(dh);
        }
        public ActionResult Chitietdonhang(int id)
        {
            var ct = db.ChiTietDonHangs.Where(n => n.MaDH == id);
            return View(ct);
        }
        [HttpGet]
        public ActionResult Xoa(int id)
        {
            var dh = db.DonHangDats.SingleOrDefault(n => n.MaDH == id);
            db.DonHangDats.DeleteOnSubmit(dh);
            db.SubmitChanges();
            return RedirectToAction("DonHang");
        }
        [HttpGet]
        public ActionResult Sua(int id)
        {
            var dh = db.DonHangDats.SingleOrDefault(n => n.MaDH == id);
            if (dh == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            ViewBag.MaDH = new SelectList(db.DonHangDats.ToList().OrderBy(n => n.TenKH), "MaDH", "TenKH", dh.MaDH);
            return View(dh);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Sua(FormCollection f, int id)
        {
            var dh = db.DonHangDats.SingleOrDefault(n => n.MaDH == id);
            ViewBag.MaDH = new SelectList(db.DonHangDats.ToList().OrderBy(n => n.MaDH), "MaDH", "TenKH", dh.MaDH);
            dh.TenKH = f["sTenKH"];
            dh.DienThoai = f["sDienThoai"];
            dh.DiaChi = f["sDiaChi"];
            dh.DaThanhToan = false;
            dh.NgayDat = Convert.ToDateTime(f["sNgayDat"]);
            db.SubmitChanges();
            return RedirectToAction("DonHang");
            return View(dh);
        }
        [HttpGet]
        public ActionResult QLTK()
        {
            var tk = from s in db.ADMINs select s;
            return View(tk);
        }
        [HttpGet]
        public ActionResult ThemTK()
        {
            ViewBag.Quyen = new SelectList(db.QuyenADs.ToList().OrderBy(n => n.TenQuyen), "Quyen" ,"TenQuyen");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemTK(FormCollection f)
        {
                ADMIN tk = new ADMIN();
                tk.TenAD = f["sTen"];
                tk.TenDN = f["sTenDN"];
                tk.MatKhau = f["sMatKhau"];
                tk.DienThoai = f["sDT"];
                tk.Quyen = int.Parse(f["Quyen"]);
                db.ADMINs.InsertOnSubmit(tk);
                db.SubmitChanges();
                return RedirectToAction("QLTK");
        }
        [HttpGet]
        public ActionResult XoaTK(int id)
        {
            var tk = db.ADMINs.SingleOrDefault(n => n.MaAD == id);
            db.ADMINs.DeleteOnSubmit(tk);
            db.SubmitChanges();
            return RedirectToAction("QLTK");
        }

        [HttpGet]
        public ActionResult SuaTK(int id)
        {
            var tk = db.ADMINs.SingleOrDefault(n => n.MaAD == id);
            ViewBag.Quyen = new SelectList(db.QuyenADs.ToList().OrderBy(n => n.TenQuyen), "Quyen", "TenQuyen");
            return View(tk);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SuaTK(FormCollection f)
        {
            var tk = db.ADMINs.SingleOrDefault(n => n.MaAD== int.Parse(f["sMaAD"]));
            ViewBag.Quyen = new SelectList(db.QuyenADs.ToList().OrderBy(n => n.Quyen), "Quyen", "TenQuyen", tk.Quyen);
            tk.TenAD = f["sTen"];
            tk.TenDN = f["sTenDN"];
            tk.MatKhau = f["sMatKhau"];
            tk.DienThoai = f["sMatKhau"];
            tk.Quyen = int.Parse(f["Quyen"]);
            db.SubmitChanges();
            return RedirectToAction("QLTK");
            return View(tk);
        }
        [HttpGet]
        public ActionResult LienHe()
        {
            var lh = db.LienHes.SingleOrDefault(n => n.ID == 1);
            return View(lh);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult LienHe(FormCollection f)
        {
            var lh = db.LienHes.SingleOrDefault(n => n.ID == 1);
            lh.DiaChi = f["sDC"];
            lh.ViTri = f["sVT"];
            lh.Email = f["sEmail"];
            lh.DienThoai = f["sDT"];
            db.SubmitChanges();
            return View(lh);
        }
    }
}