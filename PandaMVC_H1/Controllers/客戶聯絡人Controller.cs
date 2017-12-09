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

namespace PandaMVC_H1.Controllers
{
    public class 客戶聯絡人Controller : BaseController
    {


        // GET: 客戶聯絡人
        public ActionResult Index(int? search, 客戶聯絡人 客)
        {
            if (search == 0)
            {
                var data = 客戶聯絡人Repo.FindAll();
                return View(data.ToList());
            }
            else if (search == 1)
            {
                var data = 客戶聯絡人Repo.FindCondition(客);
                return View(data.ToList());
            }
            else
            {
                var data = 客戶聯絡人Repo.FindAll();
                return View(data.ToList());
            }
            return RedirectToAction("Index/0");

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
            return Index(1, 客聯資);
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
    }
}
