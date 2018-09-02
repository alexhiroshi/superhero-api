using System.Threading.Tasks;
using SuperHero.Domain.Commands;
using SuperHero.Domain.Events;

namespace SuperHero.Domain.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}