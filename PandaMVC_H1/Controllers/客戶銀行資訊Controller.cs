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
    public class 客戶銀行資訊Controller : BaseController
    {
        

        // GET: 客戶銀行資訊
        public ActionResult Index(int? search, 客戶銀行資訊 客)
        {
            if (search == 0)
            {
                var data = 客戶銀行資訊Repo.FindAll();
                return View(data.ToList());
            }
            else if (search == 1)
            {
                var data = 客戶銀行資訊Repo.FindCondition(客);
                return View(data.ToList());
            }
            else
            {
                var data = 客戶銀行資訊Repo.FindAll();
                return View(data.ToList());
            }
            return RedirectToAction("Index/0");
           
        }
        [HttpPost]
        public ActionResult SearchColumns(Search_Columns_客戶銀行資料 columns)
        {
            客戶銀行資訊 客銀資 = new 客戶銀行資訊();
            客銀資.InjectFrom(columns);
            if (columns.客戶名稱 != null && columns.客戶名稱!="")
            {
                var 客戶data = 客戶資料Repo.Find名稱(columns.客戶名稱);
                客銀資.客戶資料 = 客戶data;
            }
            return Index(1, 客銀資);
        }
        // GET: 客戶銀行資訊/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = 客戶銀行資訊Repo.Find(id);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(客戶資料Repo.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶銀行資訊/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
               客戶銀行資訊Repo.Add(客戶銀行資訊);
                客戶銀行資訊Repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(客戶資料Repo.All(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = 客戶銀行資訊Repo.Find(id);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(客戶資料Repo.All(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // POST: 客戶銀行資訊/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(客戶銀行資訊).State = EntityState.Modified;
                var data = 客戶銀行資訊Repo.Find(客戶銀行資訊.Id);
                data.InjectFrom(客戶銀行資訊);
                客戶銀行資訊Repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(客戶資料Repo.All(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = 客戶銀行資訊Repo.Find(id);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // POST: 客戶銀行資訊/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶銀行資訊 客戶銀行資訊 = 客戶銀行資訊Repo.Find(id);
            //db.客戶銀行資訊.Remove(客戶銀行資訊);
            客戶銀行資訊.已刪除=true;
            客戶銀行資訊Repo.UnitOfWork.Commit();
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
        public ActionResult Excel_Export([Bind(Prefix = "客戶銀行資訊")]IList<客戶銀行資訊> customers)
        {

            DataSet ds = new DataSet();
            DataTable 客戶銀行資訊Table = ds.Tables["客戶銀行資訊"];
            var data = from c in customers
                       select new { c.銀行名稱, c.銀行代碼, c.分行代碼, c.帳戶名稱, c.帳戶號碼, c.客戶資料.客戶名稱 };
            客戶銀行資訊Table = LINQToDataTable(data);

            string date = GetSystemDate();
            string file_path = Server.MapPath("~/App_Data/Excel_客戶銀行資訊" + date + ".xlsx");
            XLWorkbook wbook = new XLWorkbook();
            //IXLWorksheet wsheet = wbook.AddWorksheet("客戶資料");
            wbook.Worksheets.Add(客戶銀行資訊Table, "客戶銀行資訊");

            wbook.SaveAs(file_path);
            return File(
                file_path, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Excel_客戶銀行資訊" + date + ".xlsx"
                );
        }
    }
}
