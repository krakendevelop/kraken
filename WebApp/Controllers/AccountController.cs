using System.Web.Mvc;
using System.Web.Security;
using WebApp.Models;

namespace WebApp.Controllers
{
  public class AccountController : BaseController
  {
    public ActionResult Index()
    {
      return RedirectToAction("Login");
    }

    public ActionResult Login()
    {
      return View("Login");
    }

    [HttpPost]
    public ActionResult Login(LoginViewModel login)
    {
      if (!ModelState.IsValid)
      {
        ViewBag.Error = "Form is not valid; please review and try again.";
        return View("Login");
      }

      if (login.Email == "email@gmail.com" && login.Password == "password")
      {
        FormsAuthentication.SetAuthCookie(login.Email, true);
        return RedirectToAction("Index", "Home");
      }

      ViewBag.Error = "Credentials invalid. Please try again.";
      return View("Login");
    }

    public ActionResult Logout()
    {
      Session.Clear();
      FormsAuthentication.SignOut();
      return RedirectToAction("Index", "Home");
    }
  }
}