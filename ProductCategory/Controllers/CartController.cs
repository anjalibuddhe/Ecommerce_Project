using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCategory.Models;

namespace ProductCategory.Controllers
{
    public class CartController : Controller
    {
        IConfiguration configuration;
        ProductCurd productCurd;
        CartCurd cartCurd;
    public CartController(IConfiguration configuration)
    {
        this.configuration = configuration;
            productCurd = new ProductCurd(this.configuration);
            cartCurd = new CartCurd(this.configuration);
    }
     
        // GET: CartController
        public ActionResult Index()
        {
            return View();
        }




        // POST: CartController/Create
        [HttpGet]
        public ActionResult AddToCart(int id)
        {
            try
            {
                Cart cart = new Cart();
                string UId = HttpContext.Session.GetString("userID");
                cart.UserID = Convert.ToInt32(UId);
                cart.Id = id;
                cart.Quantity = 1;
                int result = cartCurd.AddTOCart(cart);
                if (result == 1)
                    return RedirectToAction(nameof(ViewCart));
                else
                    return View();
            }
            catch (Exception ex)
            {
                return View();
            }

        }

        // GET: CartController/Edit/5
        public ActionResult ViewCart(int id)
        {
            string userID = HttpContext.Session.GetString("userID");
            var model = cartCurd.ViewCart(Convert.ToInt32(userID));
            return View(model);

        }
        public ActionResult RemoveCart(int id)
        {
            try
            {
                var result = cartCurd.DeleteCart(id);

                return RedirectToAction(nameof(ViewCart));

            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // POST: CartController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CartController/Delete/5
        public ActionResult Orders(int id)
        {
            var model = productCurd.GetProductById(id);
            return View(model);
        }

        // POST: CartController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
