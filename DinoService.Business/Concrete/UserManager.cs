using DinoService.Business.Abstract;
using DinoService.DataAccess.Concrete;
using DinoService.Entities.Entities.Concrete;

namespace DinoService.Business.Concrete;

public class UserManager : IUserService
{
    private readonly EfRepositoryBase<AppUser> _userRepository;

    public UserManager(EfRepositoryBase<AppUser> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<AppUser> GetUserById(string userId)
    {
        return await _userRepository.GetAsync(u => u.Id == userId);
    }
}