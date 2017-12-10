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
    public class 客戶聯絡人Controller : BaseController
    {


        // GET: 客戶聯絡人
        public ActionResult Index(string orderby,int? search, 客戶聯絡人 客)
        {
            var data = 客戶聯絡人Repo.FindAll();
            ViewData["姓名"] = String.IsNullOrEmpty(orderby) ? "姓名_desc" : "姓名";
            ViewData["姓名"] = orderby == "姓名" ? "姓名_desc" : "姓名";
            ViewData["職稱"] = orderby == "職稱" ? "職稱_desc" : "職稱";
            ViewData["Email"] = orderby == "Email" ? "Email_desc" : "Email";
            ViewData["手機"] = orderby == "手機" ? "手機_desc" : "手機";
            ViewData["電話"] = orderby == "電話" ? "電話_desc" : "電話";
            ViewData["客戶名稱"] = orderby == "客戶名稱" ? "客戶名稱_desc" : "客戶名稱";
            if (search == 1)
            {
                 data = 客戶聯絡人Repo.FindCondition(客);
                return View(data.ToList());
            }
            switch (orderby)
            {
                case "姓名_desc":
                    data = data.OrderByDescending(s => s.姓名);
                    break;
                case "姓名":
                    data = data.OrderBy(s => s.姓名);
                    break;
                case "職稱_desc":
                    data = data.OrderByDescending(s => s.職稱);
                    break;
                case "職稱":
                    data = data.OrderBy(s => s.職稱);
                    break;
                case "Email_desc":
                    data = data.OrderByDescending(s => s.Email);
                    break;
                case "Email":
                    data = data.OrderBy(s => s.Email);
                    break;
                case "手機_desc":
                    data = data.OrderByDescending(s => s.手機);
                    break;
                case "手機":
                    data = data.OrderBy(s => s.手機);
                    break;
                case "電話_desc":
                    data = data.OrderByDescending(s => s.電話);
                    break;
                case "電話":
                    data = data.OrderBy(s => s.電話);
                    break;
                case "客戶名稱_desc":
                    data = data.OrderByDescending(s => s.客戶資料.客戶名稱);
                    break;
                case "客戶名稱":
                    data = data.OrderBy(s => s.客戶資料.客戶名稱);
                    break;
                default:
                    data = data.OrderBy(s => s.姓名);
                    break;
            }
            return View(data.ToList());

        }
        [HttpPost]
        public ActionResult SearchColumns(Search_Columns_客戶聯絡人資料 columns)
        {
            客戶聯絡人 客聯資 = new 客戶聯絡人();
            客聯資.InjectFrom(columns);

            if (columns.客戶名稱 != null && columns.客戶名稱 != "")
            {
                var 客戶data = 客戶資料Repo.Find名稱(columns.客戶名稱);
                客聯資.客戶資料 = 客戶data;
            }
            string orderby = "";
            if (ViewData["客戶名稱"] != null)
            {
                orderby = (ViewData["客戶名稱"]).ToString();
            }
            return Index(orderby,1, 客聯資);
        }
        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = 客戶聯絡人Repo.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(客戶資料Repo.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 聯絡人)
        {
            if (ModelState.IsValid)
            {

                客戶聯絡人Repo.Add(聯絡人);
                客戶聯絡人Repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(客戶資料Repo.All(), "Id", "客戶名稱", 聯絡人.客戶Id);
            return View(聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = 客戶聯絡人Repo.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(客戶資料Repo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(客戶聯絡人).State = EntityState.Modified;
                // db.SaveChanges();
                客戶聯絡人 客 = 客戶聯絡人Repo.Find(客戶聯絡人.Id);
                客.InjectFrom(客戶聯絡人);
                客戶聯絡人Repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(客戶資料Repo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = 客戶聯絡人Repo.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = 客戶聯絡人Repo.Find(id);
            //db.客戶聯絡人.Remove(客戶聯絡人);
            客戶聯絡人.已刪除 = true;
            客戶聯絡人Repo.UnitOfWork.Commit();
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
        public ActionResult Excel_Export([Bind(Prefix = "客戶聯絡人")]IList<客戶聯絡人> customers)
        {

            DataSet ds = new DataSet();
            DataTable 客戶聯絡人Table = ds.Tables["客戶聯絡人"];
            var data = from c in customers
                       select new { c.職稱, c.姓名, c.Email, c.手機, c.電話, c.客戶資料.客戶名稱 };
            客戶聯絡人Table = LINQToDataTable(data);

            string date = GetSystemDate();
            string file_path = Server.MapPath("~/App_Data/Excel_客戶聯絡人" + date + ".xlsx");
            XLWorkbook wbook = new XLWorkbook();
            //IXLWorksheet wsheet = wbook.AddWorksheet("客戶資料");
            wbook.Worksheets.Add(客戶聯絡人Table, "客戶聯絡人");

            wbook.SaveAs(file_path);
            return File(
                file_path, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Excel_客戶聯絡人" + date + ".xlsx"
                );
        }
    }
}
