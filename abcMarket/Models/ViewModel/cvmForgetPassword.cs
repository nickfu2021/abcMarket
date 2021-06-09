using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using abcMarket.Models;


public class cvmForgetPassword
{
    [Display(Name = "使用者電子信箱*")]
    [Required(ErrorMessage ="電子信箱不可空白!!")]
    public string user_email { get; set; }
    [Display(Name = "驗證碼")]
    [Required(ErrorMessage = "驗證碼不可空白!!")]
    public string code { get; set; }
}
