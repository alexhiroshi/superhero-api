using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SuperHero.Application.ViewModels;
using SuperHero.Domain.Bus;
using SuperHero.Domain.Commands.AuditEvent;
using SuperHero.Domain.Repositories;

namespace SuperHero.Application.Services
{
    public class AuditEventAppService : IAuditEventAppService
    {
        private readonly IAuditEventRepository _auditEventRepository;
        private readonly IMediatorHandler _bus;
        private readonly IMapper _mapper;

        public AuditEventAppService(IAuditEventRepository auditEventRepository,
                                    IMediatorHandler bus,
                                    IMapper mapper)
        {
            _auditEventRepository = auditEventRepository;
            _bus = bus;
            _mapper = mapper;
        }

        public async Task Add(AuditEventViewModel auditEvent)
        {
            var registerCommand = _mapper.Map<RegisterAuditEventCommand>(auditEvent);
            await _bus.SendCommand(registerCommand);
        }

        public async Task<IEnumerable<AuditEventViewModel>> GetAllAsync(int skip = 0, int take = 20)
        {
            var list = await _auditEventRepository.GetAllAsync(skip, take);
            return _mapper.Map<IEnumerable<AuditEventViewModel>>(list);
        }
    }
}
