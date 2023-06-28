using LoginLogoutUsingIdentity.Services.Abstration;
using LoginLogoutUsingIdentity.Services.Implementation;
using LoginLogoutUsingIdentity.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LoginLogoutUsingIdentity.Controllers
{
    public class UserAuthenticateController : Controller
    {
        private readonly IAccountService _accountService;
        public UserAuthenticateController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginVM loginVM)
        {
            if(!ModelState.IsValid)
            {
                return View(loginVM);
            }
            var res=_accountService.LoginAsync(loginVM);
            if(res.Result.status==0)
            {
                return RedirectToAction("Display", "Dashboard");
            }
            TempData["message"] = res.Result.Message;
            return RedirectToAction(nameof(Login));
        }
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return RedirectToAction(nameof(Login));
             
        }
        public async Task<IActionResult> Registration()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationVM registrationVM)
        {
            if (!ModelState.IsValid)
            {
                return View(registrationVM);
            }
            registrationVM.Role = "user";
            var res = await _accountService.RegistrationAsync(registrationVM);
            TempData["message"]=res.Message;
            return RedirectToAction(nameof(Registration));
        }

    //    //for creating once 
    //    public async Task<IActionResult> Register()
    //    {
    //        var registrationVM=new RegistrationVM
    //        {
    //            Name="Amit",
    //            UserName="amit123",
    //            Password= "amit123",
    //            Email= "amit123@gmail.com",
    //        };
    //        registrationVM.Role="admin";
    //        var res =await _accountService.RegistrationAsync(registrationVM);

    //        return Ok(res);
    //    }
    }
}
