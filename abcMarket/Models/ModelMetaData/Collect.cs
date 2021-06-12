using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace abcMarket.Models
{
    [MetadataType(typeof(CollectMetaDeta))]
    public partial class Collect
    {
        private class CollectMetaDeta
        {
            [Key]
            [Display(Name = "記錄ID")]
            public int rowid { get; set; }
            [Display(Name = "電子郵件")]
            public string user_email { get; set; }
            [Display(Name = "商品編號")]
            public string product_no { get; set; }
            [Display(Name ="商品名稱")]
            public string product_name { get; set; }
            [Display(Name ="商品規格")]
            public string product_spec { get; set; }
            [Display(Name ="單價")]
            public Nullable<int> price { get; set; }
            [Display(Name ="金額")]
            public Nullable<int> amount { get; set; }
            [Display(Name ="建立時間")]
            public Nullable<System.DateTime> create_time { get; set; }
        }
    }
}