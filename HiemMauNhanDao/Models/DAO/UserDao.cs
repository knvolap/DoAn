using System;
using System.Collections.Generic;
using System.Data.Entity;
using Models.EF;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class UserDao
    {
        private DbContextHM db;
        public UserDao()
        {
            db = new DbContextHM();

        }
        //Đăng nhập
        public int Login(string UserName, string PassWord)
        {
           // var Session= db.Quyens.SingleOrDefault(x => x.IdQuyen == id);

            // return 0: Tài khoản hoặc mật khẩu không chính xác
            // return 1: đăng nhập thành cônh
            //return -1: tài khoản bị khoá
            //retunrn -2: Không có quyền truy cập
            var result = db.TaiKhoans.SingleOrDefault(x => x.userName == UserName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if(result.idQuyen.Contains("Q01") || result.idQuyen.Contains("Q02"))
                {
                    if (result.trangThai == false) { return -1; }
                    else
                    {
                        if (result.password == PassWord)
                        {

                            return 1;
                        }
                        else
                            return 0;
                    }
                }
                else
                {
                    return -2;
                }
            }
        }

        public int Login2(string UserName, string PassWord)
        {
            // var Session= db.Quyens.SingleOrDefault(x => x.IdQuyen == id);

            // return 0: Tài khoản hoặc mật khẩu không chính xác
            // return 1: đăng nhập thành cônh
            //return -1: tài khoản bị khoá
            //retunrn -2: Không có quyền truy cập
            var result = db.TaiKhoans.SingleOrDefault(x => x.userName == UserName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (result.idQuyen.Contains("Q03") || result.idQuyen.Contains("Q04") || result.idQuyen.Contains("Q05") || result.idQuyen.Contains("Q06"))
                {
                    if (result.trangThai == false) { return -1; }
                    else
                    {
                        if (result.password == PassWord)
                        {

                            return 1;
                        }
                        else
                            return 0;
                    }
                }
                else
                {
                    return -2;
                }
            }
        }

        public TaiKhoan GetById(string UserName)
        {
            return db.TaiKhoans.SingleOrDefault(x => x.userName == UserName);
        }
        public Quyen GetByIdQuyen(string id)
        {
            return db.Quyens.SingleOrDefault(x => x.IdQuyen == id);
        }
    }
}
