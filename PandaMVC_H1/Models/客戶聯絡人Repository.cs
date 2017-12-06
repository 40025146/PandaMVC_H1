using System;
using System.Linq;
using System.Collections.Generic;
	
namespace PandaMVC_H1.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(p => p.已刪除 == false);
        }
        public override void Delete(客戶聯絡人 entity)
        {
            //base.Delete(entity);
            entity.已刪除 = true;
        }
        public IQueryable<客戶聯絡人> FindAll()
        {
            return this.All();
        }
        public 客戶聯絡人 Find(int? id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }
        public IQueryable<客戶聯絡人> Include篩掉被刪除( )
        {
            
            var data = this.All();


            return data;
        }
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{
       
    }
}