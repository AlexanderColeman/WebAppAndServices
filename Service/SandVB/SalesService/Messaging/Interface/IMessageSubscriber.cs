using ModelSharingService.IntegrationEvents;

namespace SalesService.Messaging.Interface
{
    public interface IMessageSubscriber
    {
        public void ProcessUserEvent(IServiceProvider serviceProvider, UserEvent userEvent);
    }
}
