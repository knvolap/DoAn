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
    public class DonViLienKetServices
    {
        private DbContextHM db = null;

        public DonViLienKetServices()
        {
            db = new DbContextHM();
        }
        public IEnumerable<NhanVienvaDVLKView> ListAllDVLK(string searchString1, int page, int pageSize)
        {
            var query = from dv in db.DonViLienKets
                        join nv in db.ThongTinCaNhans on dv.idTTCN equals nv.IdTTCN
                        select new { dv, nv };

            //check từ khóa có tồn tại hay k
            if (!string.IsNullOrEmpty(searchString1))
            {
                query = query.Where(x => x.dv.idTTCN.Contains(searchString1) || x.dv.TenDonVi.Contains(searchString1) || x.nv.hoTen.Contains(searchString1)
                                     || x.dv.diaChi.Contains(searchString1) || x.dv.soDT.Contains(searchString1));
            }
            var result = query.Select(x => new NhanVienvaDVLKView()
            {
               IdTTCN      = x.nv.IdTTCN,
               idQuyen     = x.nv.idQuyen,
               hoTen       = x.nv.hoTen,
               CCCD       = x.nv.CCCD,
               IdDVLK      = x.dv.IdDVLK,
               TenDonVi    =x.dv.TenDonVi,
               diaChi      =x.dv.diaChi,
               Email       =x.dv.Email,
               soDT        =x.dv.soDT,
               minhChung   =x.dv.minhChung,
               trangThai   = x.dv.trangThai,
            }).OrderByDescending(x => x.IdDVLK).ThenBy(q => q.hoTen).ToPagedList(page, pageSize);
            return result;
        }
        public List<DonViLienKet> GetDonViLienKets2()
        {
            return db.DonViLienKets.ToList();
        }

        public ThongTinCaNhan GetByIdTTCN(string id)
        {
            return db.ThongTinCaNhans.Where(s => s.IdTTCN.CompareTo(id) == 0).SingleOrDefault();
        }

        public DonViLienKet GetByIdDVLK(string id)
        {
            return db.DonViLienKets.Where(s => s.IdDVLK.CompareTo(id) == 0).SingleOrDefault();
        }
        //Sửa
        public void SuaTKDVLK(ThongTinCaNhan thongTinCaNhan, string id)
        {
            ThongTinCaNhan tk = GetByIdTTCN(thongTinCaNhan.IdTTCN);
            var query = from dv in db.DonViLienKets
                        join nv in db.ThongTinCaNhans on dv.idTTCN equals nv.IdTTCN
                        where nv.IdTTCN == id
                        select new { dv, nv };
            var result = query.Select(x => new ThongTinCaNhan()
            {
                IdTTCN = x.nv.IdTTCN,
                idQuyen = x.nv.idQuyen,
                hoTen = x.nv.hoTen,
                CCCD = x.nv.CCCD,
                gioiTinh=x.nv.gioiTinh,
                diaChi=x.nv.diaChi,
                userName=x.nv.userName,
                ngaySinh=x.nv.ngaySinh,
                coQuanTH=x.nv.coQuanTH,
                trinhDo=x.nv.trinhDo,
                soLanHM=x.nv.soLanHM,
                nhomMau=x.nv.nhomMau,
                ngheNghiep=x.nv.ngheNghiep,
                soDT=x.nv.soDT
               
            }).SingleOrDefault();         
            db.SaveChanges();
        }
        public void UpdateDVLK(ThongTinCaNhan thongTinCaNhan)
        {
            ThongTinCaNhan tk = GetByIdTTCN(thongTinCaNhan.IdTTCN);
            tk.idQuyen = thongTinCaNhan.idQuyen;           
            db.SaveChanges();
        }

        public IEnumerable<ChiTietPDKHvsKQHMView> GetByIdDVLK(string keysearch, string id, int page, int pagesize)
        {
            var query = from pdk in db.PhieuDKHMs
                        join tt in db.ThongTinCaNhans on pdk.idTTCN equals tt.IdTTCN
                        join dtchm in db.DotToChucHMs on pdk.idDTCHM equals dtchm.IdDTCHM
                        join ctDHM in db.chiTietDHMs on dtchm.idChiTietDHM equals ctDHM.IdChiTietDHM
                        join dvlk in db.DonViLienKets on ctDHM.idDVLK equals dvlk.IdDVLK
                        where dvlk.IdDVLK == id

                        select new { pdk, tt, dtchm , dvlk , ctDHM };
            //check từ khóa có tồn tại hay k
            if (!string.IsNullOrEmpty(keysearch))
            {
                query = query.Where(x => x.pdk.idDTCHM.Contains(keysearch) || x.pdk.idPDKHM.Contains(keysearch)
                || x.tt.IdTTCN.Contains(keysearch) || x.tt.hoTen.Contains(keysearch) || x.tt.soDT.Contains(keysearch));
            }
            //tạo biến result -> hiển thị sp ->           
            var result = query.Select(x => new ChiTietPDKHvsKQHMView()
            {
                IdTTCN = x.tt.IdTTCN,
                hoTen = x.tt.hoTen,
                gioiTinh = x.tt.gioiTinh,
                soDT = x.tt.soDT,
                idPDKHM = x.pdk.idPDKHM,
                idDTCHM = x.pdk.idDTCHM,
                tenDTCHM = x.dtchm.tenDotHienMau,
                IdChiTietDHM=x.ctDHM.IdChiTietDHM,
                idDVLK=x.dvlk.IdDVLK,

            }).OrderByDescending(x => x.idDTCHM).ThenBy(q => q.idPDKHM).ToPagedList(page, pagesize);
            return result;
        }



        public NhanVienvaDVLKView GetByIdDVLK2(string id)
        {
            var query = from dv in db.DonViLienKets
                        join nv in db.ThongTinCaNhans on dv.idTTCN equals nv.IdTTCN
                        where dv.IdDVLK == id
                        select new { dv, nv };       
            var result = query.Select(x => new NhanVienvaDVLKView()
            {
                IdTTCN = x.nv.IdTTCN,
                idQuyen = x.nv.idQuyen,
                hoTen = x.nv.hoTen,
                CCCD = x.nv.CCCD,
                IdDVLK = x.dv.IdDVLK,
                TenDonVi = x.dv.TenDonVi,
                diaChi = x.dv.diaChi,
                Email = x.dv.Email,
                soDT = x.dv.soDT,
                minhChung = x.dv.minhChung,
                trangThai = x.dv.trangThai
            }).SingleOrDefault();
            return result;
        }

        public void AddDVLK(DonViLienKet donViLienKet, string fileName)
        {
            var id = db.DonViLienKets.Max(x => x.IdDVLK);
            string phanDau = id.Substring(0, 2);
            int so = Convert.ToInt32(id.Substring(2, 2)) + 1;          
            var dv1 = new DonViLienKet()
            {
                IdDVLK = so > 9 ? phanDau + so : phanDau + "0" + so,             
                idTTCN = donViLienKet.idTTCN,
                TenDonVi = donViLienKet.TenDonVi,
                diaChi = donViLienKet.diaChi,
                Email = donViLienKet.Email,
                minhChung = fileName,
                soDT = donViLienKet.soDT,
                trangThai = donViLienKet.trangThai
            };
            db.DonViLienKets.Add(dv1);
            db.SaveChanges();
        }

        public void EditDV(DonViLienKet donViLienKet, string fileName)
        {
            DonViLienKet dv = GetByIdDVLK(donViLienKet.IdDVLK);
            dv.TenDonVi = donViLienKet.TenDonVi;
            dv.Email = donViLienKet.Email;
            dv.diaChi = donViLienKet.diaChi;
            dv.minhChung = fileName;
            dv.soDT = donViLienKet.soDT;
            dv.trangThai = donViLienKet.trangThai;         
            db.SaveChanges();
        }

        public bool Delete(string id)
        {
            try
            {
                var ls = db.DonViLienKets.Find(id);
                db.DonViLienKets.Remove(ls);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }


        //duuyet
        public bool ChangeStatus2(string id)
        {
            var chiTiet = db.DonViLienKets.Find(id);
            chiTiet.trangThai = !chiTiet.trangThai;
            db.SaveChanges();
            return chiTiet.trangThai;
        }

        public bool isExistIDTK(string userName)
        {
            DonViLienKet kh = db.DonViLienKets.Where(t => t.idTTCN == userName).FirstOrDefault();
            if (kh != null)
            {
                return true;
            }
            return false;
        }
        public bool isExistDVLK(string id)
        {
            DonViLienKet kh = db.DonViLienKets.Where(t => t.IdDVLK == id).FirstOrDefault();
            if (kh != null)
            {
                return true;
            }
            return false;
        }
        public bool isExistSDT(string sdt)
        {
            DonViLienKet kh = db.DonViLienKets.Where(t => t.soDT == sdt).FirstOrDefault();
            if (kh != null)
            {
                return true;
            }
            return false;
        }
        public bool isExistEmail(string email)
        {
            DonViLienKet kh = db.DonViLienKets.Where(t => t.Email == email).FirstOrDefault();
            if (kh != null)
            {
                return true;
            }
            return false;
        }
        public bool isExistMinhChung(string mc)
        {
            DonViLienKet kh = db.DonViLienKets.Where(t => t.minhChung == mc).FirstOrDefault();
            if (kh != null)
            {
                return true;
            }
            return false;
        }
    }
}
