using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SuperHero.Application.ViewModels;
using SuperHero.Domain.Bus;
using SuperHero.Domain.Cache;
using SuperHero.Domain.Commands.ProtectionArea;
using SuperHero.Domain.Repositories;

namespace SuperHero.Application.Services
{
    public class ProtectionAreaAppService : IProtectionAreaAppService
    {
        private readonly IProtectionAreaRepository _protectionAreaRepository;
        private readonly IMediatorHandler _bus;
        private readonly IMapper _mapper;
        private readonly ICacheManager _cache;

        public ProtectionAreaAppService(IProtectionAreaRepository protectionAreaRepository,
                                        IMediatorHandler bus,
                                        IMapper mapper,
                                        ICacheManager cache)
        {
            _protectionAreaRepository = protectionAreaRepository;
            _bus = bus;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task Add(ProtectionAreaViewModel protectionArea)
        {
            var registerCommand = _mapper.Map<RegisterProtectionAreaCommand>(protectionArea);
            await _bus.SendCommand(registerCommand);
        }

        public async Task<ProtectionAreaViewModel> GetAsync(Guid id)
        {
            var result = await _protectionAreaRepository.GetAsync(id);

            return _mapper.Map<ProtectionAreaViewModel>(result);
        }

        public async Task<IEnumerable<ProtectionAreaViewModel>> GetAllAsync(int skip = 0, int take = 20)
        {
            var list = await _protectionAreaRepository.GetAllAsync(skip, take);
            return _mapper.Map<IEnumerable<ProtectionAreaViewModel>>(list);
        }

        public async Task Remove(Guid id)
        {
            var removeCommand = new RemoveProtectionAreaCommand(id);
            await _bus.SendCommand(removeCommand);
        }

        public async Task Update(ProtectionAreaViewModel protectionArea)
        {
            var updateCommand = _mapper.Map<UpdateProtectionAreaCommand>(protectionArea);
            await _bus.SendCommand(updateCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
