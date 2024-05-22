using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCuoiKy.Models;
using PagedList;
using PagedList.Mvc;

namespace WebCuoiKy.Controllers
{
    public class TimKiemController : Controller
    {
        dbTraDataContext db = new dbTraDataContext();
        // GET: TimKiem
        public ActionResult KQTimKiem(string searchString)
        {
            
            var kqtk = db.SanPhams.Where(n => n.TenSP.Contains(searchString));
            return View(kqtk.OrderBy(n=>n.TenSP));
        }
    }
}