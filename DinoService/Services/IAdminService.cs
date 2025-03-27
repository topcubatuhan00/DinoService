using DinoService.Models;

namespace DinoService.Services
{
    public interface IAdminService
    {
        Task<Footer> GetFooter();
        Task<bool> UpdateFooter(Footer footer);

        Task<List<Slider>> GetSlider();
        Task<bool> UpdateSlider(Slider slider, bool isDeletd);

        Task<List<Service>> GetService();
        Task<bool> UpdatService(Service service, bool isDeleted);
    }
}
