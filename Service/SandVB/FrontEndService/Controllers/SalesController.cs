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

        private readonly ISalesManager _salesManager;
        public SalesController(ISalesManager salesManager)
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
