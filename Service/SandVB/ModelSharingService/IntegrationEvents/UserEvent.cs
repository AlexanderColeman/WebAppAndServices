using ModelSharingService.DTO;
using ModelSharingService.Enum;

namespace ModelSharingService.IntegrationEvents
{
    public class UserEvent : IntegrationEventBase
    {
        public UserEventTypeEnum UserEventType { get; set; }
        public UserDTO UserDTO { get; set; }
    }
}
