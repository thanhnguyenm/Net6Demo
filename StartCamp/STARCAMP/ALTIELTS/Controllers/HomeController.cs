using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ALTIELTS.Models;
using ALTIELTS.Repositories;
using ALTIELTS.Extensions;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using ALTIELTS.Entities;

namespace ALTIELTS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger,
                                IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            if(HttpContext.User != null)
            {
                RedirectToAction("Rating");
            }


            return View();
        }

        public IActionResult Login()
        {
            if (HttpContext.User != null)
            {
                RedirectToAction("Rating");
            }

            try
            {
                var services = _unitOfWork.Service.Get().ToList();
                ViewBag.Services = services;

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            List<string> errors = new List<string>();

            if (ModelState.IsValid)
            {
                //validate user
                if (!_unitOfWork.User.Get(x => x.Passcode == model.Passcode).Any())
                {
                    ModelState.AddModelError("UserInvalid", "Passcode is invalid");
                }

                if (!_unitOfWork.Service.Get(x => x.Id == model.Service).Any())
                {
                    ModelState.AddModelError("ServiceInvalid", "Service is invalid");
                }
            }

            if (!ModelState.IsValid)
            {
                string errorMsg = ModelState.GetError();
                _logger.LogError(errorMsg);

                var services = _unitOfWork.Service.Get().ToList();
                ViewBag.Services = services;
                ViewBag.Errors = errorMsg;

                return View();
            }

            //store cookkies to authen web via cookies
            var user = _unitOfWork.User.Get(x => x.Passcode == model.Passcode).FirstOrDefault();
            var claims = new List<Claim>
                            {
                                new Claim("UserId", user.Id.ToString()),
                                new Claim("Service", model.Service.ToString()),
                            };
            
            //create cookies to authenticate
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
            };
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties).Wait();

            //
            HttpContext.Session.Set("UserId", Encoding.ASCII.GetBytes(user.Id.ToString()));
            HttpContext.Session.Set("Service", Encoding.ASCII.GetBytes(model.Service.ToString()));

            return RedirectToAction("Rating");
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();

            return RedirectToAction("Login");
        }

        [Authorize]
        public IActionResult Rating()
        {
            //from session
            byte[] uid;
            byte[] service;
            if (!HttpContext.Session.TryGetValue("UserId", out uid) ||
                !HttpContext.Session.TryGetValue("Service", out service))
            {
                return RedirectToAction("Login");
            }

            try
            {
                var serviceid = Encoding.ASCII.GetString(service);
                var question = _unitOfWork.SurveyQuestion.Get(x => x.ServiceId.ToString() == serviceid).FirstOrDefault();
                return View(question);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Rating(RatingViewModel model)
        {
            List<string> errors = new List<string>();

            if (ModelState.IsValid)
            {
                //validate question
                if (!_unitOfWork.SurveyQuestion.Get(x => x.Id == model.QuestionId).Any())
                {
                    ModelState.AddModelError("Question", "Question is invalid");
                }

            }

            if (!ModelState.IsValid)
            {
                string errorMsg = ModelState.GetError();
                _logger.LogError(errorMsg);

                ViewBag.Errors = errorMsg;
            }
            else
            {
                try
                {
                    byte[] uid;
                    byte[] service;
                    if (!HttpContext.Session.TryGetValue("UserId", out uid) ||
                        !HttpContext.Session.TryGetValue("Service", out service))
                    {
                        return RedirectToAction("Login");
                    }

                    RatingResult rs = new RatingResult
                    {
                        Comment = model.Comment,
                        Rating = model.Rating,
                        QuestionId = model.QuestionId,
                        CommentDate = DateTime.Now,
                        UserId = int.Parse(Encoding.ASCII.GetString(uid))
                    };

                    _unitOfWork.RatingResult.Insert(rs);
                    _unitOfWork.Commit();

                    return RedirectToAction("RatingSuccess");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    ViewBag.Errors = ex.Message;
                }
            }
            
            return View(new Entities.SurveyQuestion { Id = model.QuestionId, Question = model.Question });
        }

        [Authorize]
        public IActionResult RatingSuccess()
        {
            
            return View();
        }

        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
