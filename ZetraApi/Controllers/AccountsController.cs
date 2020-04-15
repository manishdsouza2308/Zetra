using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ZetraApi;

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
            try
            { 
                Stream request =  Request.Body;
                StreamReader streamReader = new StreamReader(request);
                var json = await streamReader.ReadToEndAsync();
           

                var deserialize = JsonConvert.DeserializeObject<IdentityUser>(json);
                //var identity = new IdentityUser()
                //{
                //    Email = deserialize.email,
                //    UserName = deserialize.username,
                //    PasswordHash = deserialize.password
                //};
                await _usermanger.CreateAsync(deserialize, deserialize.PasswordHash);
                await _signinmanager.SignInAsync(deserialize, true);
                if(_signinmanager.IsSignedIn(User))
                {
                    return Ok("signed in");
                }
                return Ok("sign in failed");
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
                      
        }

        [Route("api/login")]
        public async Task<ActionResult> Login()
        {
            Stream request = Request.Body;
            StreamReader streamReader = new StreamReader(request);
            var json = await streamReader.ReadToEndAsync();
            var deserialize = JsonConvert.DeserializeObject<IdentityUser>(json);

            var login = await _signinmanager.PasswordSignInAsync(deserialize.UserName, deserialize.PasswordHash,true,false);

            if(login.Succeeded)
            return Ok("logged in");

            return Ok("login failed");
        }
        
        [Route("api/test")]
        public ActionResult test()
        {
            return Ok("working");
        }
       
    }
}