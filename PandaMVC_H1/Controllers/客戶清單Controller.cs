using ClosedXML.Excel;
using Omu.ValueInjecter;
using PandaMVC_H1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PandaMVC_H1.Controllers
{
    public class 客戶清單Controller : BaseController
    {
        // GET: 客戶清單

        public ActionResult Index(string orderby, int? search, 客戶清單 客)
        {
            var data = 客戶清單Repo.All();
            ViewData["客戶名稱"] = String.IsNullOrEmpty(orderby) ? "客戶名稱_desc" : "客戶名稱";
            ViewData["客戶名稱"] = orderby == "客戶名稱" ? "客戶名稱_desc" : "客戶名稱";
            ViewData["聯絡人數量"] = orderby == "聯絡人數量" ? "聯絡人數量_desc" : "聯絡人數量";
            ViewData["銀行數量"] = orderby == "銀行數量" ? "銀行數量_desc" : "銀行數量";

            if (search == 1)
            {
                data = 客戶清單Repo.FindCondition(客);
                return View(data.ToList());
            }
            switch (orderby)
            {
                case "客戶名稱_desc":
                    data = data.OrderByDescending(s => s.客戶名稱);
                    break;
                case "客戶名稱":
                    data = data.OrderBy(s => s.客戶名稱);
                    break;
                case "聯絡人數量_desc":
                    data = data.OrderByDescending(s => s.聯絡人數量);
                    break;
                case "聯絡人數量":
                    data = data.OrderBy(s => s.聯絡人數量);
                    break;
                case "銀行數量_desc":
                    data = data.OrderByDescending(s => s.銀行數量);
                    break;
                case "銀行數量":
                    data = data.OrderBy(s => s.銀行數量);
                    break;
               
                default:
                    data = data.OrderBy(s => s.客戶名稱);
                    break;
            }
            return View(data.ToList());

        }
        [HttpPost]
        public ActionResult SearchColumns(Search_Columns_客戶清單資料 columns)
        {
            客戶清單 客聯資 = new 客戶清單();
            客聯資.InjectFrom(columns);

            
            string orderby = "";
            if (ViewData["客戶名稱"] != null)
            {
                orderby = (ViewData["客戶名稱"]).ToString();
            }
            return Index(orderby, 1, 客聯資);
        }
        [HttpPost]
        public ActionResult Excel_Export([Bind(Prefix = "客戶清單")]IList<客戶清單> customers)
        {

            DataSet ds = new DataSet();
            DataTable 客戶清單Table = ds.Tables["客戶清單"];
            var data = from c in customers
                       select new {  c.客戶名稱,c.聯絡人數量,c.銀行數量 };
            客戶清單Table = LINQToDataTable(data);

            string date = GetSystemDate();
            string file_path = Server.MapPath("~/App_Data/Excel_客戶清單" + date + ".xlsx");
            XLWorkbook wbook = new XLWorkbook();
            //IXLWorksheet wsheet = wbook.AddWorksheet("客戶資料");
            wbook.Worksheets.Add(客戶清單Table, "客戶清單");

            wbook.SaveAs(file_path);
            return File(
                file_path, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Excel_客戶清單" + date + ".xlsx"
                );
        }
    }
}