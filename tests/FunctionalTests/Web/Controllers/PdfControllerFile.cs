using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.eShopWeb.Web.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.eShopWeb.Web.Pages.Pdf;
using Microsoft.eShopWeb.Web.ViewModels;
using System.Threading;
using Scriban.Runtime;
using System;
using System.Collections.Generic;
using Scriban;

namespace Microsoft.eShopWeb.FunctionalTests.Web.Controllers
{
    [Collection("Sequential")]
    public class PdfControllerFile : IClassFixture<WebTestFixture>
    {
        private readonly Mock<ICatalogViewModelService> _mockCatalogViewModelService;

        public PdfControllerFile(WebTestFixture factory)
        {
            Client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
            _mockCatalogViewModelService = new Mock<ICatalogViewModelService>();
        }

        public HttpClient Client { get; }

        [Fact]
        public async Task ReturnsPdfPageFromUrl()
        {
            var catalogIndexViewModel= new CatalogIndexViewModel();
            catalogIndexViewModel.CatalogItems = null;
            var indexPdfModel = new IndexPdfModel(_mockCatalogViewModelService.Object, null);

            CancellationToken cancellationToken = default(CancellationToken);
            _mockCatalogViewModelService.Setup(
                x => x.GetCatalogItems(It.IsAny<CatalogPageFiltersViewModel>(), It.IsAny<bool>(), It.IsAny<bool>(), cancellationToken)).ReturnsAsync(catalogIndexViewModel);
            // Arrange & Act
            var response = await Client.GetAsync("/pdf");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            
            // Assert
            Assert.Contains("Catalog Online", stringResponse);
        }

        [Fact]
        public void ReturnsPdfPageFromScriban()
        {
            var template        = Template.Parse(TemplateContent());


            var catalogModel = new CatalogIndexViewModel();
            
            var items = new List<CatalogItemViewModel>();
            items.Add(new CatalogItemViewModel(){ Name ="Name_1", Price = 1.2m, PriceSymbol = "€"});
            items.Add(new CatalogItemViewModel(){ Name ="Name_2", Price = 2.2m, PriceSymbol = "€"});
            catalogModel.CatalogItems = items;

            var listCatalogItems = new List<CatalogItemViewModel>();
            
            foreach (var item in catalogModel.CatalogItems)
            {
                listCatalogItems.Add(
                    new CatalogItemViewModel(){
                        Name = item.Name,
                        Price = item.Price,
                        ShowPrice = item.ShowPrice,
                        PriceSymbol = item.PriceSymbol,
                        PictureUri = System.IO.Path.GetFullPath($"~{item.PictureUri}").Replace("~", "wwwroot")
                        //PictureUri = "C:\\Formacao\\ASP.Net\\desenvolvimento\\eShopOnWeb\\src\\Web\\wwwroot\\images\\products\\1.png"
                    }
                );
            }

            MemberRenamerDelegate memberRenamer = member => member.Name;
            var pp = new { Data = listCatalogItems, Date = DateTime.Now.ToString(), Year = DateTime.Now.Year };
            var html = template.Render(pp, memberRenamer);
            
            // Assert
            Assert.Contains("Name_1", html);
        }

        private string TemplateContent(){
            return "<div class=\"esh-catalog-items row\">{{- for CatalogItem in Data }}<div class=\"esh-catalog-item col-md-4\"><div class=\"row\"><div><img class=\"esh-catalog-thumbnail\" src=\"{{ CatalogItem.PictureUri }}\" style=\"width: 220px;\"></div><div><div class=\"esh-catalog-name\"><span>{{ CatalogItem.Name }}</span></div><div class=\"esh-catalog-price\">{{ if CatalogItem.ShowPrice }}<span class=\"product-price\">{{ CatalogItem.Price }}</span><span class=\"product-price-unit\">{{ CatalogItem.PriceSymbol }}</span>{{ else }}<span>\"Sob-Consulta\"</span>{{ end }}</div></div></div></div>{{ end -}}</div>";
        }
    }
}
