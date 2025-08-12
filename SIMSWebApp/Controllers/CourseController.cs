using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SIMSWebApp.Controllers
{
    [Authorize]
    public class CourseController : Controller
    {
        [HttpGet]
        [Authorize(Roles = "Admin,Student,Faculty")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
    }
}
