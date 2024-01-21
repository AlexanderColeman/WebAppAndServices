using FrontEndService.Manager.Interface;
using FrontEndService.ViewModel.EndpointMap;
using ModelSharingService.DTO;

namespace FrontEndService.Manager
{
    public class SalesServiceManager : ISalesServiceManager
    {
        private IHttpManager _httpManager;
        private SalesMap _salesMap;
        public SalesServiceManager(IHttpManager httpManager, SalesMap salesMap)
        {
            _httpManager = httpManager;
            _salesMap = salesMap;
        }

        public async Task<SaleDTO> SaveUpdateSale(SaleDTO saleDTO)
        {
            var sale = await _httpManager.MakePostAsync<SaleDTO>(_salesMap.BaseUrl, _salesMap.saveUpdateSale(), saleDTO);
            return sale;
        }
    }
}
