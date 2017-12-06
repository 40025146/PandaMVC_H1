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
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{
       
	}
}