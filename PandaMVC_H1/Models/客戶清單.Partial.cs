namespace PandaMVC_H1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(客戶清單MetaData))]
    public partial class 客戶清單
    {
    }
    
    public partial class 客戶清單MetaData
    {
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 客戶名稱 { get; set; }
        public int? 聯絡人數量 { get; set; } = 0;
        public int? 銀行數量 { get; set; } = 0;
    }
}
