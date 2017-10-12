using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using erp.Models;

namespace erp.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        // GET: Test
        public ActionResult Test(int id=0)
        {
            sys_user sysuser = new sys_user();
            using (erpsEntities db = new erpsEntities())
            {
                if (id != 0)
                {
                    sysuser = db.sys_user.Where(x => x.id == id).FirstOrDefault();
                    sysuser.UserDD = db.sys_user.ToList<sys_user>();
                }else
                {
                    sysuser.UserDD = db.sys_user.ToList<sys_user>();
                }
                
            }
                return View(sysuser);
        }

        // POST: Test
        [HttpPost]
        public ActionResult Test(sys_user user)
        {
           
            return View();
        }
    }
}