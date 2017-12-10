using System;
using System.Linq;
using System.Collections.Generic;
	
namespace PandaMVC_H1.Models
{   
	public  class 客戶清單Repository : EFRepository<客戶清單>, I客戶清單Repository
	{
        public override IQueryable<客戶清單> All()
        {
            return base.All();
        }
        public IQueryable<客戶清單> FindCondition(客戶清單 客)
        {
            string str = 客.客戶名稱 ?? string.Empty;
            var data = this.All().Where(
                p => p.客戶名稱.Contains(客.客戶名稱 ?? string.Empty)
                || p.聯絡人數量==(客.聯絡人數量 )
                || p.銀行數量==(客.銀行數量 )
                );
            //.Where(p => p.電話 == 客.電話.ToString())
            //.Where(p => p.地址 == 客.地址.ToString())
            //.Where(p => p.Email == 客.Email.ToString())
            //.Where(p => p.客戶分類 == 客.客戶分類.ToString());


            return data;
        }
    }

	public  interface I客戶清單Repository : IRepository<客戶清單>
	{

	}
}