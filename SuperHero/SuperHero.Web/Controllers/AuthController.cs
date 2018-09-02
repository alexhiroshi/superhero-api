using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperHero.Application;
using SuperHero.Application.ViewModels;
using SuperHero.Domain.Notifications;

namespace SuperHero.Web.Controllers
{
    /// <summary>
    /// Auth controller. Authenticate a user
    /// </summary>
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        private readonly IUserAppService _appService;

        /// <summary>
        /// Initializes a new instance of the AuthController class.
        /// </summary>
        /// <param name="notifications">Notifications.</param>
        /// <param name="appService">App service.</param>
        public AuthController(INotificationHandler<DomainNotification> notifications,
                              IUserAppService appService)
            : base(notifications)
        {
            _appService = appService;
        }

        /// <summary>
        /// Authenticate user with username and password
        /// </summary>
        /// <returns>The token</returns>
        /// <param name="auth">Auth.</param>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AuthViewModel auth)
        {
            try
            {
                var token = await _appService.Login(auth.Username, auth.Password);
                return Ok(new { token });
            }
            catch (Exception)
            {
                // TODO: log error
                return BadRequest(new
                {
                    success = false,
                    errors = new string[] { "Could not login. Please, try again later" }
                });
            }
        }
    }
}