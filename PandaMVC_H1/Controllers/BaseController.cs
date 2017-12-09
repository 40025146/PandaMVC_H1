using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PandaMVC_H1.Models;
namespace PandaMVC_H1.Controllers
{
    public class BaseController : Controller
    {
        protected 客戶資料Repository 客戶資料Repo = RepositoryHelper.Get客戶資料Repository();
        protected 客戶聯絡人Repository 客戶聯絡人Repo = RepositoryHelper.Get客戶聯絡人Repository();
        protected 客戶銀行資訊Repository 客戶銀行資訊Repo = RepositoryHelper.Get客戶銀行資訊Repository();
        protected 客戶清單Repository 客戶清單Repo = RepositoryHelper.Get客戶清單Repository();


    }
}