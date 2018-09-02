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
    /// User controller. Add and modify a user
    /// </summary>
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IUserAppService _appService;

        /// <summary>
        /// Initializes a new instance of the UserController class.
        /// </summary>
        /// <param name="notifications">Notifications.</param>
        /// <param name="appService">App service.</param>
        public UserController(INotificationHandler<DomainNotification> notifications,
                              IUserAppService appService)
            : base(notifications)
        {
            _appService = appService;
        }

        /// <summary>
        /// List all users with pagination
        /// </summary>
        /// <returns>List of all users</returns>
        /// <param name="page">Page</param>
        /// <param name="size">Page size</param>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> Get(int page = 0, int size = 20)
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
                    errors = new string[] { "Could not get the users" }
                });
            }
        }

        /// <summary>
        /// Get the specified user
        /// </summary>
        /// <returns>The user</returns>
        /// <param name="id">user identifier</param>
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var user = await _appService.GetAsync(id);
                return new JsonResult(user);
            }
            catch (Exception)
            {
                // TODO: log error
                return BadRequest(new
                {
                    success = false,
                    errors = new string[] { "Could not get the user" }
                });
            }
        }

        /// <summary>
        /// Create a user
        /// </summary>
        /// <param name="user">Entity representation of a user</param>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserViewModel user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    NotifyModelStateErrors();
                    return Response(user);
                }

                await _appService.Add(user);
                return Response(user);
            }
            catch (Exception ex)
            {
                // TODO: log error
                return BadRequest(new
                {
                    success = false,
                    errors = new string[] { "user not inserted" }
                });
            }
        }

        /// <summary>
        /// Update the specified user
        /// </summary>
        /// <param name="user">Entity representation of a user</param>
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserViewModel user)
        {
            try
            {
                await _appService.Update(user);
                return Response(user);
            }
            catch (Exception)
            {
                // TODO: log error
                return BadRequest(new
                {
                    success = false,
                    errors = new string[] { "user not updated" }
                });
            }
        }

        /// <summary>
        /// Delete the specified user
        /// </summary>
        /// <param name="id">user identifier</param>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _appService.Remove(id);
                return Response();
            }
            catch (Exception)
            {
                // TODO: log error
                return BadRequest(new
                {
                    success = false,
                    errors = new string[] { "user not deleted" }
                });
            }
        }
    }
}
