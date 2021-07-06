using abcMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class cvmWebStore
{
    public List<string> CarouseImages { get; set; }
    public List<Products> TopProducts { get; set; }
    public List<Products> ProductCategory { get; set; }
    public List<Products> MB { get; set; }
    public List<Products> VGA { get; set; }
    public List<Products> RAM { get; set; }
    public List<Products> SSD { get; set; }
    public List<Products> POWER { get; set; }
    public List<Products> CPU { get; set; }
}
