using DinoService.Entities.Entities.Concrete;

namespace DinoService.Business.Abstract;

public interface IUserService
{
    Task<AppUser> GetUserById(string userId);
}
