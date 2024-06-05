using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogChallenge.Application.Commands;
using ProductCatalogChallenge.Application.Interfaces;
using ProductCatalogChallenge.Application.Queries;
using ProductCatalogChallenge.Domain.Entities;

namespace ProductCatalogChallenge.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ICommandHandler<CreateProductCommand,int> _createProductHandler;
        private readonly ICommandHandler<UpdateProductCommand, bool> _updateProductHandler;
        private readonly IQueryHandler<GetAllProductsQuery, IEnumerable<Product>> _getAllProductsHandler;
        private readonly IQueryHandler<GetProductByIdQuery, Product> _getProductByIdHandler;


        public ProductController(
            ICommandHandler<CreateProductCommand, int> createProductHandler,
            ICommandHandler<UpdateProductCommand, bool> updateProductHandler,
            IQueryHandler<GetAllProductsQuery, IEnumerable<Product>> getAllProductsHandler,
            IQueryHandler<GetProductByIdQuery, Product> getProductByIdHandler)
        {
            _createProductHandler = createProductHandler;
            _updateProductHandler = updateProductHandler;
            _getAllProductsHandler = getAllProductsHandler;
            _getProductByIdHandler = getProductByIdHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            await _createProductHandler.HandleAsync(command);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _getAllProductsHandler.HandleAsync(new GetAllProductsQuery());
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var query = new GetProductByIdQuery(id);
            var product = await _getProductByIdHandler.HandleAsync(query);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductCommand command)
        {
            if (id != command.ProductId)
            {
                return BadRequest("Product ID mismatch");
            }

            try
            {
                var result = await _updateProductHandler.HandleAsync(command);

                if (result)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
