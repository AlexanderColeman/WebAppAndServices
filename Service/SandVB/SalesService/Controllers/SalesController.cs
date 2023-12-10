using Microsoft.AspNetCore.Mvc;
using ModelSharingService.DTO;
using SalesService.Manager;

namespace SalesService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesController : Controller
    {
        private readonly SalesManager _salesManager;
        public SalesController(SalesManager salesManager)
        {
            _salesManager = salesManager;
        }

        [HttpPost]
        [Route("Sale")]
        public async Task<SaleDTO> createSale(SaleDTO userDTO)
        {
            var user = await _salesManager.createSale(userDTO);

            var newSaleDTO = new SaleDTO()
            {
                Title = user.Title,
            };

            return newSaleDTO;
        }
    }
}
