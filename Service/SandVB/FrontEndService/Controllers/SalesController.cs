using FrontEndService.Manager.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelSharingService.DTO;

namespace FrontEndService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {

        private readonly ISalesServiceManager _salesManager;
        public SalesController(ISalesServiceManager salesManager)
        {
            _salesManager = salesManager;
        }

        [HttpPost]
        [Route("Sale")]
        public async Task<SaleDTO> SaveUpdateSale(SaleDTO saleDTO)
        {
            var user = await _salesManager.SaveUpdateSale(saleDTO);
            return user;
        }
    }
}
