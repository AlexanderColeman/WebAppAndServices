using ModelSharingService.IntegrationEvents;

namespace SalesService.Messaging.Interface
{
    public interface IMessagePublisher
    {
        public void PublishAircraftEvent(UserEvent userEvent);
    }
}
