using AdminService.Messaging.Interface;
using ModelSharingService.IntegrationEvents;
using System.Collections.Concurrent;

namespace AdminService.Messaging
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IMessagePublisher _publisher;

        public static ConcurrentQueue<Exception> Exceptions { get; private set; }

        private static ConcurrentQueue<UserEvent> UserEvents { get; set; }

        public EventDispatcher(IMessagePublisher publisher)
        {
            _publisher = publisher;
        }

        public void Initialize()
        {
            if (UserEvents == null)
                UserEvents = new ConcurrentQueue<UserEvent>();
        }

        public void AddUserEvent(UserEvent userEvent)
        {
            UserEvents.Enqueue(userEvent);
        }

        public void DispatchIntegrationEvents()
        {
            Exceptions = new ConcurrentQueue<Exception>();
            UserEvent userEvent = null;

            while (UserEvents.Count() > 0)
            {
                // this thread could lose in a concurrency race, if so then just try again
                if (UserEvents.TryDequeue(out userEvent))
                {
                    if (userEvent != null)
                    {
                        try
                        {
                            // call rabbitMQ publisher
                            _publisher.PublishUserEvent(userEvent);
                            // set the flag as a formality, although it has already been removed from the queue
                            userEvent.IsDispatched = true;
                        }
                        catch (Exception ex)
                        {
                            EventDispatcher.Exceptions.Enqueue(ex);
                            // put the event back in the list
                            UserEvents.Enqueue(userEvent);
                        }
                    }
                }
                userEvent = null;
            }
        }
    }
}
