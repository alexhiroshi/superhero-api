using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SuperHero.Application.ViewModels;
using SuperHero.Domain.Bus;
using SuperHero.Domain.Cache;
using SuperHero.Domain.Repositories;

namespace SuperHero.Application.Services
{
	public class RoleAppService : IRoleAppService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMediatorHandler _bus;
        private readonly IMapper _mapper;
        private readonly ICacheManager _cache;

        public RoleAppService(IRoleRepository roleRepository,
                              IMediatorHandler bus,
                              IMapper mapper,
                              ICacheManager cache)
        {
            _roleRepository = roleRepository;
            _bus = bus;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<IEnumerable<RoleViewModel>> GetAllAsync(int skip = 0, int take = 20)
        {
            var list = await _roleRepository.GetAllAsync(skip, take);
            return _mapper.Map<IEnumerable<RoleViewModel>>(list);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
