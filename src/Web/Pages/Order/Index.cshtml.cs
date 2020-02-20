using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.UseTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Extensions;
using Microsoft.Extensions.Logging;

namespace Microsoft.eShopWeb.Web.Pages.Order
{
    public class IndexOrderModel : PageModel
    {
        private readonly IMediator _mediator;
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<IndexOrderModel> _logger;
        
        public IndexOrderModel(IMediator mediator, IOrderRepository orderRepository, ILogger<IndexOrderModel> logger)
        {
            _mediator = mediator;
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public OrderPageViewModel OrderViewModel;

        public async Task OnGet(int orderId)
        {

            var order = await _orderRepository.GetByIdAsync(orderId);
            OrderViewModel = new OrderPageViewModel(){
                Id = orderId,
                Status = order.OrderStatus
            };
            OrderViewModel.StatusList = Enum<OrderStatus>.GetAll().Select(order => new SelectListItem { Value = order.ToString(), Text = order.ToString() });
        }

        public async Task<IActionResult> OnPost(int orderId, string status, string comment){
            var order = await _orderRepository.GetByIdAsync(orderId);
            var r = "";
            //return RedirectToAction("AdminDetail", "Order", orderId);
            //return RedirectToPage($"/Order/AdminDetails/{orderId}");
            return RedirectToAction("AdminDetail", "Order");
        }

    }
}
