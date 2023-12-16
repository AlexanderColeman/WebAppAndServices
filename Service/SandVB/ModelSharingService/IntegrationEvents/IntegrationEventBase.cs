using MediatR;
using ModelSharingService.DTO;

namespace ModelSharingService.IntegrationEvents
{
    public class IntegrationEventBase : INotification
    {
        public Guid UserId { get; set; }
        public UserDTO User { get; set; }
        public DateTimeOffset DateOccured { get; set; }
        public bool IsDispatched { get; set; }
    }
}
