using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using SuperHero.Application.ViewModels;
using SuperHero.Domain.Auth;
using SuperHero.Domain.Bus;
using SuperHero.Domain.Cache;
using SuperHero.Domain.Commands.User;
using SuperHero.Domain.Entities;
using SuperHero.Domain.Repositories;

namespace SuperHero.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenHandler _jwtTokenHandler;
        private readonly IHashString _hashString;
        private readonly IMediatorHandler _bus;
        private readonly IMapper _mapper;
        private readonly ICacheManager _cache;

        public UserAppService(IUserRepository userRepository,
                              IJwtTokenHandler jwtTokenHandler,
                              IHashString hashString,
                              IMediatorHandler bus,
                              IMapper mapper,
                              ICacheManager cache)
        {
            _userRepository = userRepository;
            _jwtTokenHandler = jwtTokenHandler;
            _hashString = hashString;
            _bus = bus;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task Add(UserViewModel user)
        {
            var registerCommand = _mapper.Map<RegisterUserCommand>(user);
            await _bus.SendCommand(registerCommand);
        }

        public async Task<IEnumerable<UserViewModel>> GetAllAsync(int skip = 0, int take = 20)
        {
            var list = await _userRepository.GetAllAsync(skip, take);
            return _mapper.Map<IEnumerable<UserViewModel>>(list);
        }

        public async Task<UserViewModel> GetAsync(Guid id)
        {
            var result = await _cache.GetAsync<User>($"userGetAsync_{id}", async () => await _userRepository.GetAsync(id));

            return _mapper.Map<UserViewModel>(result);
        }

        public async Task Remove(Guid id)
        {
            var removeCommand = new RemoveUserCommand(id);
            await _bus.SendCommand(removeCommand);
        }

        public async Task Update(UserViewModel user)
        {
            var updateCommand = _mapper.Map<UpdateUserCommand>(user);
            await _bus.SendCommand(updateCommand);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<string> Login(string username, string password)
        {
            var user = await _userRepository.ValidateUserAsync(username, _hashString.Generate(password));
            var token = _jwtTokenHandler.Generate(user);
            return token;
        }
    }
}
