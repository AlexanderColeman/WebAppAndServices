using ModelSharingService.DTO;

namespace FrontEndService.Manager.Interface
{
    public interface ISalesServiceManager
    {
        Task<SaleDTO> SaveUpdateSale(SaleDTO saleDTO);
    }
}
