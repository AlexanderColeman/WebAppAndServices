using ModelSharingService.IntegrationEvents;
using System.Collections.Concurrent;
using System;

namespace AdminService.Messaging.Interface
{
    public interface IEventDispatcher
    {
        public void Initialize();
        public void AddUserEvent(UserEvent userEvent);
        public void DispatchIntegrationEvents();
    }
}
