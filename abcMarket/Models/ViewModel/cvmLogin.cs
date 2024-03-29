﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


public class cvmLogin
{
    [Key]
    [Required(ErrorMessage = "請輸入信箱帳號!!")]
    [Display(Name = "登入帳號")]
    public string User_Email { get; set; }
    [Required(ErrorMessage = "請輸入登入密碼!!")]
    [DataType(DataType.Password)]
    [Display(Name = "登入密碼")]
    public string Password { get; set; }
    [Display(Name = "記住我")]
    public bool Remember { get; set; }
}
