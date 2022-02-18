using Models.EF;
using Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiemMauNhanDao.Areas.Admin.Controllers
{
    public class BaiDangController : BaseController
    {
        private DbContextHM db = new DbContextHM();
        DotToChucHMServices _baidang = new DotToChucHMServices();

        // GET: Admin/BaiDang      
        public ActionResult Index(string searchString1 , int page = 1, int pageSize = 10)
        {
            var services = new DotToChucHMServices();
            var model = services.ListAllBaiDang(searchString1 , page, pageSize);
            if (!string.IsNullOrEmpty(searchString1))
            {
                ViewBag.SearchStringBD = searchString1;
            }
            
            return View(model);
        }
        public ActionResult Index2(string searchString1, int page = 1, int pageSize = 10)
        {
            var services = new DotToChucHMServices();
            var model = services.ListAllBaiDang(searchString1, page, pageSize);
            if (!string.IsNullOrEmpty(searchString1))
            {
                ViewBag.SearchStringBD = searchString1;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(string id)
        {
            var model = _baidang.GetByIdDTCHM2(id);
            return View(model);
        }

        public ActionResult Details (string id)
        {
            var model = _baidang.GetByIdDTCHM2(id);
            return View(model);
        }

        [HttpDelete]
        public ActionResult Delete (string id)
        {
            _baidang.Delete(id);
            SetAlert("Xoá thành công", "success");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ChangeStatus5(string id)
        {
            var result = new DotToChucHMServices().ChangeStatus5(id);
            return Json(new
            {
                tt = result
            });
        }


        //[HttpGet]
        //public ActionResult Details(string id)
        //{
        //    var model = _chitiet.GetByIdChiTietDHM(id);

        //    return View(model);
        //}
    }
}