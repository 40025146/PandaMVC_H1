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
    }

	public  interface I客戶清單Repository : IRepository<客戶清單>
	{

	}
}