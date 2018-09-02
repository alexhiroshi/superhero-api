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
    /// Superpower controller. Add and modify a Superpower
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SuperpowerController : BaseController
    {
        private readonly ISuperpowerAppService _appService;

        /// <summary>
        /// Initializes a new instance of the SuperherController class.
        /// </summary>
        /// <param name="notifications">Notifications.</param>
        /// <param name="appService">App service.</param>
        public SuperpowerController(INotificationHandler<DomainNotification> notifications,
                                    ISuperpowerAppService appService)
                                    : base(notifications)
        {
            _appService = appService;
        }

        /// <summary>
        /// Get all superpowers with pagination
        /// </summary>
        /// <returns>List of all superpowers</returns>
        /// <param name="page">Page</param>
        /// <param name="size">Page size</param>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SuperpowerViewModel>>> Get(int page = 0, int size = 20)
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
                    errors = new string[] { "Could not get the superpowers" }
                });
            }
        }

        /// <summary>
        /// Get the specified superpower
        /// </summary>
        /// <returns>The superpower</returns>
        /// <param name="id">Superpower identifier</param>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var superpower = await _appService.GetAsync(id);
                return new JsonResult(superpower);
            }
            catch (Exception)
            {
                // TODO: log error
                return BadRequest(new
                {
                    success = false,
                    errors = new string[] { "Could not get the superpower" }
                });
            }
        }

        /// <summary>
        /// Create a superpower
        /// </summary>
        /// <param name="superpower">Entity representation of a superpower</param>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SuperpowerViewModel superpower)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    NotifyModelStateErrors();
                    return Response(superpower);
                }

                await _appService.Add(superpower); ;
                return Response(superpower);
            }
            catch (Exception)
            {
                // TODO: log error
                return BadRequest(new
                {
                    success = false,
                    errors = new string[] { "Superpower not inserted" }
                });
            }
        }

        /// <summary>
        /// Update the specified superpower
        /// </summary>
        /// <param name="superpower">Entity representation of a superpower</param>
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] SuperpowerViewModel superpower)
        {
            try
            {
                await _appService.Update(superpower);
                return Response(superpower);
            }
            catch (Exception)
            {
                // TODO: log error
                return BadRequest(new
                {
                    success = false,
                    errors = new string[] { "Superpower not updated" }
                });
            }
        }

        /// <summary>
        /// Delete the specified superpower
        /// </summary>
        /// <param name="id">Superpower identifier</param>
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
                    errors = new string[] { "Superpower not deleted" }
                });
            }
        }
    }
}
