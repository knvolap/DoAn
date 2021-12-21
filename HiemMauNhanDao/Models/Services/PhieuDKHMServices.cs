using Models.EF;
using Models.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Services
{
   public class PhieuDKHMServices
    {
        private DbContextHM db = null;

        public PhieuDKHMServices()
        {
            db = new DbContextHM();
        }

        public List<PhieuDKHM> ListAllPDKHM()
        {
            return db.PhieuDKHMs.ToList();
        }
        public IEnumerable<PhieuDKHMView> GetListPDKHM(string keysearch, int page, int pagesize)
        {
            var query = from pdk in db.PhieuDKHMs
                        join tt in db.ThongTinCaNhans on pdk.idTTCN equals tt.IdTTCN
                        join dtc in db.DotToChucHMs on pdk.idDTCHM equals dtc.IdDTCHM
                        select new { pdk, tt,dtc };
            //check từ khóa có tồn tại hay k
            if (!string.IsNullOrEmpty(keysearch))
            {
                query = query.Where(x => x.pdk.idDTCHM.Contains(keysearch) || x.pdk.idPDKHM.Contains(keysearch) 
                || x.tt.IdTTCN.Contains(keysearch) || x.tt.hoTen.Contains(keysearch));
            }
            //tạo biến result -> hiển thị sp ->           
            var result = query.Select(x => new PhieuDKHMView()
            {
                IdTTCN = x.tt.IdTTCN,
                hoTen = x.tt.hoTen,
                gioiTinh = x.tt.gioiTinh,

                idPDKHM = x.pdk.idPDKHM,
                idDTCHM = x.dtc.IdDTCHM,
                tgDuKien = x.pdk.tgDuKien,
                trangThai = x.pdk.trangThai,             
            }).OrderByDescending(x => x.idDTCHM).ThenBy(q => q.idPDKHM).ToPagedList(page, pagesize);
            return result;
        }
        public PhieuDKHM GetByIdPDKHM(string id)
        {
            return db.PhieuDKHMs.Where(s => s.idPDKHM.CompareTo(id) == 0).SingleOrDefault();
        }
        public List<ThongTinCaNhan> GetTTCN()
        {
            return db.ThongTinCaNhans.ToList();
        }
        public List<DotHienMau> GetDHM()
        {
            return db.DotHienMaus.ToList();
        }

        public void ThemPDKHM(PhieuDKHM phieuDKHM)
        {
            var id = db.PhieuDKHMs.Max(x => x.idPDKHM);
            string phanDau = id.Substring(0, 3);
            int so = Convert.ToInt32(id.Substring(3, 2)) + 1;
            var pdkhm = new PhieuDKHM()
            {
                idPDKHM = so > 9 ? phanDau + so : phanDau + "0" + so,
                idDTCHM = phieuDKHM.idDTCHM,
                idTTCN         =phieuDKHM.idTTCN,			
                benhKhac       =phieuDKHM.benhKhac,		
                tgDuKien       =phieuDKHM.tgDuKien	,	
                sutCan         =phieuDKHM.sutCan	,		
                noiHach        =phieuDKHM.noiHach	,		
                chamCu         =phieuDKHM.chamCu	,		
                xamMinh        =phieuDKHM.xamMinh	,		
                duocTruyenMau  =phieuDKHM.duocTruyenMau	,
                suDungMatuy    =phieuDKHM.suDungMatuy	,	
                NguyCoHIV      =phieuDKHM.NguyCoHIV		,
                QHTD           =phieuDKHM.QHTD			,
                tiemVacXin     =phieuDKHM.tiemVacXin	,	
                dungTKS        =phieuDKHM.dungTKS		,	
                biSot          =phieuDKHM.biSot			,
                dTTT           =phieuDKHM.dTTT			,
                dangMangThai   =phieuDKHM.dangMangThai	,
                xacNhan        =phieuDKHM.xacNhan,
                trangThai = phieuDKHM.trangThai
            };
            db.PhieuDKHMs.Add(pdkhm);
            db.SaveChanges();
        }

    }
}
