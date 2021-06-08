using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using abcMarket.Models;


public class cvmForgetPassword
{
    [Display(Name = "使用者電子信箱")]
    public string user_email { get; set; }

}
