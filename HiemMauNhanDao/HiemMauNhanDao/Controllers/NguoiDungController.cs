using HiemMauNhanDao.Areas.Admin.Controllers;
using Models.EF;
using Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiemMauNhanDao.Controllers
{
    public class NguoiDungController : BaseController
    {
        NguoiDungServices _NguoiDung = new NguoiDungServices();
        private DbContextHM db = new DbContextHM();

        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(ThongTinCaNhan thongTinCaNhan)
        {
            if (ModelState.IsValid)
            {
                _NguoiDung.ThemTTCN(thongTinCaNhan);
                SetAlert("Thêm thành công", "success");
                return RedirectToAction("Index");
            }
            else
            {
                SetAlert("cập nhật thất bại công", "success");
                return RedirectToAction("Index");
            }    

            return View(thongTinCaNhan);
        }

      
        public ActionResult EditTTCN(string id)
        {
            return View(_NguoiDung.GetByIdTTCN(id));
        }
        [HttpPost]
        public ActionResult EditTTCN(ThongTinCaNhan thongTinCaNhan)
        {
            if (ModelState.IsValid)
            {
                _NguoiDung.SuaTTCN(thongTinCaNhan);
                SetAlert("Thêm thành công", "success");
                return RedirectToAction("Index");
            }
            return View(thongTinCaNhan);
        }

    }
}