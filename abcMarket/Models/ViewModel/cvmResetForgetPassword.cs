using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using abcMarket.Models;

public class cvmResetForgetPassword
{
    [Required(ErrorMessage = "電子信箱不可空白")]
    [Display(Name = "使用者電子信箱")]
    [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = false, HtmlEncode = true, NullDisplayText = "請輸入電子信箱")]
    [EmailAddress(ErrorMessage = "請輸入電子信箱格式")]
    public string user_email { get; set; }

    [Required(ErrorMessage = "請輸入新的密碼!!")]
    [DataType(DataType.Password)]
    [Display(Name = "新的密碼")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "請輸入確認密碼!!")]
    [DataType(DataType.Password)]
    [Display(Name = "確認密碼")]
    [Compare("NewPassword", ErrorMessage = "確認密碼不正確!!")]
    public string ConfirmPassword { get; set; }
}
