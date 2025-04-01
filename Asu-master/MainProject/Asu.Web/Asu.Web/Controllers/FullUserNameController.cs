using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asu.Web.Controllers
{
    public class FullUserName : Controller
    {
        public string Get_Full_Name(string fullName)
        {
            var _FIO = fullName.Split(new char[] { ' ' }, 3);
            string full_name = _FIO[0] + ' ' + _FIO[1].Substring(0, 1) + '.' + _FIO[2].Substring(0, 1) + '.';
            return full_name;
        }
    }
}