using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ONYX.Configuration;
using ONYXProducts.Application.AdaptersPorts.WebApi;
using ONYXProducts.Application.UseCases.UserAuthentication;
using System.Web.Http;

namespace ONYX.Products.Adapters.WebAPI
{
    [ApiController]
    [Route(ApiRoutes.Base)]
    [Authorize()]
    public class ProductsController : ControlerBase
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

            if (products.Count() == 0)
                return NoContent();
            else
                return Ok(new JsonResult(products));

        }

        [HttpGet(ApiRoutes.GetByColour)]
        public async Task<ActionResult<ProductWebDto>> GetByColourAsync(string colour)
        {

            var products = await _productsCatalog.GetAllProductsByColourAsync(colour);

            if (products.Count() == 0)
                return NoContent();
            else
                return Ok(new JsonResult(products));
        }
        /// <summary>
        /// This method ideally should go in a different controller where dealing with the users. I am adding it here for simplicity. 
        /// </summary>
        /// <param name="userCredentials"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost(ApiRoutes.Authenticate)]
        public async Task<IActionResult> Authenticate([FromBody]UserCredential userCredentials)
        {
            //WARNING: This method ideally should go in a different controller where dealing with the users. I am adding it here for simplicity.

            var token = await _jwtONYXAuthenticator.AuthenticateAsync(userCredentials.userName, userCredentials.password); 
            if(string.IsNullOrEmpty(token))
                return Unauthorized();
            return Ok();
        }
    }
}