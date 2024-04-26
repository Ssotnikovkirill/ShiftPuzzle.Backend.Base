using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
[Controller]
[Route("/api/account/")]
public class AccountController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
     RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;

        var role1 = new IdentityRole { Name = "Manager"};
        var role2 = new IdentityRole { Name = "Programmer"};
        var role3 = new IdentityRole { Name = "Designer"};

        _roleManager.CreateAsync(role1);
        _roleManager.CreateAsync(role2);
        _roleManager.CreateAsync(role3);
    }


    [HttpPost("register")]
    public IActionResult Create([FromBody] User account)
    {
        var user = new IdentityUser { UserName = account.Name, Email = account.Email};
        var role = new IdentityRole { Name = account.Role};
        var result = _userManager.CreateAsync(user, account.Password).Result;

        if (result.Succeeded)
        {
            _userManager.AddToRoleAsync(user, role.Name);
            _signInManager.SignInAsync(user, false).Wait();
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }
    
    [HttpPost("login")]
    public IActionResult Login([FromBody] User account)
    {
        var result = _signInManager.PasswordSignInAsync(account.Name, account.Password,false, false).Result;
        if (result.Succeeded)
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
        _signInManager.SignOutAsync().Wait();
        return Ok();
    }

    [HttpGet("account")]
    public List<User> GetAccounts()
    {
        return _userManager.Users.Select(u => new User { Name = u.UserName, Email = u.Email }).ToList();
    }
    [HttpPost("editrole")]
    public async Task<IActionResult> EditRole(int userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user != null)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
            
            await _userManager.RemoveFromRolesAsync(user, userRoles);
            await _userManager.AddToRoleAsync(user, role);

            return Ok();

        }
        return BadRequest();
    }
}