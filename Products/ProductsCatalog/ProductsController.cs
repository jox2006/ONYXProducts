using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ONYX.Configuration;
using ONYXProducts.Application.AdaptersPorts.WebApi;
using ONYXProducts.Application.UseCases.UserAuthentication;
using ONYXProducts.WebApi.ProductsCatalog;
using System.Text.Json;

namespace ONYX.Products.Catalog.Adapters
{
    [ApiController]
    [Route($"{ApiRoutes.Base}/[controller]")]
    [Produces("application/json")]
    [Authorize()]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsCatalog _productsCatalog;
        private readonly IJwtONYXAuthenticator _jwtONYXAuthenticator;

        public ProductsController(IProductsCatalog productsCatalog, IJwtONYXAuthenticator jwtONYXAuthenticator)
        {
            _productsCatalog = productsCatalog;
            _jwtONYXAuthenticator = jwtONYXAuthenticator;
        }

        [HttpGet(ApiRoutes.GetAll)]
        public async Task<ActionResult<ProductWebDto>> GetAllAsync()
        {
            var products = await _productsCatalog.GetAllProductsAsync();

            if (products.Any()){
                return Ok((new JsonResult(products.Select(p => Mapper.ToProductWebDto(p)))).Value);
            }
            return NoContent();


        }

        [HttpGet(ApiRoutes.GetByColour)]
        public async Task<ActionResult<ProductWebDto>> GetByColourAsync(string colour)
        {

            var products = await _productsCatalog.GetAllProductsByColourAsync(colour);

            if (products.Any())
                return Ok((new JsonResult(products.Select(p => Mapper.ToProductWebDto(p)))).Value);

            return NoContent();

        }
        /// <summary>
        /// This method ideally should go in a different controller where dealing with the users. I am adding it here for simplicity. 
        /// </summary>
        /// <param name="userCredentials"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost(ApiRoutes.Authenticate)]
        public async Task<IActionResult> Authenticate([FromBody] UserCredential userCredentials)
        {
            //WARNING: This method ideally should go in a different controller where dealing with the users. I am adding it here for simplicity.

            var token = await _jwtONYXAuthenticator.AuthenticateAsync(userCredentials.userName, userCredentials.password);
            if (string.IsNullOrEmpty(token))
                return Unauthorized();
            return Ok();
        }
    }
}