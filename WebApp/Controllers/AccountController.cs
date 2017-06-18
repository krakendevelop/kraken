using System.Web.Mvc;
using System.Web.Security;
using BusinessLogic.Users.Auth;
using Common.Exceptions;
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
        return View(login);

      AuthUser authUser;
      var status = AuthManager.ValidateAndRead(login.Password, out authUser, login.Username, login.Email);
      if (status == AuthenticationStatus.Succesful)
      {
        if (authUser == null)
          throw new KrakenException("User was validated sucessfully but AuthUser is null");

        var cookieLogin = string.IsNullOrEmpty(login.Username) ? login.Email : login.Username;
        FormsAuthentication.SetAuthCookie(cookieLogin, true);
        ProcessSuccesfulLogin(authUser);

        return RedirectToAction("Index", "Home");
      }

      ModelState.AddModelError("", "The user name or password provided is incorrect.");
      return View(login);
    }

    public ActionResult Logout()
    {
      Session.Clear();
      FormsAuthentication.SignOut();
      ProcessLogOut();

      return RedirectToAction("Index", "Home");
    }
  }
}