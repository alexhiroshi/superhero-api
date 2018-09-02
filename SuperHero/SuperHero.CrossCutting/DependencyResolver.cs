using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SuperHero.Application;
using SuperHero.Application.Services;
using SuperHero.CrossCutting.Auth;
using SuperHero.CrossCutting.Bus;
using SuperHero.CrossCutting.Cache;
using SuperHero.Domain.Auth;
using SuperHero.Domain.Bus;
using SuperHero.Domain.Cache;
using SuperHero.Domain.CommandHandlers;
using SuperHero.Domain.Commands.AuditEvent;
using SuperHero.Domain.Commands.ProtectionArea;
using SuperHero.Domain.Commands.Superhero;
using SuperHero.Domain.Commands.Superpower;
using SuperHero.Domain.Commands.User;
using SuperHero.Domain.EventHandlers;
using SuperHero.Domain.Events.Superhero;
using SuperHero.Domain.Notifications;
using SuperHero.Domain.Repositories;
using SuperHero.Domain.Services;
using SuperHero.Infrastructure;
using SuperHero.Infrastructure.Repositories;
using SuperHero.Infrastructure.Services;

namespace SuperHero.CrossCutting
{
    public static class DependencyResolver
    {
        public static void Resolve(IServiceCollection services)
        {
            // Application
            services.AddScoped<ISuperheroAppService, SuperheroAppService>();
            services.AddScoped<ISuperpowerAppService, SuperpowerAppService>();
            services.AddScoped<IProtectionAreaAppService, ProtectionAreaAppService>();
            services.AddScoped<IAuditEventAppService, AuditEventAppService>();
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IRoleAppService, RoleAppService>();

            // Infrastructure
            services.AddScoped<ISuperheroRepository, SuperheroRepository>();
            services.AddScoped<ISuperpowerRepository, SuperpowerRepository>();
            services.AddScoped<IProtectionAreaRepository, ProtectionAreaRepository>();
            services.AddScoped<IAuditEventRepository, AuditEventRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();

            services.AddScoped<IAuditEventService, AuditEventService>();

            services.AddScoped<ICacheManager, CacheManager>();
            services.AddScoped<IJwtTokenHandler, JwtTokenHandler>();
            services.AddScoped<IHashString, HashString>();

            // Command Handler
            services.AddScoped<INotificationHandler<RegisterSuperheroCommand>, SuperheroCommandHandler>();
            services.AddScoped<INotificationHandler<UpdateSuperheroCommand>, SuperheroCommandHandler>();
            services.AddScoped<INotificationHandler<RemoveSuperheroCommand>, SuperheroCommandHandler>();

            services.AddScoped<INotificationHandler<RegisterSuperpowerCommand>, SuperpowerCommandHandler>();
            services.AddScoped<INotificationHandler<UpdateSuperpowerCommand>, SuperpowerCommandHandler>();
            services.AddScoped<INotificationHandler<RemoveSuperpowerCommand>, SuperpowerCommandHandler>();

            services.AddScoped<INotificationHandler<RegisterProtectionAreaCommand>, ProtectionAreaCommandHandler>();
            services.AddScoped<INotificationHandler<UpdateProtectionAreaCommand>, ProtectionAreaCommandHandler>();
            services.AddScoped<INotificationHandler<RemoveProtectionAreaCommand>, ProtectionAreaCommandHandler>();

            services.AddScoped<INotificationHandler<RegisterAuditEventCommand>, AuditEventCommandHandler>();

            services.AddScoped<INotificationHandler<RegisterUserCommand>, UserCommandHandler>();
            services.AddScoped<INotificationHandler<UpdateUserCommand>, UserCommandHandler>();
            services.AddScoped<INotificationHandler<RemoveUserCommand>, UserCommandHandler>();

            // Command Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<SuperheroUpdatedEvent>, SuperheroEventHandler>();

            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
