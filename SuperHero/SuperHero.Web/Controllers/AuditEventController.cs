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
    /// Audit event controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuditEventController : BaseController
    {
        private readonly IAuditEventAppService _appService;

        /// <summary>
        /// Initializes a new instance of the AuditEventController class.
        /// </summary>
        /// <param name="notifications">Notifications.</param>
        /// <param name="appService">App service.</param>
        public AuditEventController(INotificationHandler<DomainNotification> notifications,
                                   IAuditEventAppService appService)
            : base(notifications)
        {
            _appService = appService;
        }

        /// <summary>
        /// Get all audit events with pagination
        /// </summary>
        /// <returns>List of all audit events</returns>
        /// <param name="page">Page</param>
        /// <param name="size">Page size</param>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuditEventViewModel>>> Get(int page = 0, int size = 20)
        {
            var list = await _appService.GetAllAsync(page, size);
            return new JsonResult(list);
        }

        /// <summary>
        /// Create a audit event
        /// </summary>
        /// <param name="auditEvent">Entity representation of a audit event</param>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AuditEventViewModel auditEvent)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    NotifyModelStateErrors();
                    return Response(auditEvent);
                }

                await _appService.Add(auditEvent);
                return Response(auditEvent);
            }
            catch (Exception)
            {
                // TODO: log error
                return BadRequest(new
                {
                    success = false,
                    errors = new string[] { "Audit Event not inserted" }
                });
            }
        }
    }
}
