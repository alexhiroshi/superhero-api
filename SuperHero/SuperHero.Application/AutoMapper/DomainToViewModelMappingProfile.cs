using AutoMapper;
using SuperHero.Application.ViewModels;
using SuperHero.Domain.Entities;

namespace SuperHero.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Superhero, SuperheroViewModel>();
            CreateMap<Superpower, SuperpowerViewModel>();
            CreateMap<ProtectionArea, ProtectionAreaViewModel>();
            CreateMap<AuditEvent, AuditEventViewModel>();
            CreateMap<User, UserViewModel>();
            CreateMap<Role, RoleViewModel>();
            CreateMap<UserRole, UserRoleViewModel>();
        }
    }
}
