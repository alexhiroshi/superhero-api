using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using SuperHero.Application.ViewModels;
using SuperHero.Domain.Bus;
using SuperHero.Domain.Cache;
using SuperHero.Domain.Commands.Superhero;
using SuperHero.Domain.Entities;
using SuperHero.Domain.Repositories;

namespace SuperHero.Application.Services
{
    public class SuperheroAppService : ISuperheroAppService
    {
        private readonly ISuperheroRepository _superheroRepository;
        private readonly IMediatorHandler _bus;
        private readonly IMapper _mapper;
        private readonly ICacheManager _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SuperheroAppService(ISuperheroRepository superheroRepository,
                                   IMediatorHandler bus,
                                   IMapper mapper,
                                   ICacheManager cache,
                                   IHttpContextAccessor httpContextAccessor)
        {
            _superheroRepository = superheroRepository;
            _bus = bus;
            _mapper = mapper;
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Add(SuperheroViewModel superhero)
        {
            var username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            superhero.Username = username;
            var registerCommand = _mapper.Map<RegisterSuperheroCommand>(superhero);
            await _bus.SendCommand(registerCommand);
        }

        public async Task<SuperheroViewModel> GetAsync(Guid id)
        {
            var result = await _cache.GetAsync<Superhero>($"superheroGetAsync_{id}", async () => await _superheroRepository.GetAsync(id));

            return _mapper.Map<SuperheroViewModel>(result);
        }

        public async Task<IEnumerable<SuperheroViewModel>> GetAllAsync(int skip = 0, int take = 20)
        {
            var list = await _superheroRepository.GetAllAsync(skip, take);
            return _mapper.Map<IEnumerable<SuperheroViewModel>>(list);
        }

        public async Task Remove(Guid id)
        {
            var username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            var removeCommand = new RemoveSuperheroCommand(id, username);
            await _bus.SendCommand(removeCommand);
        }

        public async Task Update(SuperheroViewModel superhero)
        {
            var username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value;
            superhero.Username = username;
            var updateCommand = _mapper.Map<UpdateSuperheroCommand>(superhero);
            await _bus.SendCommand(updateCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
