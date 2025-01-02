using DinoService.Models;
using DinoService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DinoService.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _service;

        public AdminController(IAdminService service)
        {
            _service = service;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult EditHeader()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> EditFooter()
        {
            var res = await _service.GetFooter();
            return View(res);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFooter(Footer footer)
        {
            var res = await _service.UpdateFooter(footer);
            return RedirectToAction("EditFooter", "Admin");
        }

       
        [Authorize]
        public IActionResult EditSideBar()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> EditSlider()
        {
            var res = await _service.GetSlider();
            return View(res);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSlider(Slider slider, bool isDeleted)
        {
            var res = await _service.UpdateSlider(slider, isDeleted);
            return Json(new {success = res });
        }

        [Authorize]
        public IActionResult EditServices()
        {
            var res = _service.GetService();
            return View(res);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateServices(Service service)
        {
            var res = await _service.UpdatService(service);
            return RedirectToAction("EditServices", "Admin");
        }
    }
}
