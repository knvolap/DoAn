using Models.EF;
using Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiemMauNhanDao.Areas.Admin.Controllers
{
    public class DotHienMauController : BaseController
    {
        DotHienMauServices _DHM = new DotHienMauServices();
        private DbContextHM db = new DbContextHM();
        // GET: Admin/DotHiemMau
        public ActionResult Index(string searchString1, string searchString2, int page = 1, int pageSize = 5)
        {
            var services = new DotHienMauServices();         
            var model = services.ListAllPagingDHM(searchString1, searchString2,page, pageSize);

            if (!string.IsNullOrEmpty(searchString1))
            {
                ViewBag.SearchString1 = searchString1;
            }
            else
            {
                ViewBag.SearchString2 = searchString2;
            }
            return View(model);
        }


        public ActionResult CreateDHM()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateDHM(DotHienMau dotHienMau)
        {
            if (ModelState.IsValid)
            {
                if (_DHM.isExistDTC(dotHienMau.TenDHM))
                {
                    SetAlert("Tên đợt đã tồn tại !", "error");
                    return RedirectToAction("CreateDHM");
                }
                else if(dotHienMau.tgBatDau >= dotHienMau.tgKetThuc)
                {
                    SetAlert("Vui lòng nhập thời gian kết thúc lớn hơn thòi gian bắt đầu !", "error");
                    return RedirectToAction("CreateDHM");
                }
                else
                {
                    _DHM.ThemDHM(dotHienMau);
                    SetAlert("Thêm thành công", "success");
                    return RedirectToAction("Index");
                }        
            }
            return View(dotHienMau);
        }
        public ActionResult EditDHM(string id)
        {
            return View(_DHM.GetByIdDHM(id));
        }
        [HttpPost]
        public ActionResult EditDHM(DotHienMau dotHienMau)
        {
            _DHM.SuaDHM(dotHienMau);
            SetAlert("Sửa thành công", "success");
            return RedirectToAction("Index");
        }
        

        public ActionResult DetailsDHM(string id)
        {
            var model = _DHM.GetByIdDHM(id);
            return View(model);
        }
      
        [HttpDelete]
        public ActionResult DeleteDHM(string id)
        {
            _DHM.XoaDHM(id);
           SetAlert("Xoá thành công", "success");
            return RedirectToAction("Index");
        }
    


        //public ActionResult DanhSachDK(string searchString, int page = 1, int pageSize = 5)
        //{
        //    var sanpham = new SanPhamFunction();
        //    var model = sanpham.GetListSanPham(searchString, page, pageSize);
        //    ViewBag.ChuoiTimKiemSP = searchString;
        //    return View(model);
        //}
    }
}