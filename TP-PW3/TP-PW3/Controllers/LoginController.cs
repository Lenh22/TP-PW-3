using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using System.Reflection.Metadata;
using TP_PW3.Data;
using TP_PW3.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TP_PW3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IUserServices userServices;

        public LoginController(IUserServices userServices)
        {
            this.userServices = userServices;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] User user)
        {

            TokenClass tokenClass = new TokenClass();

            User userObj = await this.userServices.GetByUserName(user.Username);
            if (userObj == null)
            {
                tokenClass.TokenOrMessage = "Unauthorized User";
                return Ok(tokenClass);
            }
            bool credentials = userObj.Password.Equals(user.Password);
            if (!credentials)
            {
                tokenClass.TokenOrMessage = "Invalid Password";
                return Ok(tokenClass);
            }
            tokenClass = TokenManager.GenerateToken(user.Username);
            tokenClass.TrueSession = true;
            return Ok(tokenClass);

        }
    }
}
