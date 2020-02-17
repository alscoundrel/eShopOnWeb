using Microsoft.AspNetCore.Mvc;
using IronPdf;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.eShopWeb.Web.Interfaces;
using Microsoft.eShopWeb.Web.ViewModels;
using Microsoft.Extensions.Configuration;
using Scriban;
using Scriban.Runtime;
using System.Collections.Generic;
using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.Web.Controllers.Pdf
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("reports/[controller]/[action]")]
    public class PdfController : Controller
    {
        private readonly IViewRenderService _viewRenderService;
        private readonly ICatalogViewModelService _catalogViewModelService;
        private readonly IConfiguration _configuration;

        public PdfController(IViewRenderService viewRenderService, ICatalogViewModelService catalogViewModelService, IConfiguration configuration){
            _viewRenderService = viewRenderService;
            _catalogViewModelService = catalogViewModelService;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<FileResult> Catalog(){
            var query = this.Request.QueryString.ToString();
            string root = $"{Request.Scheme}://{Request.Host}/pdf{query}";
            var uri = new Uri(root);//"https://localhost:5001/reports/pdf/catalog"
            var urlToPdf = new HtmlToPdf();
            var pdf = await urlToPdf.RenderUrlAsPdfAsync(uri);
            //pdf.SaveAs(Path.Combine(Directory.GetCurrentDirectory(), "UrlToPdfExample1.Pdf"));
            return File(pdf.BinaryData, "application/pdf;", "Catalog.pdf");
        }

        [HttpGet]
        public async Task<FileResult> RenderAsync(){
            var catalogPageModel = new CatalogPageFiltersViewModel(){
                Culture = CultureInfo.CurrentCulture.Name,
                ItemsPerPage = 0
            };
            var catalogModel = await _catalogViewModelService.GetCatalogItems(catalogPageModel, false, true);
            var cc = catalogModel.CatalogItems.FirstOrDefault();

            var html = await _viewRenderService.RenderToStringAsync("Pdf/Catalog", cc);
            var htmlToPdf = new HtmlToPdf();
            var pdf = await htmlToPdf.RenderHtmlAsPdfAsync(html);
            return File(pdf.BinaryData, "application/pdf;", "Catalog.pdf");
        }

        [HttpGet]
        public async Task<FileResult> ScribanAsync(){
            var pathTemplate    = _configuration.GetValue<string>("Template:CatalogPdf");
            var templateContent = await System.IO.File.ReadAllTextAsync(pathTemplate); 
            var template        = Template.Parse(templateContent);

            var catalogPageModel = new CatalogPageFiltersViewModel(){
                Culture = CultureInfo.CurrentCulture.Name,
                ItemsPerPage = 0
            };
            var catalogModel = await _catalogViewModelService.GetCatalogItems(catalogPageModel, false, true);
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

            var htmlToPdf = new HtmlToPdf();
            var pdf = await htmlToPdf.RenderHtmlAsPdfAsync(html);
            return File(pdf.BinaryData, "application/pdf;", "Catalog.pdf");
        }
    }
}