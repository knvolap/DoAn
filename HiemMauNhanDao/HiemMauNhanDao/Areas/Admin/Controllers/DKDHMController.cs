using Models.EF;
using Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiemMauNhanDao.Areas.Admin.Controllers
{
    public class DKDHMController : BaseController
    {
        DKDHMServices _DKDHM = new DKDHMServices();
        // GET: Admin/DKDHM
        public ActionResult Index(string searchString, int page=1, int pageSize=5)
        {
            var chitiet = new DKDHMServices();
            var model = chitiet.ListAllDKDHM(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        public ActionResult EditDKHM(string id)
        {
            ViewBag.idDVLK = new SelectList(_DKDHM.GetDonViLienKets(), "IdDVLK", "TenDonVi");
            return View(_DKDHM.GetChiTietDHMID(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDKHM(chiTietDHM chiTietDHM)
        {
            if (ModelState.IsValid)
            {
                _DKDHM.EditCTDHM(chiTietDHM);
                 SetAlert("Chỉnh sửa thành công", "success");
                return RedirectToAction("Index");
            }
            else
            {
                SetAlert("Chỉnh sửa thất bại", "warning");
                return RedirectToAction("Index");
            }
            return View(chiTietDHM);
        }

        public ActionResult Details(string id)
        {
            var model = _DKDHM.GetChiTietDHMID(id);
            return View(model);
        }

        public ActionResult DeleteDKHM(string id)
        {
            _DKDHM.DeleteDKDHM(id);
            return RedirectToAction("Index");
        }
    }
}