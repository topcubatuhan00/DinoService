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
        // Gerekli verileri bir kere çek ve gruplandır.
        var serviceData = await dc.ServiceInformations
            .GroupBy(si => new { si.Status, si.KargoAlici })
            .Select(group => new
            {
                group.Key.Status,
                group.Key.KargoAlici,
                Count = group.Count()
            })
            .ToListAsync();

        // Verileri gruplardan çek
        var deliveredCount = serviceData.FirstOrDefault(x => x.Status == "Teslim Edildi")?.Count ?? 0;
        var receivedCount = serviceData.FirstOrDefault(x => x.Status == "Teslim Alındı")?.Count ?? 0;
        var guaranteed = serviceData.FirstOrDefault(x => x.Status == "Garantiye Gönderildi")?.Count ?? 0;
        var process = serviceData.FirstOrDefault(x => x.Status == "İşleme Alındı")?.Count ?? 0;
        var cgiden = serviceData.FirstOrDefault(x => x.Status == "Garantiye Gönderildi" && x.KargoAlici == "Çetin Pos Bilişim")?.Count ?? 0;
        var cgelen = serviceData.FirstOrDefault(x => x.Status == "Garantiden Geliyor" && x.KargoAlici == "Çetin Pos Bilişim")?.Count ?? 0;

        // Tüm kayıt sayısını al
        var total = await dc.ServiceInformations.CountAsync();

        return new HomeDataModel
        {
            CetinGelen = cgelen,
            CetinGiden = cgiden,
            Delivered = deliveredCount,
            Received = receivedCount,
            GuaranteedItems = guaranteed,
            InProcess = process,
            Total = total
        };
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
        
        if (model != null)
        {
            var res2 = await dc.ServiceInformations.Where(p => p.ServiceNumber == model.ServiceNumber || p.CustomerPhoneNumber == model.ServiceNumber).ToListAsync();
            return res2;
        }
        var res = await dc.ServiceInformations.ToListAsync();
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
        if (model.TeslimAlan != null && model.TeslimAlan.Length > 0)
        {
            e.ServiceLeaveDate = DateTime.Now;
        }
        e.KargoAlici = model.KargoAlici;
        e.LastStatusChangeDate = DateTime.Now;
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
