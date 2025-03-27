using DinoService.Data;
using DinoService.Models;
using Microsoft.EntityFrameworkCore;

namespace DinoService.Services
{
    public class AdminService : IAdminService
    {
        private readonly AppDbContext dc;

        public AdminService(AppDbContext dc)
        {
            this.dc = dc;
        }

        public async Task<Footer> GetFooter()
        {
            var res = await dc.Footers.FirstOrDefaultAsync();
            return res;
        }

        public async Task<List<Service>> GetService()
        {
            var res = await dc.Services.ToListAsync();
            return res;
        }

        public async Task<List<Slider>> GetSlider()
        {
            var res = await dc.Sliders.ToListAsync();
            return res;
        }

        public async Task<bool> UpdateFooter(Footer footer)
        {
            var a = await dc.Footers.Where(p => p.Id == footer.Id).FirstOrDefaultAsync();
            a.Title = footer.Title;
            a.Address = footer.Address;
            a.Phone = footer.Phone;
            a.Mail = footer.Mail;
            a.BottomText = footer.BottomText;
            dc.Footers.Update(a);
            var res = await dc.SaveChangesAsync();
            return res > 0;
        }

        public async Task<bool> UpdateSlider(Slider slider, bool isDeletd)
        {
            if (slider.Id > 0)
            {
                if (isDeletd)
                {
                    dc.Sliders.Remove(slider);
                    var res2 = await dc.SaveChangesAsync();
                    return res2 > 0;
                }
                var a = await dc.Sliders.Where(p => p.Id == slider.Id).FirstOrDefaultAsync();
                a.Title = slider.Title;
                a.ButtonLeftHref = slider.ButtonLeftHref;
                a.ButtonRightHref = slider.ButtonRightHref;
                a.ButtonLeftText = slider.ButtonLeftText;
                a.ButtonRightText = slider.ButtonRightText;
                a.Content = slider.Content;
                dc.Sliders.Update(a);
                var res = await dc.SaveChangesAsync();
                return res > 0;
            }
            else
            {
                dc.Sliders.Add(slider);
                var res = await dc.SaveChangesAsync();
                return res > 0;
            }
        }

        public async Task<bool> UpdatService(Service service, bool isDeletd)
        {
            if (service.Id > 0)
            {
                if (isDeletd)
                {
                    dc.Services.Remove(service);
                    var res2 = await dc.SaveChangesAsync();
                    return res2 > 0;
                }
                var a = await dc.Services.Where(p => p.Id == service.Id).FirstOrDefaultAsync();
                a.Title = service.Title;
                a.Content = service.Content;
                a.Icon = service.Icon;
                dc.Services.Update(a);
                var res = await dc.SaveChangesAsync();
                return res > 0;
            }
            else
            {
                dc.Services.Add(service);
                var res = await dc.SaveChangesAsync();
                return res > 0;
            }

            
        }
    }
}
