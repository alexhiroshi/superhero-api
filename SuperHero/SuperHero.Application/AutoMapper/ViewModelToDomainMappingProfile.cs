using System.Collections.Generic;
using AutoMapper;
using SuperHero.Application.ViewModels;
using SuperHero.Domain.Commands.AuditEvent;
using SuperHero.Domain.Commands.ProtectionArea;
using SuperHero.Domain.Commands.Superhero;
using SuperHero.Domain.Commands.Superpower;
using SuperHero.Domain.Commands.User;
using SuperHero.Domain.Entities;

namespace SuperHero.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<SuperheroViewModel, Superhero>();
            CreateMap<SuperheroViewModel, RegisterSuperheroCommand>()
                .ConvertUsing(x => new RegisterSuperheroCommand(x.Name, x.Alias, x.ProtectionAreaId, x.Username));
            CreateMap<SuperheroViewModel, UpdateSuperheroCommand>()
                .ConvertUsing(x => new UpdateSuperheroCommand(x.Id, x.Name, x.Alias, x.ProtectionAreaId, x.Username));

            CreateMap<SuperpowerViewModel, Superpower>();
            CreateMap<SuperpowerViewModel, RegisterSuperpowerCommand>()
                .ConvertUsing(x => new RegisterSuperpowerCommand(x.Name, x.Description));
            CreateMap<SuperpowerViewModel, UpdateSuperpowerCommand>()
                .ConvertUsing(x => new UpdateSuperpowerCommand(x.Id, x.Name, x.Description));

            CreateMap<ProtectionAreaViewModel, ProtectionArea>();
            CreateMap<ProtectionAreaViewModel, RegisterProtectionAreaCommand>()
                .ConvertUsing(x => new RegisterProtectionAreaCommand(x.Name, x.Lat, x.Long, x.Radius));
            CreateMap<ProtectionAreaViewModel, UpdateProtectionAreaCommand>()
                .ConvertUsing(x => new UpdateProtectionAreaCommand(x.Id, x.Name, x.Lat, x.Long, x.Radius));

            CreateMap<AuditEventViewModel, AuditEvent>();
            CreateMap<AuditEventViewModel, RegisterAuditEventCommand>()
                .ConvertUsing(x => new RegisterAuditEventCommand(x.Entity, x.EntityId, x.Username, x.Action));

            CreateMap<RoleViewModel, Role>();
            CreateMap<UserRoleViewModel, UserRole>();

            CreateMap<UserViewModel, User>();

            CreateMap<UserViewModel, RegisterUserCommand>()
                .ConvertUsing(new UserTypeConverter());

            CreateMap<UserViewModel, UpdateUserCommand>()
                .ConvertUsing(x => new UpdateUserCommand(x.Id, x.Password, Mapper.Map<ICollection<UserRole>>(x.UserRoles)));

        }
    }

    public class UserTypeConverter : ITypeConverter<UserViewModel, RegisterUserCommand>
    {
        public RegisterUserCommand Convert(UserViewModel source, RegisterUserCommand destination, ResolutionContext context)
        {
            var userRole = context.Mapper.Map<ICollection<UserRoleViewModel>, ICollection<UserRole>>(source.UserRoles);
            return new RegisterUserCommand(source.Username, source.Password, userRole);
        }
    }
}