using DinoService.Data;
using DinoService.Models;
using DinoService.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace DinoService.Services;

public class ServiceInformationService : IServiceInformationService
{
    private readonly AppDbContext dc;

    public ServiceInformationService(AppDbContext dc)
    {
        this.dc = dc;
    }

    public async Task<ServiceInformation> GetById(int id)
    {
        var res = await dc.ServiceInformations.FirstOrDefaultAsync(p => p.Id == id);
        return res;
    }

    public async Task<HomeDataModel> GetHomeData()
    {
        var deliveredCount = await dc.ServiceInformations
                             .Where(si => si.Status == "Teslim Edildi")
                             .CountAsync();
        var receivedCount = await dc.ServiceInformations
                             .Where(si => si.Status == "Teslim Alındı")
                             .CountAsync();
        var guaranteed =  await dc.ServiceInformations
                             .Where(si => si.Status == "Garantiye Gönderildi")
                             .CountAsync();
        var process =  await dc.ServiceInformations
                             .Where(si => si.Status == "İşleme Alındı")
                             .CountAsync();
        var cgiden = await dc.ServiceInformations
                             .Where(si => si.Status == "Dinosoft Kargoya Verdi" && si.KargoAlici == "Çetin Pos Bilişim")
                             .CountAsync();
        var cgelen = await dc.ServiceInformations
                             .Where(si => si.Status == "Karşı Taraf Kargoya Verdi" && si.KargoAlici == "Çetin Pos Bilişim")
                             .CountAsync();

        var total = await dc.ServiceInformations
                             .CountAsync();

        return new HomeDataModel { CetinGelen = cgelen, CetinGiden = cgiden, Delivered = deliveredCount, Received = receivedCount, GuaranteedItems = guaranteed, InProcess = process, Total = total };
    }

    public async Task<SaveProductData> GetProductData()
    {
        var oPro = await dc.OtherProducts.ToListAsync();
        var sSta = await dc.ServiceStatuses.ToListAsync();
        return new SaveProductData
        {
            OtherProducts = oPro,
            ServiceStatus = sSta
        };
    }

    public async Task<List<ServiceInformation>> GetServiceInformation(GetServiceInformationViewModel? model)
    {
        var res = await dc.ServiceInformations.ToListAsync();
        if (model != null)
            res.Where(p => p.ServiceNumber == model.ServiceNumber || p.CustomerPhoneNumber == model.ServiceNumber);
        return res;
    }

    public List<ServiceStatus> GetStatus()
    {
        var res = dc.ServiceStatuses.ToList();  
        return res;
    }

    public async Task<bool> ChangeStatus(ChangeStatusModel model)
    {
        var e = await dc.ServiceInformations.Where(p => p.Id == model.Id).FirstOrDefaultAsync();
        e.Status = model.Status;
        e.TeslimAlan = model.TeslimAlan;
        dc.ServiceInformations.Update(e);
        var res = await dc.SaveChangesAsync();
        return res > 0;
    }

    public async Task<bool> SaveServiceProduct(CreatePoductModel model)
    {
        var res = await dc.ServiceInformations.AddAsync(new ServiceInformation
        {
            CustomerName = model.CustomerName,
            CustomerLastName = model.CustomerLastName,
            CustomerPhoneNumber = model.CustomerPhoneNumber,
            CustomerCompanyName = model.CustomerCompanyName,
            ServiceNumber = model.ServiceNumber,
            Status = model.Status,
            ServiceRegistirationDate = model.ServiceRegistirationDate != null ? model.ServiceRegistirationDate : DateTime.Now,
            ProductName = model.ProductName,
            ProductAmount = model.ProductAmount,
            OtherItems = model.OtherItems,
            ProductProblem = model.ProductProblem,
            Price = model.Price,
        });

        var res2 = await dc.SaveChangesAsync();
        return res2 > 0;
    }
}
