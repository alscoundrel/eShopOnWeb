using ApplicationCore.UseTypes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.ApplicationCore.Constants;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Web.Features.AdminOrders;
using Microsoft.eShopWeb.Web.Features.MyOrders;
using Microsoft.eShopWeb.Web.Features.OrderDetails;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize] // Controllers that mainly require Authorization still use Controller/View; other pages use Pages
    [Route("[controller]/[action]")]
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IOrderRepository _orderRepository;

        public OrderController(IMediator mediator, IOrderRepository orderRepository)
        {
            _mediator = mediator;
            _orderRepository = orderRepository;
        }

        [HttpGet()]
        public async Task<IActionResult> MyOrders()
        {
            var viewModel = await _mediator.Send(new GetMyOrders(User.Identity.Name));

            return View(viewModel);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> Detail(int orderId)
        {
            var viewModel = await _mediator.Send(new GetOrderDetails(User.Identity.Name, orderId));

            if (viewModel == null)
            {
                return BadRequest("No such order found for this user.");
            }

            return View(viewModel);
        }

        [HttpGet()]
        [Authorize(Roles=AuthorizationConstants.Roles.ADMINISTRATORS)]
        public async Task<IActionResult> AdminOrders()
        {
            var viewModel = await _mediator.Send(new GetAdminOrders());

            return View(viewModel);
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> AdminDetail(int orderId)
        {
            var viewModel = await _mediator.Send(new GetOrderDetails(orderId));

            if (viewModel == null)
            {
                return BadRequest("No such order found for this user.");
            }

            return View(viewModel);
        }

        [HttpGet("{orderId}")]
        [Authorize(Roles=AuthorizationConstants.Roles.ADMINISTRATORS)]
        public async Task<IActionResult> CancelAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            order.OrderStatus = OrderStatus.Canceled;
            await _orderRepository.UpdateAsync(order);
            return RedirectToAction("AdminOrders", "Order");
            //return Redirect("/Order/AdminOrders");
        }
    }
}
