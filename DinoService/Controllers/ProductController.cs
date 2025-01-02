using DinoService.Services;
using DinoService.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DinoService.Controllers;

public class ProductController : Controller
{

    private readonly IServiceInformationService _serviceInformationService;

    public ProductController(IServiceInformationService serviceInformationService)
    {
        _serviceInformationService = serviceInformationService;
    }

    public IActionResult Services()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> GetServiceInformation([FromBody] GetServiceInformationViewModel model)
    {
        if (ModelState.IsValid)
        {
            var res = await _serviceInformationService.GetServiceInformation(model);
            return Json(new { Data = res, Success = true });
        }
        else
        {
            return Json(new { Success = false });
        }
    }



    public IActionResult SaveProduct()
    {
        return View();
    }

    public IActionResult ListProducts()
    {
        return View();
    }

    public IActionResult Summary()
    {
        return View();
    }

    public async Task<IActionResult> Detail(int id)
    {
        var res = await _serviceInformationService.GetById(id);
        return View(res);
    }


    [HttpPost]
    public async Task<IActionResult> SaveProduct(CreatePoductModel model)
    {
        var res = await _serviceInformationService.SaveServiceProduct(model);
        return Json(new { success = res });
    }

    [HttpPost]
    public async Task<IActionResult> ChangeStatus(ChangeStatusModel model)
    {
        var res = await _serviceInformationService.ChangeStatus(model);
        return Json(new { success = res });
    }

    [HttpPost]
    public async Task<ActionResult> GetProducts()
    {
        var draw = Request.Form["draw"].FirstOrDefault();
        var length = Convert.ToInt32(Request.Form["length"].FirstOrDefault());
        var start = Convert.ToInt32(Request.Form["start"].FirstOrDefault());
        string searchValue = Request.Form["search[value]"];

        var aa = await _serviceInformationService.GetServiceInformation(null);

        int recordsTotal = aa.Count();

        if (!string.IsNullOrEmpty(searchValue))
        {
            aa = aa.Where(
                k => k.CustomerName.ToLower().Contains(searchValue.ToLower()) || 
                k.CustomerLastName.ToLower().Contains(searchValue.ToLower()) || 
                k.CustomerCompanyName.ToLower().Contains(searchValue.ToLower()) || 
                k.CustomerPhoneNumber.ToLower().Contains(searchValue.ToLower()) || 
                k.ProductName.ToLower().Contains(searchValue.ToLower())
                ).ToList();
        }

        int totalrowsAfterCount = aa.Count;
        aa = aa.Skip(start).Take(length).ToList();
        return Json(new
        {
            data = aa,
            draw = draw,
            recordsTotal = recordsTotal,
            recordsFiltered = totalrowsAfterCount
        });
    }
}
