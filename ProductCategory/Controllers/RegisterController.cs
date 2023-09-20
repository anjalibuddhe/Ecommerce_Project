using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Win32;
using ProductCategory.Models;
using ProductCategory.UserData;

namespace ProductCategory.Controllers
{
    public class RegisterController : Controller
    {

        private readonly IConfiguration configuration;
        private RegisterCrud crud;
        private LoginCurd Lcrud;
        private ProductCurd Pcurd;

        public RegisterController(IConfiguration configuration)
        {
            this.configuration = configuration;
            crud=new RegisterCrud(this.configuration);
            Lcrud =new LoginCurd(this.configuration);
            Pcurd =new ProductCurd(this.configuration);

        }
       
        // GET: RegisterController
        public ActionResult UserProducts(int pg=1)
        {
            var model = Pcurd.GetProducts();
            const int pagesize = 8;
            if (pg < 1)
            {
                pg = 1;
            }
            int recscount = model.Count();

            var pager = new Pager(recscount, pg, pagesize);

            int recskip = (pg - 1) * pagesize;

            var data = model.Skip(recskip).Take(pager.PageSize).ToList();

            this.ViewBag.Pager = pager;
            return View(data);
        }

        // GET: RegisterController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RegisterController/Create
        public ActionResult RegisterUser()
        {
            return View();
        }

        // POST: RegisterController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser(Register register)
        {
            try
            {
                int result= crud.AddUser(register);
                if (result >= 1)
                    return RedirectToAction(nameof(Login));
                else
                    return View();
            }
            catch(Exception ex)
            {
                return View();
            }
        }
        public ActionResult Login(Login login)
        {
            try
            {
              
                var model = Lcrud.GetLogin(login.Email,login.Password);
                if (model != null)
                {
                    HttpContext.Session.SetString("RoleId", model.RoleId.ToString());
                    HttpContext.Session.SetString("userID", model.UserID.ToString());
                    HttpContext.Session.SetString("email", model.Email);
                    if (model.RoleId == 1)
                    {
                        return RedirectToAction("Index", "Product");
                    }
                    else if (model.RoleId == 0)
                    {
                        return RedirectToAction("UserProducts", "Register");
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }

     


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }
        // GET: RegisterController/Edit/5
        public ActionResult Edit(int userID)
        {
            var result = crud.GetUserById(userID);
            return View(result);
        }

        public ActionResult VeiwMore(int id)
        {
            var model = Pcurd.GetProductById(id);
            return View(model);
        }

        // POST: RegisterController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Register register)
        {
            try
            {
                int result = crud.UpdateUser(register);
                if (result == 1)
                    return RedirectToAction(nameof(Index));
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: RegisterController/Delete/5
        public ActionResult Delete(int userID)
        {
            var result=crud.DeleteUser(userID);
            return View(result);
        }

        // POST: RegisterController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int userID)
        {
            try
            {
                int result = crud.DeleteUser(userID);
                if (result == 1)
                    return RedirectToAction(nameof(Index));
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
