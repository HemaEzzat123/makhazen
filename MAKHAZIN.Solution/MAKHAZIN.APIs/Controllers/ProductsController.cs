using MAKHAZIN.APIs.Errors;
using MAKHAZIN.Application.Features.Products.Commands;
using MAKHAZIN.Application.Features.Products.Query;
using MAKHAZIN.Core.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MAKHAZIN.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all products with pagination and optional search
        /// </summary>
        /// <param name="search">Optional search term to filter products by name</param>
        /// <param name="pageSize">Number of items per page (default: 10)</param>
        /// <param name="pageIndex">Page number (default: 1)</param>
        /// <returns>Paginated list of products</returns>
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductDTO>>> GetProducts(
            [FromQuery] string? search,
            [FromQuery] int pageSize = 10,
            [FromQuery] int pageIndex = 1)
        {
            var query = new GetProductsQuery
            {
                Search = search,
                PageSize = pageSize,
                PageIndex = pageIndex
            };

            var result = await _mediator.Send(query);

            if (result.IsSuccess)
                return Ok(result.Value);

            return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, result.Error));
        }

        /// <summary>
        /// Get a specific product by ID
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>Product details</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var query = new GetProductByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result.IsSuccess)
                return Ok(result.Value);

            return NotFound(new ApiResponse((int)HttpStatusCode.NotFound, result.Error));
        }

        /// <summary>
        /// Create a new product (Seller or Admin only)
        /// </summary>
        /// <param name="command">Product creation details</param>
        /// <returns>Created product ID</returns>
        [HttpPost]
        [Authorize(Roles = "Seller,Admin")]
        public async Task<ActionResult<int>> CreateProduct(CreateProductCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
                return Ok(new { productId = result.Value, message = "Product created successfully" });

            return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, result.Error));
        }

        /// <summary>
        /// Update an existing product (Seller or Admin only)
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <param name="command">Updated product details</param>
        /// <returns>Success or failure</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Seller,Admin")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductCommand command)
        {
            if (id != command.Id)
                return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, "Product ID mismatch"));

            var result = await _mediator.Send(command);

            if (result.IsSuccess)
                return Ok(new { message = "Product updated successfully" });

            return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, result.Error));
        }

        /// <summary>
        /// Delete a product (Admin only)
        /// </summary>
        /// <param name="id">Product ID</param>
        /// <returns>Success or failure</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var command = new DeleteProductCommand(id);
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
                return Ok(new { message = "Product deleted successfully" });

            return BadRequest(new ApiResponse((int)HttpStatusCode.BadRequest, result.Error));
        }
    }
}
