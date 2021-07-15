using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using abcMarket.Models;

namespace abcMarket.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Dictionary<string, int> products = new Dictionary<string, int>();
            //products.Add("MB", 15);
            //products.Add("VGA", 5);
            //products.Add("RAM", 3);
            //products.Add("SSD", 16);
            //products.Add("POWER", 17);
            //products.Add("CPU", 4);

            //foreach (KeyValuePair<string, int> product in products)
            //{
            //     ?? = Shop.GetProductsCategory(product.Value);
            //}
            cvmWebStore model = new cvmWebStore();
            List<string> cateList = new List<string>()
            {
                "A04" , "A03" , "A01" , "A05" , "A06" , "A02"
            };

            model.CarouseImages = Shop.GetCarouselImages();
            model.TopProducts = Shop.GetTopProducts();
            model.CategoryList = new List<Categorys>();
            foreach (var item in cateList)
            {
                model.CategoryList.Add(Shop.GetCategory(item));
            }
           

            //MB = Shop.GetProductsCategory(15),
            //VGA = Shop.GetProductsCategory(5),
            //RAM = Shop.GetProductsCategory(3),
            //SSD = Shop.GetProductsCategory(16),
            //POWER = Shop.GetProductsCategory(17),
            //CPU = Shop.GetProductsCategory(4),

            return View(model);
        }

        public JsonResult GetCategoryMenuList()
        {
            using (abcMarketEntities db = new abcMarketEntities())
            {
                int int_count = 0;
                string str_text = "";
                string str_href = "";
                List<Node> models = new List<Node>();

                var node1 = Shop.GetCategorys(0);
                if (node1.Count > 0)
                {
                    foreach (var item1 in node1)
                    {
                        int_count = Shop.GetProductCount(item1.rowid);
                        str_text = item1.category_name;
                        str_href = string.Format("/Product/CategoryList/{0}", item1.category_no);
                        Node model1 = new Node();
                        model1.text = str_text;

                        var node2 = Shop.GetCategorys(item1.rowid);
                        if (node2.Count == 0 && int_count > 0) model1.href = str_href;

                        if (node2.Count > 0)
                        {
                            foreach (var item2 in node2)
                            {
                                int_count = Shop.GetProductCount(item2.rowid);
                                str_text = item2.category_name;
                                str_href = string.Format("/Product/CategoryList/{0}", item2.category_no);
                                Node model2 = new Node();
                                model2.text = str_text;

                                var node3 = Shop.GetCategorys(item2.rowid);
                                if (node3.Count == 0 && int_count > 0) model2.href = str_href;

                                if (node3.Count > 0)
                                {
                                    foreach (var item3 in node3)
                                    {
                                        int_count = Shop.GetProductCount(item3.rowid);
                                        str_text = item3.category_name;
                                        str_href = string.Format("/Product/CategoryList/{0}", item3.category_no);
                                        Node model3 = new Node();
                                        model3.text = str_text;
                                        if (int_count > 0) model3.href = str_href;

                                        model2.nodes.Add(model3);
                                    }
                                }
                                model1.nodes.Add(model2);
                            }
                        }
                        models.Add(model1);
                    }
                }

                var jdata = Json(models, JsonRequestBehavior.AllowGet);
                return jdata;
            }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}