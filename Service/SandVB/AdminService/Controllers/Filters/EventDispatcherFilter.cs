using AdminService.Messaging.Interface;
using AdminService.Messaging;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AdminService.Controllers.Filters
{
    public class EventDispatcherFilter : IActionFilter
    {
        IEventDispatcher _eventDispatcher;
        public EventDispatcherFilter(IEventDispatcher eventDispatcher)
        {
            _eventDispatcher = eventDispatcher;
        }

        void IActionFilter.OnActionExecuted(ActionExecutedContext context)
        {
            _eventDispatcher.DispatchIntegrationEvents();

            if (EventDispatcher.Exceptions.Any())
            {
                // TODO:
                // ?? Not sure if we should throw an exception here (would happen if messages could not be published to RabbitMQ) or just log them

                throw new AggregateException(EventDispatcher.Exceptions.ToArray());
            }
            // This is done internally by the dispatcher
            //_eventDispatcher.ClearDispatchedEvents();
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext context)
        {
            _eventDispatcher.Initialize();
        }
    }
}
