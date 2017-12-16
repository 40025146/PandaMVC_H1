using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PandaMVC_H1.Models;
using Omu.ValueInjecter;
using ClosedXML.Excel;

namespace PandaMVC_H1.Controllers
{
    public class 客戶資料Controller : BaseController
    {

        public class Customer
        {
            public int Id { get; set; }
            public string 客戶名稱 { get; set; }
            public string 統一編號 { get; set; }
            public string 電話 { get; set; }
            public string 傳真 { get; set; }
            public string 地址 { get; set; }
            public string Email { get; set; }
        }
        // GET: 客戶資料
        public ActionResult Index(string orderby, int? search, 客戶資料 客)
        {
            ViewBag.客戶分類 = new SelectList(客戶資料Repo.Get_客戶分類清單(), dataTextField: "Type", dataValueField: "Type");
            ViewData["客戶名稱"] = String.IsNullOrEmpty(orderby) ? "客戶名稱_desc" : "客戶名稱";
            ViewData["客戶名稱"] = orderby == "客戶名稱" ? "客戶名稱_desc" : "客戶名稱";
            ViewData["統一編號"] = orderby == "統一編號" ? "統一編號_desc" : "統一編號";
            ViewData["電話"] = orderby == "電話" ? "電話_desc" : "電話";
            ViewData["傳真"] = orderby == "傳真" ? "傳真_desc" : "傳真";
            ViewData["地址"] = orderby == "地址" ? "地址_desc" : "地址";
            ViewData["Email"] = orderby == "Email" ? "Email_desc" : "Email";
            ViewData["客戶分類sort"] = orderby == "客戶分類sort" ? "客戶分類sort_desc" : "客戶分類sort";
            var data = 客戶資料Repo.FindAll();
            if (search == 1)
            {
                data = 客戶資料Repo.FindCondition(客);

            }
            switch (orderby)
            {
                case "客戶名稱_desc":
                    data = data.OrderByDescending(s => s.客戶名稱);
                    break;
                case "客戶名稱":
                    data = data.OrderBy(s => s.客戶名稱);
                    break;
                case "統一編號_desc":
                    data = data.OrderByDescending(s => s.統一編號);
                    break;
                case "統一編號":
                    data = data.OrderBy(s => s.統一編號);
                    break;
                case "電話_desc":
                    data = data.OrderByDescending(s => s.電話);
                    break;
                case "電話":
                    data = data.OrderBy(s => s.電話);
                    break;
                case "傳真_desc":
                    data = data.OrderByDescending(s => s.傳真);
                    break;
                case "傳真":
                    data = data.OrderBy(s => s.傳真);
                    break;
                case "地址_desc":
                    data = data.OrderByDescending(s => s.地址);
                    break;
                case "地址":
                    data = data.OrderBy(s => s.地址);
                    break;
                case "Email_desc":
                    data = data.OrderByDescending(s => s.Email);
                    break;
                case "Email":
                    data = data.OrderBy(s => s.Email);
                    break;
                case "客戶分類sort_desc":
                    data = data.OrderByDescending(s => s.客戶分類);
                    break;
                case "客戶分類sort":
                    data = data.OrderBy(s => s.客戶分類);
                    break;
                default:
                    data = data.OrderBy(s => s.客戶名稱);
                    break;
            }

            return View(data.ToList());
        }
        [HttpPost]
        public ActionResult SearchColumns(Search_Columns_客戶資料 columns)
        {
            string orderby = "";
            if (ViewData["客戶名稱"] != null)
            {
                orderby = (ViewData["客戶名稱"]).ToString();
            }

            客戶資料 客資 = new 客戶資料();
            客資.InjectFrom(columns);

            return Index(orderby, 1, 客資);
        }
        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = 客戶資料Repo.Find((int)id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶分類 = new SelectList(客戶資料Repo.Get_客戶分類清單());
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            ViewBag.客戶分類 = new SelectList(客戶資料Repo.Get_客戶分類清單(), dataTextField: "Type", dataValueField: "Type");
            return View();
        }

        // POST: 客戶資料/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {

                客戶資料Repo.Add(客戶資料);
                客戶資料Repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = 客戶資料Repo.Find((int)id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            string type = 客戶資料.客戶分類.Trim();
            ViewBag.客戶分類 = new SelectList(items: 客戶資料Repo.Get_客戶分類清單(), dataTextField: "Type", dataValueField: "Type", selectedValue: type);
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email,客戶分類")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(客戶資料).State = EntityState.Modified;
                客戶資料 客 = 客戶資料Repo.Find(客戶資料.Id);
                客.InjectFrom(客戶資料);
                客戶資料Repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = 客戶資料Repo.Find((int)id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = 客戶資料Repo.Find(id);
            客戶資料Repo.Delete(客戶資料);
            客戶資料Repo.UnitOfWork.Commit();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult Excel_Export([Bind(Prefix = "客戶資料")]IList<客戶資料> customers)
        {

            DataSet ds = new DataSet();
            DataTable 客戶資料Table = ds.Tables["客戶資料"];
            var data = from c in customers
                       select new { c.客戶名稱, c.統一編號, c.電話, c.傳真, c.地址, c.Email, c.客戶分類 };
            客戶資料Table = LINQToDataTable(data);

            string date = GetSystemDate();
            string file_path = Server.MapPath("~/App_Data/Excel_客戶資料" + date + ".xlsx");
            XLWorkbook wbook = new XLWorkbook();
            //IXLWorksheet wsheet = wbook.AddWorksheet("客戶資料");
            wbook.Worksheets.Add(客戶資料Table, "客戶資料");

            wbook.SaveAs(file_path);
            return File(
                file_path, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Excel_客戶資料" + date + ".xlsx"
                );
        }


        public ActionResult Edit_list()
        {
            var data = 客戶資料Repo.All();



            return View(data);
        }
        [HttpPost]
        public ActionResult Edit_Patch(Customer[] Customer)
        {
            if (ModelState.IsValid)
            {
                foreach (var 客 in Customer)
                {
                    var 客Item = 客戶資料Repo.Find(客.Id);
                    客Item.InjectFrom(客);
                    客戶資料Repo.UnitOfWork.Commit();
                }
                
            }

            return RedirectToAction("Edit_list");
        }
    }
}
