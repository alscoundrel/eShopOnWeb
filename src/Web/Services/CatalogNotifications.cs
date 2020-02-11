using Microsoft.AspNetCore.Identity;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.WishListAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Scriban;
using Scriban.Runtime;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Web.Services
{
    /// <summary>
    /// For Catalog Notifications
    /// </summary>
    public class CatalogNotifications
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAsyncRepository<CatalogItem> _itemRepository;
        private readonly IAsyncRepository<WishList> _wishListRepository;
        private readonly IEmailSender _emailSender;
        private IServiceProvider _serviceProvider;
        private readonly ILogger<CatalogNotifications> _logger;

        public CatalogNotifications(IAsyncRepository<CatalogItem> itemRepository, IAsyncRepository<WishList> wishListRepository, IEmailSender emailSender, SignInManager<ApplicationUser> signInManager, IServiceProvider serviceProvider, ILoggerFactory loggerFactory){
            _itemRepository = itemRepository;
            _wishListRepository = wishListRepository;
            _emailSender = emailSender;
            _signInManager = signInManager;
            _serviceProvider = serviceProvider;
            _logger = loggerFactory.CreateLogger<CatalogNotifications>();

            // var configuration = _serviceProvider.GetRequiredService<IConfiguration>();
            // TemplateSubject = configuration.GetValue<string>("SendGrid:templateSubject");
            // TemplateBody = configuration.GetValue<string>("SendGrid:TemplateBody");

            new Action(async () => {await loadTemplatesDataAsync();}).Invoke();
        }

        private Template templateBody;
        private Template templateSubject;
        private string greeting = "Good morning";

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task loadTemplatesDataAsync(){
            var configuration = _serviceProvider.GetRequiredService<IConfiguration>();
            
            // for email subject
            var pathTemplateSubject    = configuration.GetValue<string>("SendGrid:templateSubject");
            var templateContentSubject = await File.ReadAllTextAsync(pathTemplateSubject); 
            templateSubject            = Template.Parse(templateContentSubject);

            // for email message
            var pathTemplateBody    = configuration.GetValue<string>("SendGrid:TemplateBody");
            var templateContentBody = await File.ReadAllTextAsync(pathTemplateBody); 
            templateBody            = Template.Parse(templateContentBody);

            // greeting
            int hour = DateTimeOffset.Now.Hour;
            if(19 < hour){ greeting = "Good evening";}
            else if(12 < hour){ greeting = "Good afternoon";}
        }

        /// <summary>
        /// Catalog Item Notify
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="priceChenged"></param>
        /// <returns></returns>
        public async Task CatalogItemsNotifyAsync(int itemId, decimal priceNew){
            _logger.LogInformation("Inicialize catalog items notify...");

            var catalogItem = await _itemRepository.GetByIdAsync(itemId);
            var users = _signInManager.UserManager.Users.ToList();
            
            //All wishLists
            var wishesList = await _wishListRepository.ListAllAsync();
            foreach (var wl in wishesList)
            {
                var wishListSpec = new WishListWithItemsSpecification(wl.Id);
                var wishList = (await _wishListRepository.ListAsync(wishListSpec)).FirstOrDefault();

                //items of wishList
                foreach (var item in wishList.Items)
                {
                    // if the item with itemId is in this wishList
                    if(item.CatalogItemId == itemId){
                        var user = users.Where(x => x.UserName==wishList.WisherId).FirstOrDefault();
                        await CatalogItemNotifyClient(user.Email, catalogItem, priceNew);
                    }
                }
            }
        }

        /// <summary>
        /// Catalog Item notify client
        /// </summary>
        /// <param name="email"></param>
        /// <param name="catalogItem"></param>
        /// <param name="priceNew"></param>
        /// <returns></returns>
        public async Task CatalogItemNotifyClient(string email, CatalogItem catalogItem, decimal priceNew){
            var anyChanges = false;
            if(priceNew != catalogItem.Price){ anyChanges = true;}

            if(anyChanges){
                MemberRenamerDelegate memberRenamer = member => member.Name;
                
                //email subject
                var subject = templateSubject.Render(new { CatalogItem = catalogItem }, memberRenamer);

                //email message
                var message = templateBody.Render(
                        new { Greeting =  greeting, PriceChanged = priceNew != catalogItem.Price}
                    );
                
                await _emailSender.SendEmailAsync(email, subject, message);
            }

        }



    }
}
