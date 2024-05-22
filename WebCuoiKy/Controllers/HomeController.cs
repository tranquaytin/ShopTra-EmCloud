using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCuoiKy.Models;
using PagedList;
using PagedList.Mvc;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using System.IO;
using System.Configuration;


namespace WebCuoiKy.Controllers
{
    public class HomeController : Controller
    {
        dbTraDataContext db = new dbTraDataContext();
        private List<SanPham> LaySPMoi(int count)
        {
            return db.SanPhams.OrderByDescending(s=>s.NgayCapNhat).Take(count).ToList();
        }

        public ActionResult Index( int? page)
        {
            int iSize = 8;
            int iPageNum = (page ?? 1);
            var Laysp = LaySPMoi(20);
            return View(Laysp.ToPagedList(iPageNum, iSize));
        }
        public ActionResult BilLienHe()
        {
            return View();
        }
        public ActionResult LienHe()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LienHe(string ten, string phone, string address, string email)
        {
            string content = System.IO.File.ReadAllText(Server.MapPath("~/Views/Home/BilLienHe.cshtml"));
            content = content.Replace("{{CustomerName}}", ten);
            content = content.Replace("{{Phone}}", phone);
            content = content.Replace("{{Email}}", email);
            content = content.Replace("{{Address}}", address);

            var toEmail = ConfigurationManager.AppSettings["ToEmailAdress"].ToString();

            new MailHelper().SendMail(email, "Phản hồi của bạn", content);
            new MailHelper().SendMail(toEmail, "Thông tin người liên hệ", content);

            return RedirectToAction("XacNhanLienHe", "Home");
        }
        public ActionResult XacNhanLienHe()
        {
            return View();
        }
        public ActionResult DangNhapPartial()
        {
            return PartialView();
        }
        public ActionResult ChiTietSanPham(int id, string bul)
        {
            var sp = db.SanPhams.Where(n => n.MaSP == id).FirstOrDefault();
            
            return View(sp);
        }
        
        public ActionResult HeadPartial()
        {
            return PartialView();
        }
        public ActionResult DMSPPartial()
        {
            var id = from dm in db.DanhMucs select dm;
            return PartialView(id);
        }
        [ChildActionOnly]
        public ActionResult NavPartial()
        {
            List<Menu> lst = new List<Menu>();
            lst = db.Menus.Where(m => m.ParentId == null).OrderBy(m => m.OrderNumBer).ToList();
            int[] a = new int[lst.Count];
            for (int i = 0; i < lst.Count; i++)
            {
                var l = db.Menus.Where(m => m.ParentId == lst[i].Id);
                a[i] = l.Count();
            }
            ViewBag.lst = a;
            return PartialView(lst);
        }
        [ChildActionOnly]
        public ActionResult LoadChildMenu(int parentId)
        {
            List<Menu> lst = new List<Menu>();
            lst = db.Menus.Where(m => m.ParentId == parentId).OrderBy(m => m.OrderNumBer).ToList();
            ViewBag.Count = lst.Count();
            int[] a = new int[lst.Count()];
            for (int i = 0; i < lst.Count; i++)
            {
                var l = db.Menus.Where(m => m.ParentId == lst[i].Id);
                a[i] = l.Count();
            }
            ViewBag.lst = a;
            return PartialView("LoadChildMenu", lst);
        }
        public ActionResult SliderPartial()
        {
            var sl = from s in db.Sliders select s;
            return PartialView(sl);
        }
        public ActionResult FooterPartial()
        {
            var ft = from s in db.LienHes select s;
            return PartialView(ft);
        }
        public ActionResult KhuyenMaiPartial()
        {
            return PartialView();
        }
        public ActionResult SanPhamTheoDanhMuc(int id)
        {
            var sp = from s in db.SanPhams where s.MaDM == id select s;
            return View(sp);
        }

        public ActionResult DanhGiaPartial(FormCollection f)
        {
            var dg = db.DanhGias.OrderByDescending(n => n.ID).ToList();
            return PartialView(dg);
        }
       
        [HttpGet]
        public ActionResult DanhGia()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DanhGia(FormCollection f)
        {
            DanhGia dg = new DanhGia();
            dg.TenNguoiDG = f["sTen"];
            dg.NoiDung = f["sND"];
            dg.Email = f["sEmail"];
            dg.NgayDG = DateTime.Now;
            db.DanhGias.InsertOnSubmit(dg);
            db.SubmitChanges();
            return View("Index");
        }

    }
}