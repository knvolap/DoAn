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
            var result = db.TaiKhoans.SingleOrDefault(x => x.userName == UserName);
            if (result == null)
            {
                return 0;
            }
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

        public TaiKhoan GetById(string UserName)
        {
            return db.TaiKhoans.SingleOrDefault(x => x.userName == UserName);
        }
    }
}
