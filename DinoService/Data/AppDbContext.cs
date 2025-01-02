using DinoService.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DinoService.Data;

public class AppDbContext : IdentityDbContext<Users>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ServiceInformation> ServiceInformations { get; set; }
    public DbSet<ServiceStatus> ServiceStatuses { get; set; }
    public DbSet<OtherProducts> OtherProducts { get; set; }
    public DbSet<Slider> Sliders { get; set; }
    public DbSet<Footer> Footers { get; set; }
    public DbSet<Service> Services { get; set; }
}
