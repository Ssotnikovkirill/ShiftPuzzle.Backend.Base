using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;   


public class AccountController : ControllerBase
{
    //private readonly UserManager<IdentityUser> _userManager;
    //private readonly SignInManager<IdentityUser> _signInManager;

    private readonly IAccountManager _accountManager;
    public AccountController(IAccountManager accountManager)
    {
        _accountManager = accountManager;
    }

    [HttpPost("api/account/verify")]
    public bool Verify([FromBody] User account)
    {
        return _accountManager.VerifyAccount(account);
    }

    [HttpPost("/api/account/register")]  
    public void Register([FromBody] User account)
    {
        _accountManager.RegisterAccount(account);
    }

    [HttpGet("/api/account/get/{name}")] 
    public User GetUser(string name)
    {
        return _accountManager.GetAccount(name);
    }

    [HttpGet("/api/account/getall")]
    public List<User> GetAll()
    {
        return _accountManager.GetAccounts();
    }
}