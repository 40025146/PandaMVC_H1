using System;
using System.Linq;
using System.Collections.Generic;
	
namespace PandaMVC_H1.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(p=>p.已刪除==false);
        }
        public override void Delete(客戶資料 entity)
        {
            //base.Delete(entity);
            entity.已刪除 = true;
        }
        public IQueryable<客戶資料> FindAll()
        {
            return this.All();
        }
        public 客戶資料 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }
       
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{
        
	}
}