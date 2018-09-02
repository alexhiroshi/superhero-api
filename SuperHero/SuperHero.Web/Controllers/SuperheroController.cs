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
    /// Superhero controller. Add and modify a Superhero
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SuperheroController : BaseController
    {
        private readonly ISuperheroAppService _appService;

        /// <summary>
        /// Initializes a new instance of the SuperherController class.
        /// </summary>
        /// <param name="notifications">Notifications.</param>
        /// <param name="appService">App service.</param>
        public SuperheroController(INotificationHandler<DomainNotification> notifications,
                                   ISuperheroAppService appService)
                                    : base(notifications)
        {
            _appService = appService;
        }

        /// <summary>
        /// Get all superheroes with pagination
        /// </summary>
        /// <returns>List of all superheroes</returns>
        /// <param name="page">Page</param>
        /// <param name="size">Page size</param>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SuperheroViewModel>>> Get(int page = 0, int size = 20)
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
                    errors = new string[] { "Could not get the superheroes" }
                });
            }
        }

        /// <summary>
        /// Get the specified superhero
        /// </summary>
        /// <returns>The superhero</returns>
        /// <param name="id">Superhero identifier</param>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var superhero = await _appService.GetAsync(id);
                return new JsonResult(superhero);
            }
            catch (Exception)
            {
                // TODO: log error
                return BadRequest(new
                {
                    success = false,
                    errors = new string[] { "Could not get the superhero" }
                });
            }
        }

        /// <summary>
        /// Create a superhero
        /// </summary>
        /// <param name="superhero">Entity representation of a superhero</param>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SuperheroViewModel superhero)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    NotifyModelStateErrors();
                    return Response(superhero);
                }

                await _appService.Add(superhero); ;
                return Response(superhero);
            }
            catch (Exception)
            {
                // TODO: log error
                return BadRequest(new
                {
                    success = false,
                    errors = new string[] { "Superhero not inserted" }
                });
            }
        }

        /// <summary>
        /// Update the specified superhero
        /// </summary>
        /// <param name="superhero">Entity representation of a superhero</param>
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] SuperheroViewModel superhero)
        {
            try
            {
                await _appService.Update(superhero);
                return Response(superhero);
            }
            catch (Exception ex)
            {
                // TODO: log error
                return BadRequest(new
                {
                    success = false,
                    errors = new string[] { "Superhero not updated" }
                });
            }
        }

        /// <summary>
        /// Delete the specified superhero
        /// </summary>
        /// <param name="id">Superhero identifier</param>
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
                    errors = new string[] { "Superhero not deleted" }
                });
            }
        }
    }
}
