using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace abcMarket.Models
{
    [MetadataType(typeof(ProductsMetaData))]
    public partial class Products
    {
        [NotMapped]
        [Display(Name = "首頁")]
        public bool bool_istop { get; set; }
        [NotMapped]
        [Display(Name = "熱門")]
        public bool bool_ishot { get; set; }
        [NotMapped]
        [Display(Name = "上架")]
        public bool bool_issale { get; set; }
        [NotMapped]
        [Display(Name = "主機板")]
        public bool bool_isMB { get; set; }
        [NotMapped]
        [Display(Name = "顯示卡")]
        public bool bool_isVGA { get; set; }
        [NotMapped]
        [Display(Name = "記憶體")]
        public bool bool_isRAM { get; set; }
        [NotMapped]
        [Display(Name = "硬碟")]
        public bool bool_isSSD { get; set; }
        [NotMapped]
        [Display(Name = "電源供應器")]
        public bool bool_isPOWER { get; set; }
        [NotMapped]
        [Display(Name = "CPU")]
        public bool bool_isCPU { get; set; }
        private class ProductsMetaData
        {
            [Key]
            [Display(Name = "記錄ID")]
            public int rowid { get; set; }
            [Display(Name = "分類ID")]
            public Nullable<int> categoryid { get; set; }
            [Display(Name = "商品分類")]
            public string category_name { get; set; }
            [Display(Name = "首頁顯示")]
            public Nullable<int> istop { get; set; }
            [Display(Name = "熱門商品")]
            public Nullable<int> ishot { get; set; }
            [Display(Name = "上架銷售")]
            public Nullable<int> issale { get; set; }
            [Display(Name = "主機板類")]
            public Nullable<int> isMB { get; set; }
            [Display(Name = "顯示卡類")]
            public Nullable<int> isVGA { get; set; }
            [Display(Name = "記憶體類")]
            public Nullable<int> isRAM { get; set; }
            [Display(Name = "硬碟類")]
            public Nullable<int> isSSD { get; set; }
            [Display(Name = "電源供應器類")]
            public Nullable<int> isPOWER { get; set; }
            [Display(Name = "CPU類")]
            public Nullable<int> isCPU { get; set; }

            //[Display(Name = "瀏覽次數")]
            //public Nullable<int> browse_count { get; set; }
            //[Display(Name = "廠商編號")]
            //[Required(ErrorMessage = "廠商編號不可空白")]
            //public string vendor_no { get; set; }
            [Display(Name = "商品編號")]
            [DisplayFormat(ConvertEmptyStringToNull = false, HtmlEncode = true, NullDisplayText = "請輸入商品編號")]
            [Required(ErrorMessage = "商品編號不可空白")]
            public string product_no { get; set; }
            [Display(Name = "商品名稱")]
            [DisplayFormat(ConvertEmptyStringToNull = false, HtmlEncode = true, NullDisplayText = "請輸入商品名稱")]
            [Required(ErrorMessage = "商品名稱不可空白")]
            public string product_name { get; set; }
            [Display(Name = "商品規格")]
            [DisplayFormat(ConvertEmptyStringToNull = false, HtmlEncode = true, NullDisplayText = "請輸入商品規格")]
            public string product_spec { get; set; }
            [Display(Name = "成本價格")]
            public Nullable<int> price_cost { get; set; }
            [Display(Name = "銷售價格")]
            public Nullable<int> price_sale { get; set; }
            [Display(Name = "促銷價格")]
            public Nullable<int> price_discont { get; set; }
            //[Display(Name = "推薦星數")]
            //public Nullable<int> start_count { get; set; }
            [Display(Name = "備註")]
            [DisplayFormat(ConvertEmptyStringToNull = false, HtmlEncode = true, NullDisplayText = "請輸入備註")]
            public string remark { get; set; }
        }
    }
}