using eTickets.Data;
using eTickets.Data.Static;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace eTickets.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }


        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }


        public IActionResult Login() => View(new LoginVM());

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if(user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                TempData["Error"] = "Wrong password. Please, try again!";
                return View(loginVM);
            }

            TempData["Error"] = "Please enter valid user name and try again!";

                TempData["Error"] = "Wrong credentials. Please, try again!";
                return View(loginVM);
            
           
        }


        public IActionResult Register() => View(new RegisterVM());

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if(user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerVM);
            }

            var newUser = new ApplicationUser()
            {
                FullName = registerVM.FullName,
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress,
                PhoneNumber = registerVM.PhoneNumber
 
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);

            return View("RegisterCompleted");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movies");
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }


        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM forgotPasswordVm)
        {
            if (!ModelState.IsValid) return View(forgotPasswordVm);

            var user = await _userManager.FindByEmailAsync(forgotPasswordVm.EmailAddress);
            
            if(user == null)
            {
                TempData["Error"] = "This email address doesn't exist";
                return View(forgotPasswordVm);
            }
            else
            {
                var phone = await  _userManager.GetPhoneNumberAsync(user);
                var res = SendOTP(phone);
                TempData["success"] = res.message;
                TempData["email"] = forgotPasswordVm.EmailAddress;
                return View("ResetPassword");
            }
        }
        private static void ServiceManager()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls12;
               // | SecurityProtocolType.Ssl3;

            ServicePointManager.ServerCertificateValidationCallback =
                new RemoteCertificateValidationCallback(
                    delegate
                    {
                        return true;
                    }
                );
        }
        public static OTPResponse SendOTP(string phone)
        {
            try
            {
                 ServiceManager();
                var data = new NameValueCollection();
                var client = new WebClient();
                data["secret"] = "680a8f3796bdec685b537cde0a6749f209c82d5f";
                data["mode"] = "credits";
                data["type"] = "sms";
                data["gateway"] = "4";
                data["message"] = "Your otp is {{otp}}";
                data["phone"] = phone;
                string singleurl = "https://sms.genius.ke/api/send/otp";

                var responseBytes = client.UploadValues(singleurl, "POST", data);
                string responseStr = Encoding.UTF8.GetString(responseBytes);
                Console.WriteLine(responseStr);
                OTPResponse response = JsonConvert.DeserializeObject<OTPResponse>(
                    responseStr
                );
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public class OTPResponse
        {
            public string status { get; set; }
            public string message { get; set; }
            public Data data { get; set; }
        }
        
        public class  Data
        {
            public string phone { get; set; }
            public string message { get; set; }
            public int otp { get; set; }
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM resetPasswordVm)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordVm.EmailAddress);
            if (user != null)
            {
                Console.WriteLine(user);
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                
                var password = await _userManager.ResetPasswordAsync(user, token, resetPasswordVm.NewPassword);
                if (password.Succeeded)
                {
                    return View("Login");
                }
                else
                {
                    TempData["Error"] = "An error occured Please try again";
                    return View("ForgotPassword");
                }
                
            }
            else
            {
                TempData["Error"] = "An error occured!!";
                return View("ForgotPassword");
            }
            
        }

    }
}
