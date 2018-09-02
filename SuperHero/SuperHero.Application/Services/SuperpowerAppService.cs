using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SuperHero.Application.ViewModels;
using SuperHero.Domain.Bus;
using SuperHero.Domain.Cache;
using SuperHero.Domain.Commands.Superpower;
using SuperHero.Domain.Entities;
using SuperHero.Domain.Repositories;

namespace SuperHero.Application.Services
{
    public class SuperpowerAppService : ISuperpowerAppService
    {
        private readonly ISuperpowerRepository _superpowerRepository;
        private readonly IMediatorHandler _bus;
        private readonly IMapper _mapper;
        private readonly ICacheManager _cache;

        public SuperpowerAppService(ISuperpowerRepository superpowerRepository,
                                   IMediatorHandler bus,
                                   IMapper mapper,
                                   ICacheManager cache)
        {
            _superpowerRepository = superpowerRepository;
            _bus = bus;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task Add(SuperpowerViewModel superpower)
        {
            var registerCommand = _mapper.Map<RegisterSuperpowerCommand>(superpower);
            await _bus.SendCommand(registerCommand);
        }

        public async Task<SuperpowerViewModel> GetAsync(Guid id)
        {
            var result = await _cache.GetAsync<Superpower>($"superpowerGetAsync_{id}", async () => await _superpowerRepository.GetAsync(id));

            return _mapper.Map<SuperpowerViewModel>(result);
        }

        public async Task<IEnumerable<SuperpowerViewModel>> GetAllAsync(int skip = 0, int take = 20)
        {
            var list = await _superpowerRepository.GetAllAsync(skip, take);
            return _mapper.Map<IEnumerable<SuperpowerViewModel>>(list);
        }

        public async Task Remove(Guid id)
        {
            var removeCommand = new RemoveSuperpowerCommand(id);
            await _bus.SendCommand(removeCommand);
        }

        public async Task Update(SuperpowerViewModel superpower)
        {
            var updateCommand = _mapper.Map<UpdateSuperpowerCommand>(superpower);
            await _bus.SendCommand(updateCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
