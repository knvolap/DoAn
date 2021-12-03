using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HiemMauNhanDao.Common
{
    public class UserLogin
    {
        [Required]
        public string UserID { set; get; }
        public string Accounts { set; get; }
        public string Password { set; get; }
        public string AuthorID { set; get; }
        public string Name { set; get; }

        public bool UserType { set; get; }
    }
}