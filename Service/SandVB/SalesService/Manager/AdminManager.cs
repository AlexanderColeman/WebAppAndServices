using Microsoft.AspNetCore.Mvc;
using ModelSharingService.DTO;
using SalesService.Data;
using SalesService.Model;
using Microsoft.AspNetCore.Mvc;
using ModelSharingService.IntegrationEvents;
using SalesService.Manager.Interface;

namespace SalesService.Manager
{
    public class AdminManager : IAdminManager
    {
        private SalesDbContext _salesDbContext;
        public AdminManager(SalesDbContext salesDbContext)
        {
            _salesDbContext = salesDbContext;
        }

        public async Task ProcessUserEventAsync(UserEvent userEvent)
        {
            if (userEvent != null)
            {
                await createSalesUser(userEvent);
            }
        }

        public async Task createSalesUser(UserEvent userEvent)
        {
            var newSalesUser = new SalesUser()
            {
                Name = userEvent.UserDTO.Name,
            };

            if (newSalesUser != null)
            {
                await _salesDbContext.AddAsync(newSalesUser);
                await _salesDbContext.SaveChangesAsync();
            }
        }
    }
}
