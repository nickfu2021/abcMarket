using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using abcMarket.Models;


public class cvmForgetPassword
{
    [Required(ErrorMessage = "電子信箱不可空白")]
    [Display(Name = "電子信箱")]
    [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = false, HtmlEncode = true, NullDisplayText = "請輸入電子信箱")]
    [EmailAddress(ErrorMessage = "請輸入電子信箱格式")]
    public string user_email { get; set; }
    [Display(Name = "驗證碼")]
    [Required(ErrorMessage = "驗證碼不可空白!!")]
    public string code { get; set; }
}
