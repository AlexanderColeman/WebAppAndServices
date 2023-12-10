using ModelSharingService.DTO;

namespace FrontEndService.Manager.Interface
{
    public interface ISalesManager
    {
        Task<SaleDTO> SaveUpdateSale(SaleDTO saleDTO);
    }
}
