using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PandaMVC_H1.Models;
using System.Data;
using System.Reflection;

namespace PandaMVC_H1.Controllers
{
    public class BaseController : Controller
    {
        protected 客戶資料Repository 客戶資料Repo = RepositoryHelper.Get客戶資料Repository();
        protected 客戶聯絡人Repository 客戶聯絡人Repo = RepositoryHelper.Get客戶聯絡人Repository();
        protected 客戶銀行資訊Repository 客戶銀行資訊Repo = RepositoryHelper.Get客戶銀行資訊Repository();
        protected 客戶清單Repository 客戶清單Repo = RepositoryHelper.Get客戶清單Repository();

        public class Search_Columns_客戶資料
        {
            public string 客戶名稱 { get; set; }
            public string 電話 { get; set; }
            public string 地址 { get; set; }
            public string Email { get; set; }
            public string 客戶分類 { get; set; }
        }
        public class Search_Columns_客戶銀行資料
        {
            public string 銀行名稱 { get; set; } = "";
            public int 銀行代碼 { get; set; } = 0;
            public Nullable<int> 分行代碼 { get; set; } = 0;
            public string 帳戶名稱 { get; set; } = "";
            public string 帳戶號碼 { get; set; } = "";
            public string 客戶名稱 { get; set; } = "";
        }
        public class Search_Columns_客戶聯絡人資料
        {
            public string 職稱 { get; set; }
            public string 姓名 { get; set; }
            public string Email { get; set; }
            public string 手機 { get; set; }
            public string 電話 { get; set; }
            public string 客戶名稱 { get; set; }
        }
        public class Search_Columns_客戶清單資料
        {
            public string 客戶名稱 { get; set; }
            public Nullable<int> 聯絡人數量 { get; set; }
            public Nullable<int> 銀行數量 { get; set; }
        }
        public DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others  will follow
                 if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
        public string  GetSystemDate()
        {
            DateTime dt = new DateTime();
            return dt.ToBinary().ToString();
        }
    }
}