using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Xml.Linq;
using System;
using System.IO;
using System.Web;
using WebApplicationn.Database.Dreamy;
using WebApplicationn.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Hosting.Server;

namespace WebApplicationn.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllProducts()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult Clients()
        {
            return View();
        }
        public IActionResult AddProduct(int productNo, Int64 price, int quantity, string productName, string category)
        {

            Cart cart = new Cart();

            cart.productNo = productNo;
            cart.price = price;
            cart.quantity = quantity;
            cart.productName = productName;
            cart.category = category;
            cart.finalPrice = price; //setting the initial value of finalPrice to the price of the product

            string output = Dreamy.AddToCart(cart);

            return MyCart();

        }
     
        public IActionResult PrivacyPolicy()
        {
            return View();
        }
        public IActionResult TermsAndConditions()
        {
            return View();
        }


        #region Cart
        [HttpGet]
        public IActionResult MyCart()
        {
            Int64 subtotal = Database.Dreamy.Dreamy.SubTotal();
            ViewBag.subtotal = subtotal;

            List<Cart> carts = Database.Dreamy.Dreamy.GetProducts();


            return View("~/views/home/cart.cshtml", carts);
        }

        [HttpGet]
        public IActionResult AddQuantity(int quantity, int prodNo, Int64 price)
        {

            quantity += 1;
            Int64 finalPrice = quantity * price;
            string str = Database.Dreamy.Dreamy.UpdateCart(quantity, prodNo, finalPrice);

            Int64 subtotal = Database.Dreamy.Dreamy.SubTotal();
            ViewBag.subtotal = subtotal;

            List<Cart> carts = Database.Dreamy.Dreamy.GetProducts();

            return PartialView("~/views/home/partialcart.cshtml", carts);
        }

        public IActionResult ReduceQuantity(int quantity, int prodNo, Int64 price)
        {
            quantity -= 1;
            if (quantity <= 0)
            {
                Database.Dreamy.Dreamy.RemoveFromCart(prodNo);

                Int64 subtotal = Database.Dreamy.Dreamy.SubTotal();
                ViewBag.subtotal = subtotal;
            }
            else
            {
                Int64 finalPrice = quantity * price;
                string str = Database.Dreamy.Dreamy.UpdateCart(quantity, prodNo, finalPrice);

                Int64 subtotal = Database.Dreamy.Dreamy.SubTotal();
                ViewBag.subtotal = subtotal;
            }

            List<Cart> carts = Database.Dreamy.Dreamy.GetProducts();

            return PartialView("~/views/home/partialcart.cshtml", carts);
        }


        #endregion

        [HttpGet]
        public IActionResult DeleteProduct(int prodNo)
        {

            Database.Dreamy.Dreamy.RemoveFromCart(prodNo);


            Int64 subtotal = Database.Dreamy.Dreamy.SubTotal();
            ViewBag.subtotal = subtotal;

            List<Cart> carts = Database.Dreamy.Dreamy.GetProducts();
            return PartialView("~/views/home/partialcart.cshtml", carts);

        }

        [HttpGet]
        public IActionResult PartialCart()
        {

            return PartialView("~/views/home/partialcart.cshtml");
        }


        #region allproducts
        
        public IActionResult Armchair()
        {
            return View("~/views/allproducts/armchair.cshtml");
        }

        public IActionResult MinimalRoseArmchair()
        {
            return View("~/views/allproducts/minimalrosearmchair.cshtml");
        }
        public IActionResult BedFrame()
        {
            return View("~/views/allproducts/bedframe.cshtml");
        }
        public IActionResult ModernSofa()
        {
            return View("~/views/allproducts/modernsofa.cshtml");
        }
        public IActionResult VintageCabinet()
        {
            return View("~/views/allproducts/vintagecabinet.cshtml");
        }
        public IActionResult DiningTable()
        {
            return View("~/views/allproducts/diningtable.cshtml");
        }
        public IActionResult DropLeafTable()
        {
            return View("~/views/allproducts/dropleaftable.cshtml");
        }
        public IActionResult ShoeCabinet()
        {
            return View("~/views/allproducts/shoecabinet.cshtml");
        }

        public IActionResult StorageBox()
        {
            return View("~/views/allproducts/storagebox.cshtml");
        }
        public IActionResult StorageCase()
        {
            return View("~/views/allproducts/storagecase.cshtml");
        }
        public IActionResult LargeStorage()
        {
            return View("~/views/allproducts/largestorage.cshtml");
        }
        public IActionResult SideTable()
        {
            return View("~/views/allproducts/sidetable.cshtml");
        }
        public IActionResult TableLamp()
        {
            return View("~/views/allproducts/tablelamp.cshtml");
        }
        public IActionResult LEDTableLamp()
        {
            return View("~/views/allproducts/ledtablelamp.cshtml");
        }
        public IActionResult LEDSpotlight()
        {
            return View("~/views/allproducts/ledspotlight.cshtml");
        }
        public IActionResult LEDSolarLamp()
        {
            return View("~/views/allproducts/ledsolarlamp.cshtml");
        }
        public IActionResult LEDLightingChain()
        {
            return View("~/views/allproducts/ledlightingchain.cshtml");
        }
        public IActionResult LEDSolarTableLamp()
        {
            return View("~/views/allproducts/ledsolartablelamp.cshtml");
        }
        public IActionResult WorkLamp()
        {
            return View("~/views/allproducts/worklamp.cshtml");
        }
        public IActionResult TableLampBulb()
        {
            return View("~/views/allproducts/tablelampbulb.cshtml");
        }

        #endregion


    }

}