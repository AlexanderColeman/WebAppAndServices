using ModelSharingService.DTO;
using SalesService.Data;
using SalesService.Model;

namespace SalesService.Manager
{
    public class SalesManager
    {
        private SalesDbContext _salesDbContext;
        public SalesManager(SalesDbContext salesDbContext)
        {
            _salesDbContext = salesDbContext;
        }

        public async Task<SaleDTO> createSale(SaleDTO saleDTO)
        {
            var newSale = new Sale()
            {
                Title = saleDTO.Title,
            };

            await _salesDbContext.AddAsync(newSale);
            await _salesDbContext.SaveChangesAsync();

            return saleDTO;
        }
    }
}
