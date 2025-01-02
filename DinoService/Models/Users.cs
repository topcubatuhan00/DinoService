using Microsoft.AspNetCore.Identity;

namespace DinoService.Models;

public class Users : IdentityUser
{
    public string FullName { get; set; }
}
