using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP23.P02.Web.Data;
using SP23.P02.Web.Features.DTOs;
using SP23.P02.Web.Features.Users;

namespace SP23.P02.Web.Controllers;

[Route("api/authentication")]
[ApiController]

public class AuthenticationController : ControllerBase

{
    private SignInManager<User> _signInManager;
    private UserManager<User> _userManager;
    private readonly DataContext dataContext;

    public AuthenticationController(UserManager<User> userManager, SignInManager<User> signInManager, DataContext dataContext)
    {
        this.dataContext = dataContext;
        _signInManager = signInManager;
        _userManager = userManager;
    }
   
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto users)
    {
        var userFound = await _userManager.FindByNameAsync(users.UserName);
        var isPassWordValid = await _userManager.ChangePasswordAsync(userFound, users.Password);

        if (isPassWordValid && userFound != null)
        {
            await _signInManager.SignInAsync(userFound,false);
                return Ok(dataContext.Users.Select(x => new { x.UserName, }));
        }
    }
    
    }
    