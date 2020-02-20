﻿using System;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.UseTypes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.eShopWeb.ApplicationCore.Constants;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.eShopWeb.Web.Extensions;
using Microsoft.Extensions.Logging;

namespace Microsoft.eShopWeb.Web.Pages.Order
{
    public class IndexOrderModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMediator _mediator;
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<IndexOrderModel> _logger;
        private readonly IEmailSender _emailSender;
        
        public IndexOrderModel(UserManager<ApplicationUser> userManager, IMediator mediator, IOrderRepository orderRepository, ILogger<IndexOrderModel> logger, IEmailSender emailSender)
        {
            _userManager = userManager;
            _mediator = mediator;
            _orderRepository = orderRepository;
            _logger = logger;
            _emailSender = emailSender;
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

        [Authorize(Roles=AuthorizationConstants.Roles.ADMINISTRATORS)]
        public async Task<IActionResult> OnPost(int orderId, OrderStatus status, string comment){
            var changedStatus = false;
            var addedNewComment = false;

            var order = await _orderRepository.GetByIdAsync(orderId);
            
            if(!order.OrderStatus.Equals(status)){
                order.OrderStatus = status;
                changedStatus = true;
            }

            if(!string.IsNullOrEmpty(comment)){
                var date = DateTime.Now;
                var userName = _userManager.GetUserName(User);
                comment = $"{date.ToString("g")} by {userName}\n{comment}";
                if(string.IsNullOrEmpty(order.Comments)){
                    order.Comments = comment;
                }
                else{
                    order.Comments = $"{order.Comments}\n\n{comment}";
                }
                addedNewComment = true;
            }
            
            try{
                await _orderRepository.UpdateAsync(order);
                if(changedStatus || addedNewComment){
                    await sendEmailToClientAsync(order.Id, changedStatus?status.ToString():null, comment);
                }
            }
            return RedirectToPage("/Order");
        }

        private async Task sendEmailToClientAsync(int orderNumber, string status, string comment){
            var subject = $"Order {orderNumber} - Your order has been changed";
            var body = "Hi!<br>";
            if(!string.IsNullOrEmpty(status)){ body = $"{body}<br>New status: {status}";}
            if(!string.IsNullOrEmpty(comment)){ body = $"{body}<br>{comment}";}
            body = $"{body}<br><br>Good day for shopping<br>eShopOnWeb time";

            var userName = _userManager.GetUserName(User);
            if(userName.Contains("@")){
                await _emailSender.SendEmailAsync(userName, subject, body);
            }
        }

    }
}
