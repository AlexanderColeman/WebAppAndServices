using ModelSharingService.IntegrationEvents;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace AdminService.Messaging.Interface
{
    public interface IMessagePublisher
    {
        public void PublishUserEvent(UserEvent userEvent);
    }
}
