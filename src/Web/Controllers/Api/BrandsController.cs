using Microsoft.eShopWeb.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.eShopWeb.Web.ViewModels;
using System.Collections.Generic;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.eShopWeb.Web.Authorization;
using Microsoft.eShopWeb.ApplicationCore.Constants;

namespace Microsoft.eShopWeb.Web.Controllers.Api
{
    [Authorize]
    public class BrandsController : BaseApiController
    {
        private readonly IAsyncRepository<CatalogBrand> _brandRepository;

        public BrandsController(IAsyncRepository<CatalogBrand> brandRepository) => _brandRepository = brandRepository;

        /// <summary>
        /// return catalog brands list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(ApiAuthorizationConstants.CATALOG_BRAND_READ_SCOPE)]
        public async Task<ActionResult<IReadOnlyList<CatalogBrand>>> List()
        {
            var brands = await _brandRepository.ListAllAsync();
            return Ok(brands);
        }

        /// <summary>
        /// Get catalog brand by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(ApiAuthorizationConstants.CATALOG_BRAND_READ_SCOPE)]
        public async Task<ActionResult<CatalogBrand>> GetById(int id) {
            try  {
                var brand = await _brandRepository.GetByIdAsync(id);
                return Ok(brand);
            } catch (ModelNotFoundException) {
                return NotFound();
            }
        }

        /// <summary>
        /// Get Catalog Brand by brand name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        [Authorize(ApiAuthorizationConstants.CATALOG_BRAND_READ_SCOPE)]
        public async Task<ActionResult<CatalogBrand>> GetByBrand(string name) {
            try  {
                var brands = await _brandRepository.ListAllAsync();
                var brandFindeds = brands.Where(x => string.Compare(x.Brand, name, StringComparison.InvariantCultureIgnoreCase) == 0);
                return Ok(brandFindeds);
            } catch (ModelNotFoundException) {
                return NotFound();
            }
        }

        // Post: api/Brands/
        /// <summary>
        /// Insert new catalog brand
        /// </summary>
        /// <param name="catalogBrand"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(ApiAuthorizationConstants.CATALOG_BRAND_WRITE_SCOPE)]
        [Authorize(Roles=AuthorizationConstants.Roles.ADMINISTRATORS)]
        public async Task<ActionResult<IReadOnlyList<CatalogBrand>>> Post(CatalogBrand catalogBrand)
        {

            try  {
                var brands = await _brandRepository.ListAllAsync();
                var brandFindeds = brands
                        .Where(x => string.Compare(x.Brand, catalogBrand.Brand, StringComparison.InvariantCultureIgnoreCase) == 0);
                if(brandFindeds != null){ return Conflict();}

                await _brandRepository.AddAsync(catalogBrand);
                return Ok();
            } catch (ModelNotFoundException) {
                return NotFound();
            }
        }

        /// <summary>
        /// Update Catalog brand
        /// api/Brands/
        /// </summary>
        /// <param name="catalogBrand"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(ApiAuthorizationConstants.CATALOG_BRAND_WRITE_SCOPE)]
        [Authorize(Roles=AuthorizationConstants.Roles.ADMINISTRATORS)]
        public async Task<ActionResult<IReadOnlyList<CatalogBrand>>> Put(CatalogBrand catalogBrand)
        {
            try  {
                var brands = await _brandRepository.ListAllAsync();
                // return catalog brands list where de brand name is the same and id not iqual
                var brandFindeds = brands
                        .Where(x => string.Compare(x.Brand, catalogBrand.Brand, StringComparison.InvariantCultureIgnoreCase) == 0)
                        .Where(x => x.Id != catalogBrand.Id);
                if(brandFindeds != null){ return Conflict();}

                await _brandRepository.UpdateAsync(catalogBrand);
                return Ok();
            } catch (ModelNotFoundException) {
                return NotFound();
            }
        }

        /// <summary>
        /// Delete Catalog Brand By id
        /// api/Brands/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(ApiAuthorizationConstants.CATALOG_BRAND_WRITE_SCOPE)]
        [Authorize(Roles=AuthorizationConstants.Roles.ADMINISTRATORS)]
        public async Task<ActionResult<IReadOnlyList<CatalogBrand>>> Delete(int id)
        {
            try  {
                var brand = await _brandRepository.GetByIdAsync(id);
                if(brand == null){ return NotFound();}

                await _brandRepository.DeleteAsync(brand);
                return Ok(brand);
            } catch (ModelNotFoundException) {
                return NotFound();
            }
        }
    }
}
