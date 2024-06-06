using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductCatalogChallenge.Api.Models.Requests;
using ProductCatalogChallenge.Api.Models.Responses;
using ProductCatalogChallenge.Application.Commands;
using ProductCatalogChallenge.Application.Interfaces;
using ProductCatalogChallenge.Application.Queries;
using ProductCatalogChallenge.Domain.Entities;
using ProductCatalogChallenge.Domain.Enums;

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
        private readonly IMapper _mapper;


        public ProductController(
            ICommandHandler<CreateProductCommand, int> createProductHandler,
            ICommandHandler<UpdateProductCommand, bool> updateProductHandler,
            IQueryHandler<GetAllProductsQuery, IEnumerable<Product>> getAllProductsHandler,
            IQueryHandler<GetProductByIdQuery, Product> getProductByIdHandler,
            IMapper mapper)
        {
            _createProductHandler = createProductHandler;
            _updateProductHandler = updateProductHandler;
            _getAllProductsHandler = getAllProductsHandler;
            _getProductByIdHandler = getProductByIdHandler;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {

            var command  =  _mapper.Map<CreateProductCommand>(request);
            var productId = await _createProductHandler.HandleAsync(command);

            var product = new Product
            {
                ProductId = productId,
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                StatusId = (int)ProductCatalogStatus.Active,
                CreatedAt = DateTime.UtcNow
            };

            var response = _mapper.Map<CreateProductResponse>(product);

            return CreatedAtAction(nameof(GetProductById), new { id = productId }, response);
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
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductRequest request)
        {
            try
            {
                var command = _mapper.Map<UpdateProductCommand>(request);
                command.ProductId = id;
                        
                var result = await _updateProductHandler.HandleAsync(command);

                if (!result)
                {
                    return NotFound();
                }

                return NoContent();

            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
