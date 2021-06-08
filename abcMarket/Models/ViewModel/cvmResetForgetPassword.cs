using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using abcMarket.Models;

public class cvmResetForgetPassword
{
    [Display(Name = "使用者電子信箱")]
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
