﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SIMSWebApp.Controllers
{
    [Authorize] // bat phai dang nhap
    public class DashboardController : Controller
    {
        [Authorize(Roles = "Admin,Student,Faculty")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
