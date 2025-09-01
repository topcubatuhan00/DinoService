using DinoService.Models;
using DinoService.ViewModels;

namespace DinoService.Services;

public interface IServiceInformationService
{
    Task<List<ServiceInformation>> GetServiceInformation(GetServiceInformationViewModel? model);
    Task<bool> SaveServiceProduct(CreatePoductModel model);
    Task<SaveProductData> GetProductData();
    Task<HomeDataModel> GetHomeData();
    Task<ServiceInformation> GetById(int id);
    List<ServiceStatus> GetStatus();
    Task<bool> ChangeStatus(ChangeStatusModel model);
    Task<bool> DeleteProduct(int id);
}
