using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HiemMauNhanDao.Common
{
    [Serializable]
    public class UserLogin
    {
  
        public string UserID { set; get; }
        public string AuthorID { set; get; }
       
        public string Name { set; get; }
        public string Accounts { set; get; }
        public string Password { set; get; }
        

    }
}