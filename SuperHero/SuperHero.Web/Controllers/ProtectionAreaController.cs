using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SuperHero.Application;
using SuperHero.Application.ViewModels;
using SuperHero.Domain.Notifications;

namespace SuperHero.Web.Controllers
{
    /// <summary>
    /// Protection Area controller. Add and modify a protection area
    /// </summary>
    [Route("api/[controller]")]
    public class ProtectionAreaController : BaseController
    {
        private readonly IProtectionAreaAppService _appService;

        /// <summary>
        /// Initializes a new instance of the ProtectionAreaController class.
        /// </summary>
        /// <param name="notifications">Notifications.</param>
        /// <param name="appService">App service.</param>
        public ProtectionAreaController(INotificationHandler<DomainNotification> notifications,
                                        IProtectionAreaAppService appService)
                                    : base(notifications)
        {
            _appService = appService;
        }

        /// <summary>
        /// Get all protection area with pagination
        /// </summary>
        /// <returns>List of all Protection area</returns>
        /// <param name="page">Page</param>
        /// <param name="size">Page size</param>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProtectionAreaViewModel>>> Get(int page = 0, int size = 20)
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
                    errors = new string[] { "Could not get the Protection areas" }
                });
            }
        }

        /// <summary>
        /// Get the specified protection area
        /// </summary>
        /// <returns>The Protection area</returns>
        /// <param name="id">Protection area identifier</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var protectionArea = await _appService.GetAsync(id);
                return new JsonResult(protectionArea);
            }
            catch (Exception)
            {
                // TODO: log error
                return BadRequest(new
                {
                    success = false,
                    errors = new string[] { "Could not get the protection area" }
                });
            }
        }

        /// <summary>
        /// Create a protection area
        /// </summary>
        /// <param name="protectionArea">Entity representation of a Protection area</param>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProtectionAreaViewModel protectionArea)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    NotifyModelStateErrors();
                    return Response(protectionArea);
                }

                await _appService.Add(protectionArea); ;
                return Response(protectionArea);
            }
            catch (Exception)
            {
                // TODO: log error
                return BadRequest(new
                {
                    success = false,
                    errors = new string[] { "Protection area not inserted" }
                });
            }
        }

        /// <summary>
        /// Update the specified protection area
        /// </summary>
        /// <param name="protectionArea">Entity representation of a protection area</param>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProtectionAreaViewModel protectionArea)
        {
            try
            {
                await _appService.Update(protectionArea);
                return Response(protectionArea);
            }
            catch (Exception)
            {
                // TODO: log error
                return BadRequest(new
                {
                    success = false,
                    errors = new string[] { "Protection area not updated" }
                });
            }
        }

        /// <summary>
        /// Delete the specified protection area
        /// </summary>
        /// <param name="id">Protection area identifier</param>
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
                    errors = new string[] { "Protection area not deleted" }
                });
            }
        }
    }
}
