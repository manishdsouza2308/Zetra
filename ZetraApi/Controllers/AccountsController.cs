using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Zetra.Controllers
{
    [ApiController]
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> _usermanger;
        private SignInManager<IdentityUser> _signinmanager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _usermanger = userManager;
            _signinmanager = signInManager;
        }
        // GET api/values
        [HttpPost]
        [Route("api/accounts")]
        public async Task<ActionResult> Register()
        {
            Stream request =  Request.Body;
            StreamReader streamReader = new StreamReader(request);
            var json = await streamReader.ReadToEndAsync();
            return Ok(json);
            

            //var newUser = new IdentityUser { Email = email, PasswordHash = password, UserName = username };
            //var result = await _usermanger.CreateAsync(newUser, password);

            //if (result.Succeeded)
            //{
            //    await _signinmanager.SignInAsync(newUser, true);
            //    return Ok("Signed In");
            //}
        }
       
        [Route("api/test")]
        public ActionResult Test()
        {
            return Ok("It worked");
        }
    }
}