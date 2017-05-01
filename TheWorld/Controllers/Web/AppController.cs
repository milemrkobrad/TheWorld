using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;
        private IWorldRepository _repository;
        private ILogger<AppController> _logger;

        public AppController(IMailService mailService, IConfigurationRoot config, IWorldRepository repository
            ,ILogger<AppController> logger)
        {
            _mailService = mailService;
            _config = config;
            _repository = repository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {                
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get Index page: {ex.Message}");
                return Redirect("/error");
            }
        }

        [Authorize]
        public IActionResult Trips()
        {
            try
            {
                //var data = _repository.GetAllTrips();
                //return View(data);
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get trips in Index page: {ex.Message}");
                return Redirect("/error");
            }
        }


        public IActionResult Contact()
        {
            //throw new InvalidOperationException("Bad things happen to good developers");

            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (model.Email.Contains("aol.com")) {
                ModelState.AddModelError("Email", "We don't support AOL addresses");
            }

            if (ModelState.IsValid)
            {
                _mailService.SendMail(_config["MailSettings:ToAddress"], model.Email, "From TheWorld", model.Message);
            }

            ModelState.Clear();

            ViewBag.UserMessage = "Message Sent";

            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
