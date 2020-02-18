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
        /// All catalog brands list
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return CatalogBrand list</response>
        [HttpGet]
        [Authorize(ApiAuthorizationConstants.CATALOG_BRAND_READ_SCOPE)]
        public async Task<ActionResult<IReadOnlyList<CatalogBrand>>> List()
        {
            var brands = await _brandRepository.ListAllAsync();
            return Ok(brands);
        }

        /// <summary>
        /// Catalog brand by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Return CatalogBrand by id</response>
        /// <response code="400">Somehere an error was triggered</response>
        [HttpGet("{id}")]
        [Authorize(ApiAuthorizationConstants.CATALOG_BRAND_READ_SCOPE)]
        public async Task<ActionResult<CatalogBrand>> GetById(int id) {
            try  {
                var brand = await _brandRepository.GetByIdAsync(id);
                return Ok(brand);
            } catch (ModelNotFoundException me) {
                return BadRequest(me);
            }
        }

        /// <summary>
        /// Catalog brand by brand name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <response code="200">Return CatalogBrand by name</response>
        /// <response code="400">Somehere an error was triggered</response>
        [HttpGet("{name}")]
        [Authorize(ApiAuthorizationConstants.CATALOG_BRAND_READ_SCOPE)]
        public async Task<ActionResult<CatalogBrand>> GetByBrand(string name) {
            try  {
                var brands = await _brandRepository.ListAllAsync();
                var brandFindeds = brands.Where(x => string.Compare(x.Brand, name, StringComparison.InvariantCultureIgnoreCase) == 0);
                return Ok(brandFindeds);
            } catch (ModelNotFoundException me) {
                return BadRequest(me);
            }
        }

        // Post: api/Brands/
        /// <summary>
        /// Insert new catalog brand
        /// </summary>
        /// <remarks>
        /// Example:
        ///
        ///     CatalogBrand
        ///     {
        ///        "Brand": "Brand1"
        ///     }
        /// </remarks>
        /// <param name="catalogBrand"></param>
        /// <returns></returns>
        /// <response code="200">CatalogBrand successfully saved</response>
        /// <response code="400">Somehere an error was triggered</response>
        /// <response code="409">CatalogBrand exists with the same name</response>
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
            } catch (ModelNotFoundException me) {
                return BadRequest(me);
            }
        }

        /// <summary>
        /// Update catalog brand
        /// api/Brands/
        /// </summary>
        /// <remarks>
        /// Example:
        ///
        ///     CatalogBrand
        ///     {
        ///        "Id" : 2,    
        ///        "Brand": "Brand1"
        ///     }
        /// </remarks>
        /// <param name="catalogBrand"></param>
        /// <returns></returns>
        /// <response code="200">CatalogBrand successfully updated</response>
        /// <response code="400">Somehere an error was triggered</response>
        /// <response code="409">CatalogBrand exists with the same name</response>
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
            } catch (ModelNotFoundException me) {
                return BadRequest(me);
            }
        }

        /// <summary>
        /// Delete catalog crand By id
        /// api/Brands/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">CatalogBrand successfully deleted</response>
        /// <response code="400">Somehere an error was triggered</response>
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
            } catch (ModelNotFoundException me) {
                return BadRequest(me);
            }
        }
    }
}
