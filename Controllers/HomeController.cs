
using CookieDisclaimer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace CookieDisclaimer.Controllers
{
    public class CookieViewModel
    {
        public ICollection<string> cookies { get; set; }
    }
    public class HomeController : Controller
    {
        private const string Key = "CookieDisclamer";
        private string Value = "No";
        private readonly CookieDisclamerViewModel viewModel = new CookieDisclamerViewModel();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return CheckCookieValue();
        }

        public IActionResult Privacy()
        {
            return CheckCookieValue();
        }

        public IActionResult ContactUs()
        {
            return CheckCookieValue();
        }
        public IActionResult Login()
        {
            return CheckCookieValue();
        }
        public IActionResult Register()
        {
            return CheckCookieValue();
        }

        private IActionResult CheckCookieValue()
        {
            if (this.Request.Cookies[Key] != null)
            {
                if (this.Request.Cookies[Key].Contains("Yes"))
                {
                    viewModel.IsAccepted = true;
                }

                else
                {
                    viewModel.IsAccepted = false;
                }
                return this.View(viewModel);
            }

            this.CreateCookie();
            viewModel.IsAccepted = false;

            return View(viewModel);
        }

        public IActionResult Accept()
        {
            this.DeleteCookie(Key, Value);

            ChangeCookieValue();

            return RedirectToAction("Index");
        }

        private void ChangeCookieValue()
        {
            var cookieDisclamer1 = new CookieOptions();
            Value = "Yes";
            this.Response.Cookies.Append(Key, Value, cookieDisclamer1);
        }

        public void CreateCookie()
        {
            var key = "CookieDisclamer";
            var value = "No";

            var cookieDisclamer = new CookieOptions();
            this.Response.Cookies.Append(key, value, cookieDisclamer);
        }

        public void DeleteCookie(string key, string value)
        {

            var cookieDisclamer = new CookieOptions();
            cookieDisclamer.Expires = DateTime.Now.AddDays(-1);
            this.Response.Cookies.Append(key, value, cookieDisclamer);
        }
    }
}
