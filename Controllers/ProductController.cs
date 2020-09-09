using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.data;
using Shop.Models;

namespace Shop.Controllers
{
    [Route("v1/products")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Product>>> Get(
            [FromServices] DataContext context
        )
        {
            var products = await context.Products
                .Include(prod => prod.Category)
                .AsNoTracking()
                .ToListAsync();

            return Ok(products);
        }

        [HttpGet]
        [Route("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Product>>> GetById(
            int id,
            [FromServices] DataContext context
        )
        {
            var product = await context.Products
                .Include(prod => prod.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(prod => prod.Id == id);

            return Ok(product);
        }

        [HttpGet]
        [Route("categories/{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Product>>> GetByCategoryId(
            int id,
            [FromServices] DataContext context
        )
        {
            var products = await context.Products
                .Include(prod => prod.Category)
                .AsNoTracking()
                .Where(prod => prod.CategoryId == id)
                .ToListAsync();

            return Ok(products);
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<Product>> Post(
            [FromBody] Product model,
            [FromServices] DataContext context
        )
        {
            if (ModelState.IsValid)
            {
                try
                {
                    context.Products.Add(model);
                    await context.SaveChangesAsync();
                    return Ok(model);
                }
                catch
                {
                    return BadRequest(new { message = "Algo inesperado aconteceu!" });
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}