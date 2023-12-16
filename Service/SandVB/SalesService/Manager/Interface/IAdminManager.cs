using ModelSharingService.IntegrationEvents;

namespace SalesService.Manager.Interface
{
    public interface IAdminManager
    {
        Task ProcessUserEventAsync(UserEvent userEvent);
        Task createSalesUser(UserEvent userEvent);
    }
}
