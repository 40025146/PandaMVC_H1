using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PandaMVC_H1.Controllers
{
    public class 客戶清單Controller : BaseController
    {
        // GET: 客戶清單
        public ActionResult Index()
        {
            var data = 客戶清單Repo.All();
            return View(data);
        }
    }
}