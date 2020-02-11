using Microsoft.AspNetCore.Identity;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.WishListAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Infrastructure.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        public CatalogNotifications(IAsyncRepository<CatalogItem> itemRepository, IAsyncRepository<WishList> wishListRepository, IEmailSender emailSender, SignInManager<ApplicationUser> signInManager, IServiceProvider serviceProvider){
            _itemRepository = itemRepository;
            _wishListRepository = wishListRepository;
            _emailSender = emailSender;
            _signInManager = signInManager;
            _serviceProvider = serviceProvider;

            var configuration = _serviceProvider.GetRequiredService<IConfiguration>();
            TemplateSubject = configuration.GetValue<string>("SendGrid:templateSubject");
            TemplateBody = configuration.GetValue<string>("SendGrid:TemplateBody");
        }

        private string TemplateSubject;
        private string TemplateBody;

        /// <summary>
        /// Catalog Item Notify
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="priceChenged"></param>
        /// <returns></returns>
        public async Task CatalogItemsNotifyAsync(int itemId, decimal priceNew){
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
                // greeting
                int hour = DateTimeOffset.Now.Hour;
                var greeting = "Good morning";
                if(19 < hour){ greeting = "Good evening";}
                else if(12 < hour){ greeting = "Good afternoon";}

                MemberRenamerDelegate memberRenamer = member => member.Name;
                
                //email subject
                var templateContentSubject = await File.ReadAllTextAsync(TemplateSubject);
                // Parse a scriban template
                var templateSubject = Template.Parse(templateContentSubject);
                var subject = templateSubject.Render(new { CatalogItem = catalogItem }, memberRenamer);

                //email message
                var templateContentBody = await File.ReadAllTextAsync(TemplateBody);
                // Parse a scriban template
                var templateBody = Template.Parse(templateContentBody);
                var message = templateBody.Render(
                        new { Greeting =  greeting, PriceChanged = priceNew != catalogItem.Price}
                    );
                
                await _emailSender.SendEmailAsync(email, subject, message);
            }

        }



    }
}
