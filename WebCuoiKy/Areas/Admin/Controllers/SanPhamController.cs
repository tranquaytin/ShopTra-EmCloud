using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using PagedList.Mvc;
using PagedList;
using WebCuoiKy.Models;

namespace WebCuoiKy.Areas.Admin.Controllers
{
    public class SanPhamController : Controller
    {
        dbTraDataContext db = new dbTraDataContext();
        // GET: Admin/SanPham
        public ActionResult SanPham(int? page, string strSearch)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 8;
            return View(db.SanPhams.ToList().OrderByDescending(n => n.MaSP).ToPagedList(iPageNum, iPageSize));
            ViewBag.Search = strSearch;
            if (strSearch != null)
            {
                var kq = from s in db.SanPhams where s.TenSP.Contains(strSearch) || s.MoTa.Contains(strSearch) select s;
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FormCollection f)
        {
            SanPham sp = new SanPham();
            sp.TenSP = f["sTenSP"];
            sp.AnhBia = f["sAnhBia"];
            sp.SoLuong = int.Parse(f["sSoLuong"]);
            sp.DonGia = decimal.Parse(f["sDonGia"]);
            sp.MoTa = f["sMoTa"];
            sp.ChiTiet = f["sChiTiet"];
            db.SanPhams.InsertOnSubmit(sp);
            db.SubmitChanges();
            return RedirectToAction("SanPham");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
                var sp = db.SanPhams.SingleOrDefault(n => n.MaSP == id);
                db.SanPhams.DeleteOnSubmit(sp);
                db.SubmitChanges();
                return RedirectToAction("SanPham");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var sp = db.SanPhams.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            ViewBag.MaSP = new SelectList(db.SanPhams.ToList().OrderBy(n => n.TenSP), "MaSP", "TenSP", sp.MaSP);
            return View(sp);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f, int id)
        {
            var sp = db.SanPhams.SingleOrDefault(n => n.MaSP == id);
            ViewBag.MaSP = new SelectList(db.SanPhams.ToList().OrderBy(n => n.TenSP), "MaSP", "TenSP", sp.MaSP);
            sp.TenSP = f["sTenSP"];
            sp.AnhBia = f["sAnhBia"];
            sp.SoLuong = int.Parse(f["sSoLuong"]);
            sp.DonGia = decimal.Parse(f["sDonGia"]);
            sp.MoTa = f["sMoTa"];
            sp.ChiTiet = f["sChiTiet"];
            db.SubmitChanges();
            return RedirectToAction("SanPham");
            return View(sp);
        }
    }
}