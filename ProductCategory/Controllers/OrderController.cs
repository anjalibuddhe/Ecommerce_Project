using Microsoft.AspNetCore.Mvc;
using ProductCategory.Models;
using ProductCategory.UserData;

namespace ProductCategory.Controllers
{

    public class OrderController : Controller
    {

        IConfiguration configuration;
        ProductCurd productCrud;
        CartCurd cartCRUD;
        CartDetailsCurd orderCrud;
        RegisterCrud userCrud;
      
        public OrderController(IConfiguration configuration)
            {
                this.configuration = configuration;
                productCrud = new ProductCurd(this.configuration);
                userCrud = new RegisterCrud(this.configuration);
                cartCRUD = new CartCurd(this.configuration);
                 orderCrud = new CartDetailsCurd(this.configuration);

            }
            // GET: OrderController
            //[HttpGet]
            //public ActionResult Order()
            //{
            //    string uid = HttpContext.Session.GetString("uid");
            //    var model = cartCRUD.ViewCart(Convert.ToInt32(uid));

            //    return View(model);


            //}


            //GET: OrderController/Details/5

            [HttpGet]
            public ActionResult GetOrder(int id)
            {
                try
                {

                    //string uid = HttpContext.Session.GetString("uid");

                    var model = productCrud.GetProductById(id);

                    return View(model);
                }
                catch (Exception ex)
                {
                    return View();
                }
            }
            [HttpGet]
            public ActionResult GetOrderConfirm(int id)
            {
                try
                {
                CartDetails order = new CartDetails();
                    string uid = HttpContext.Session.GetString("userID");
                    order.UserID = Convert.ToInt32(uid);
                    order.Id = id;
                    order.Quantity = 1;
                    int result = orderCrud.AddToOrder(order);
                    if (result == 1)
                        return RedirectToAction(nameof(MyOrder));
                    else
                        return View();
                }
                catch (Exception ex)
                {
                    return View();
                }
            }

            [HttpGet]
            //[ActionName("Order")]
            public ActionResult ConfirmOrder()
            {
                return View();
            }
            // GET: OrderController/Delete/5
            public ActionResult AddToOrder(int id)
            {
                return View();
            }

            ////POST: OrderController/Delete/5
            //[HttpGet]


            public IActionResult MyOrder()
            {
                try
                {
                    string uid = HttpContext.Session.GetString("userID");
                    var result = orderCrud.MyOrder(Convert.ToInt32(uid));
                    return View(result);
                }
                catch (Exception)
                {

                    return View();
                }


            }
        

    }
}

