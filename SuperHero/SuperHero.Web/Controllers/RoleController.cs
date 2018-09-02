using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperHero.Application;
using SuperHero.Application.ViewModels;
using SuperHero.Domain.Notifications;

namespace SuperHero.Web.Controllers
{
    /// <summary>
    /// Role controller
    /// </summary>
    [Route("api/[controller]")]
    public class RoleController : BaseController
    {
        private readonly IRoleAppService _appService;

        /// <summary>
        /// Initializes a new instance of the RoleController class.
        /// </summary>
        /// <param name="notifications">Notifications.</param>
        /// <param name="appService">App service.</param>
        public RoleController(INotificationHandler<DomainNotification> notifications,
                              IRoleAppService appService)
            : base(notifications)
        {
            _appService = appService;
        }

        /// <summary>
        /// List all roles with pagination
        /// </summary>
        /// <returns>List of all roles</returns>
        /// <param name="page">Page</param>
        /// <param name="size">Page size</param>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleViewModel>>> Get(int page = 0, int size = 20)
        {
            try
            {
                var list = await _appService.GetAllAsync(page, size);
                return new JsonResult(list);
            }
            catch (Exception)
            {
                // TODO: log error
                return BadRequest(new
                {
                    success = false,
                    errors = new string[] { "Could not get the roles" }
                });
            }
        }
    }
}
