using System;
using System.Linq;
using System.Collections.Generic;

namespace PandaMVC_H1.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All().Where(p => p.已刪除 == false);
        }
        public override void Delete(客戶銀行資訊 entity)
        {
            //base.Delete(entity);
            entity.已刪除 = true;
        }
        public IQueryable<客戶銀行資訊> FindAll()
        {
             
            var data = this.All();
            return data;
        }
        public 客戶銀行資訊 Find(int? id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }
        public IQueryable<客戶銀行資訊> FindCondition(客戶銀行資訊 客)
        {

            var data = this.All();
            if (客.客戶資料 != null)
            {
                data = data.Where(
               p => p.銀行名稱.Contains(客.銀行名稱 ?? string.Empty)
               || p.銀行代碼 == (客.銀行代碼)
               || p.分行代碼 == (客.分行代碼)
               || p.帳戶名稱.Contains(客.帳戶名稱 ?? string.Empty)
               || p.帳戶號碼.Contains(客.帳戶號碼 ?? string.Empty)
               || p.客戶資料.Id == (客.客戶資料.Id)
               );
            }
            else
            {
                data = data.Where(
               p => p.銀行名稱.Contains(客.銀行名稱 ?? string.Empty)
               || p.銀行代碼 == (客.銀行代碼)
               || p.分行代碼 == (客.分行代碼)
               || p.帳戶名稱.Contains(客.帳戶名稱 ?? string.Empty)
               || p.帳戶號碼.Contains(客.帳戶號碼 ?? string.Empty)
               //                || p.客戶資料.Id == (客.客戶資料.Id)
               );
            }
            
            
            //.Where(p => p.電話 == 客.電話.ToString())
            //.Where(p => p.地址 == 客.地址.ToString())
            //.Where(p => p.Email == 客.Email.ToString())
            //.Where(p => p.客戶分類 == 客.客戶分類.ToString());


            return data;
        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{
       
	}
}