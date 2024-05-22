using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCuoiKy.Models;


namespace WebCuoiKy.Areas.Admin.Controllers
{
    public class DanhMucController : Controller
    {
        dbTraDataContext db = new dbTraDataContext();
        // GET: Admin/DanhMuc
        public ActionResult Index()
        {
            var id = from dm in db.DanhMucs select dm;
            return View(id);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection f)
        {
            DanhMuc dm = new DanhMuc();
            dm.TenDM = f["sTenDM"];
            dm.AnhBia = f["sAnhBia"];
            db.DanhMucs.InsertOnSubmit(dm);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var dm = db.DanhMucs.SingleOrDefault(n => n.MaDM == id);
            db.DanhMucs.DeleteOnSubmit(dm);
            db.SubmitChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dm = db.DanhMucs.SingleOrDefault(n => n.MaDM == id);
            if (dm == null)
            {
                Response.SubStatusCode = 404;
                return null;
            }
            ViewBag.MaDM = new SelectList(db.DanhMucs.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM", dm.MaDM);
            return View(dm);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f, int id)
        {
            var dm = db.DanhMucs.SingleOrDefault(n => n.MaDM == id);
            ViewBag.MaDM = new SelectList(db.DanhMucs.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM", dm.MaDM);
            dm.TenDM = f["sTenDM"];
            dm.AnhBia = f["sAnhBia"];
            db.SubmitChanges();
            return RedirectToAction("Index");
            return View(dm);
        }
    }
}