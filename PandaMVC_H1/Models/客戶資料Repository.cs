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

        public IQueryable<客戶資料> FindCondition(客戶資料 客)
        {
            string str = 客.客戶名稱 ?? string.Empty;
            var data = this.All().Where(
                p => p.客戶名稱. Contains(客.客戶名稱 ?? string.Empty)
                || p.Email.Contains(客.Email ?? string.Empty)
                || p.地址.Contains(客.地址 ?? string.Empty)
                || p.電話.Contains(客.電話 ?? string.Empty)
                || p.客戶分類.Contains(客.客戶分類 ?? string.Empty)
                );
                //.Where(p => p.電話 == 客.電話.ToString())
                //.Where(p => p.地址 == 客.地址.ToString())
                //.Where(p => p.Email == 客.Email.ToString())
                //.Where(p => p.客戶分類 == 客.客戶分類.ToString());
            

            return data;
        }
        public IQueryable<客戶資料> Select_客戶分類(string 客戶分類Type)
        {
            return this.All().Where(客=>客.客戶分類==客戶分類Type);
        }
        public IQueryable<客戶分類List> Get_客戶分類清單()
        {
            var list = new[]
            {
                new 客戶分類List{ Id=0,Type=""},
                new 客戶分類List{ Id=1,Type="大腸型"},
                new 客戶分類List{ Id=2,Type="大便型"},
                new 客戶分類List{ Id=3,Type="大腦型"}
            };
            return list.AsQueryable();
        }

        public 客戶資料 Find名稱(string name)
        {
            if (name != null)
            {
                var data = this.All().Where(p => p.客戶名稱.Contains(name));
                return data.FirstOrDefault();
            }
            else
            {
                return null;
            }
            
        }
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{
        
	}
}