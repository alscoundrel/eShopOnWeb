using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using static ApplicationCore.Interfaces.ICurrencyService;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Microsoft.eShopWeb.Web.ViewModels
{
    public class CatalogItemViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Item Name")]
        [Required(ErrorMessage="Please enter name"),MaxLength(30)]
        public string Name { get; set; }

        public string PictureUri { get; set; }

        [Display(Name = "Price")]
        [Range(0.0, 10000.00)]
        public decimal Price { get; set; }

        [Display(Name = "Show Price")]
        public bool ShowPrice { get; set; } = true;
        public Currency PriceUnit { get; set; }
        public string PriceSymbol { get; set; }

        [Display(Name = "Image")]
        [Required(ErrorMessage="Please enter image")]
        public IFormFile FormImage { get; set; }

        public int CatalogTypeId { get; set; }
        public int CatalogBrandId { get; set; }

        public IEnumerable<SelectListItem> Brands { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }
    }
}
