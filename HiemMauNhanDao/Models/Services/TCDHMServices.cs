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
    public class TCDHMServices
    {
        private DbContextHM db = null;
    
        public TCDHMServices()
        {
            db = new DbContextHM();
        }
        public IEnumerable<DotToChucHM> ListAlldTCHM(string keysearch, int page, int pageSize)
        {
            IEnumerable<DotToChucHM> model = db.DotToChucHMs;
            if (!string.IsNullOrEmpty(keysearch))
            {
                model = model.Where(x => x.IdDTCHM.Contains(keysearch) || x.tenDotHienMau.Contains(keysearch) || x.noiDung.Contains(keysearch));
            }
            return model.OrderBy(x => x.IdDTCHM).ThenByDescending(x => x.tenDotHienMau).ToPagedList(page, pageSize);
        }

        public List<DotToChucHM> GetListDTCHM()
        {
            return db.DotToChucHMs.OrderByDescending(x => x.IdDTCHM).ThenBy(x => x.tenDotHienMau).ToList( );             
        }

        public DotToChucHM GetByIdTCHM(string id)
        {
            return db.DotToChucHMs.Where(b => b.IdDTCHM.CompareTo(id) == 0).FirstOrDefault();
        }

        public BaiDangView GetByIdTCHM2( string id)
        {
            var query = from dthm in db.DotToChucHMs
                        join ct in db.chiTietDHMs on dthm.idChiTietDHM equals ct.IdChiTietDHM
                        join bv in db.BenhViens on ct.idBenhVien equals bv.IdBenhVien
                        join dv in db.DonViLienKets on ct.idDVLK equals dv.IdDVLK
                        where dthm.IdDTCHM==id
                        select new { ct, dthm, dv, bv };

            //check từ khóa có tồn tại hay k
         
            var result = query.Select(x => new BaiDangView()
            {
               IdDTCHM=x.dthm.IdDTCHM,
               tenDotHienMau=x.dthm.tenDotHienMau,
               noiDung=x.dthm.noiDung,
               soLuong = x.dthm.soLuong,
               doiTuongThamGia = x.dthm.doiTuongThamGia,
               diaChiToChuc = x.dthm.diaChiToChuc,
               ngayBatDauDK = x.dthm.ngayBatDauDK,
               ngayKetThucDK = x.dthm.ngayKetThucDK,
               ngayToChuc = x.dthm.ngayToChuc,
               tenNguoiDangBai = x.dthm.tenNguoiDangBai,
               idDVLK = x.dv.IdDVLK,
               sdtDVLK = x.dv.soDT,
               tenDVLK = x.dv.TenDonVi,
               idBenhVien = x.bv.IdBenhVien,
               sdtBV = x.bv.soDTBV,
               tenBV = x.bv.TenBenhVien,
               idCTDK = x.ct.IdChiTietDHM,
            }).SingleOrDefault();
            return result;
        }




        public List<BenhVien> ListAllLLeftMenuBV()
        {
            return db.BenhViens.ToList();
        }
        public BenhVien ListAllLLeftMenuBV(string id)
        {
            return db.BenhViens.Where(s => s.IdBenhVien.CompareTo(id) == 0).FirstOrDefault();

        }

    }
}
