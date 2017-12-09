using System;
using System.Linq;
using System.Collections.Generic;
	
namespace PandaMVC_H1.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public class 客戶分類List
        {
            public int Id { get; set; }
            public string Type { get; set; }
        }
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
        public IQueryable<客戶資料> Select_客戶分類(string 客戶分類Type)
        {
            return this.All().Where(客=>客.客戶分類==客戶分類Type);
        }
        public IQueryable<客戶分類List> Get_客戶分類清單()
        {
            var list = new[]
            {
                new 客戶分類List{ Id=1,Type="大腸型"},
                new 客戶分類List{ Id=2,Type="大便型"},
                new 客戶分類List{ Id=3,Type="大腦型"}
            };
            return list.AsQueryable();
        }
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{
        
	}
}