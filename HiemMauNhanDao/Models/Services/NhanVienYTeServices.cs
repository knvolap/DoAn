using Models.EF;
using Models.ViewModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Services
{
    
    public class NhanVienYTeServices
    {
        private DbContextHM db = null;

        public NhanVienYTeServices()
        {
            db = new DbContextHM();
        }
        public List<ThongTinCaNhan> ListTKNV()
        {
            return db.ThongTinCaNhans.ToList();
        }
        public List<NhanVienYTe> ListNV()
        {
            return db.NhanVienYTes.ToList();
        }
        public bool isExistIDTK(string userName)
        {
            NhanVienYTe kh = db.NhanVienYTes.Where(t => t.idTTCN == userName).FirstOrDefault();
            if (kh != null)
            {
                return true;
            }
            return false;
        }
        public bool isExistNVYT(string id)
        {
            NhanVienYTe kh = db.NhanVienYTes.Where(t => t.IdNVYT == id).FirstOrDefault();
            if (kh != null)
            {
                return true;
            }
            return false;
        }
        public bool isExistQuyen(string id)
        {
            ThongTinCaNhan kh = db.ThongTinCaNhans.Where(t => t.idQuyen == id).FirstOrDefault();
            if (kh != null)
            {
                return true;
            }
            return false;
        }
        public ThongTinCaNhan GetByIdTTCN(string id)
        {
            return db.ThongTinCaNhans.Where(s => s.IdTTCN.CompareTo(id) == 0).SingleOrDefault();
        }

        //DanhSachTK
        public IEnumerable<ThongTinCaNhan>ListAllPageTKNV (string searchString1, int page, int pageSize)
        {
            IEnumerable<ThongTinCaNhan> model = db.ThongTinCaNhans;
            if(!string.IsNullOrEmpty(searchString1))
            {
                model = model.Where(x => x.IdTTCN.Contains(searchString1) || x.userName.Contains(searchString1));
            }
            return model.OrderByDescending(x => x.idQuyen= "Q05").ToPagedList(page, pageSize);
        }

        public IEnumerable<NhanVienView> GetListNVYT(string keysearch , int page, int pagesize)
        {
            var query = from nv in db.NhanVienYTes                      
                        join tt in db.ThongTinCaNhans on nv.idTTCN equals tt.IdTTCN
                        join bv in db.BenhViens on nv.idBenhVien equals bv.IdBenhVien                     
                        select new { nv, tt,bv };                      
            //check từ khóa có tồn tại hay k
            if (!string.IsNullOrEmpty(keysearch))
            {
                query = query.Where(x => x.nv.IdNVYT.Contains(keysearch) || x.nv.idBenhVien.Contains(keysearch)
                || x.tt.IdTTCN.Contains(keysearch) || x.tt.hoTen.Contains(keysearch) || x.tt.soDT.Contains(keysearch) || x.tt.CCCD.Contains(keysearch));
            }
            //tạo biến result -> hiển thị sp ->           
            var result = query.Select(x => new NhanVienView()
            {
                IdTTCN = x.tt.IdTTCN,
                idQuyen = x.tt.idQuyen,
                hoTen = x.tt.hoTen,
                CCCD = x.tt.CCCD,
                IdNVYT = x.nv.IdNVYT,
                idBenhVien = x.nv.idBenhVien,
                tenChucVu = x.nv.tenChucVu,
                trangThai = x.nv.trangThai,
            }).OrderByDescending(x => x.IdNVYT).ThenBy(q => q.IdTTCN).ToPagedList(page, pagesize);
            return result;
        }


        public NhanVienView GetByIdNVYT2(string id)
        {
            var query = from nv in db.NhanVienYTes
                        join bv in db.BenhViens on nv.idBenhVien equals bv.IdBenhVien
                        join tt in db.ThongTinCaNhans on nv.idTTCN equals tt.IdTTCN
                        where nv.IdNVYT == id
                        select new { nv, tt,bv };
            var result = query.Select(x => new NhanVienView()
            {
               
                IdTTCN = x.tt.IdTTCN,
                CCCD = x.tt.CCCD,
                userName = x.tt.userName,
                password = x.tt.password,
                ngaySinh = x.tt.ngaySinh,
                coQuanTH = x.tt.coQuanTH,
                ngheNghiep = x.tt.ngheNghiep,
                trinhDo = x.tt.trinhDo,
                soLanHM = x.tt.soLanHM,
                soDT = x.tt.soDT,
                gioiTinh = x.tt.gioiTinh,
                nhomMau = x.tt.nhomMau,
                idQuyen = x.tt.idQuyen,
                hoTen = x.tt.hoTen,
                diaChi=x.tt.diaChi,

                IdNVYT = x.nv.IdNVYT,
                tenChucVu = x.nv.tenChucVu,
                trangThai = x.nv.trangThai,
                trinhDoCM = x.nv.trinhDoCM,
                khoa = x.nv.khoa,
                idBenhVien=x.bv.IdBenhVien,
                tenBenhVien = x.bv.TenBenhVien,
            }).SingleOrDefault();
            return result;
        }


        public void addNVYT(ThongTinCaNhan taiKhoan)
        {
            var id = db.ThongTinCaNhans.Max(x => x.IdTTCN);
            string phanDau = id.Substring(0, 2);
            int so = Convert.ToInt32(id.Substring(2, 2)) + 1;
            var nhanvien = new ThongTinCaNhan()
            {
                IdTTCN = so > 9 ? phanDau + so : phanDau + "0" + so,
                idQuyen = taiKhoan.idQuyen = "Q05",
                userName = taiKhoan.userName,
                password = taiKhoan.password,
                trangThai = taiKhoan.trangThai
            };
            db.ThongTinCaNhans.Add(nhanvien);
            db.SaveChanges();
        }
        public ThongTinCaNhan GetByIdNVYT(string id)
        {
            return db.ThongTinCaNhans.Where(s => s.IdTTCN.CompareTo(id) == 0).SingleOrDefault();
        }

        public void editNVYT(ThongTinCaNhan taiKhoan)
        {
            ThongTinCaNhan nvyt = GetByIdNVYT(taiKhoan.IdTTCN);
            nvyt.password = taiKhoan.password;

            db.SaveChanges();
        }

        //xoa
        public bool XoaNVYT(string id)
        {
            try
            {
                var xoaNVYT = db.NhanVienYTes.Find(id);
                db.NhanVienYTes.Remove(xoaNVYT);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //duuyet
        public bool ChangeStatus3(string id)
        {
            var chiTiet = db.NhanVienYTes.Find(id);
            chiTiet.trangThai = !chiTiet.trangThai;
            db.SaveChanges();
            return chiTiet.trangThai;
        }
        public BenhVien GetByIdBV1(string id)
        {
            return db.BenhViens.Where(x => x.IdBenhVien.CompareTo(id) == 0).SingleOrDefault();
        }

        public IEnumerable<NhanVienView> GetListNVYT2(string keysearch,string idbv, int page, int pagesize)
        {
            var query = from nv in db.NhanVienYTes
                        join tt in db.ThongTinCaNhans on nv.idTTCN equals tt.IdTTCN
                        join bv in db.BenhViens on nv.idBenhVien equals bv.IdBenhVien
                        where bv.IdBenhVien== idbv
                        select new { nv, tt, bv };
            
            //check từ khóa có tồn tại hay k
            if (!string.IsNullOrEmpty(keysearch))
            {
                query = query.Where(x => x.nv.IdNVYT.Contains(keysearch) || x.nv.idBenhVien.Contains(keysearch)
                || x.tt.IdTTCN.Contains(keysearch) || x.tt.hoTen.Contains(keysearch) || x.tt.soDT.Contains(keysearch));
            }
            //tạo biến result -> hiển thị sp ->           
            var result = query.Select(x => new NhanVienView()
            {
                IdTTCN = x.tt.IdTTCN,
                idQuyen = x.tt.idQuyen,
                hoTen = x.tt.hoTen,
                CCCD = x.tt.CCCD,
                IdNVYT = x.nv.IdNVYT,
                idBenhVien = x.bv.IdBenhVien,
                tenChucVu = x.nv.tenChucVu,
                trangThai = x.nv.trangThai,
            }).OrderByDescending(x => x.IdNVYT).ThenBy(q => q.IdTTCN).ToPagedList(page, pagesize);
            return result;
        }

        public IEnumerable<NhanVienView> GetListNVYT3(string keysearch, string id, int page, int pagesize)
        {
            var query = from dsnv in db.DSNVTHs
                        join nv in db.NhanVienYTes on dsnv.idNVYT equals  nv.IdNVYT
                        join tt in db.ThongTinCaNhans on nv.idTTCN equals tt.IdTTCN
                        join bv in db.BenhViens on nv.idBenhVien equals bv.IdBenhVien                       
                        join dtchm in db.DotToChucHMs on dsnv.idDTCHM equals dtchm.IdDTCHM

                        where dsnv.idNVYT==nv.IdNVYT && bv.IdBenhVien == id

                        select new { nv, tt, bv, dsnv, dtchm };

            //check từ khóa có tồn tại hay k           
            //tạo biến result -> hiển thị sp ->           
            var result = query.Select(x => new NhanVienView()
            {
                IdTTCN = x.tt.IdTTCN,
                idQuyen = x.tt.idQuyen,
                hoTen = x.tt.hoTen,
                CCCD = x.tt.CCCD,
                IdNVYT = x.nv.IdNVYT,
                idBenhVien = x.bv.IdBenhVien,
                tenChucVu = x.nv.tenChucVu,            
                trangThai = x.nv.trangThai,
                idDTCHM=x.dtchm.IdDTCHM,
                tenDTCHM=x.dtchm.tenDotHienMau,
                ngayToChuc=x.dtchm.ngayToChuc,
                NhiemVu = x.dsnv.nhiemVu,
            }).OrderByDescending(x => x.idDTCHM).ThenBy(q => q.IdNVYT).ToPagedList(page, pagesize);
            return result;
        }

    }
}
