using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.data;
using Shop.Models;

[Route("v1/categories")]
public class CategoryController : ControllerBase
{
    [HttpGet]
    [Route("")]
    [Authorize(Roles = "employee")]
    public async Task<ActionResult<List<Category>>> Get(
        [FromServices] DataContext context
    )
    {
        // Retorna a lista de todas categorias
        var categories = await context.Categories.AsNoTracking().ToListAsync();

        return Ok(categories);
    }

    [HttpGet]
    [Route("{id:int}")]
    [Authorize(Roles = "employee")]
    public async Task<ActionResult<Category>> GetById(
        int id,
        [FromServices] DataContext context
    )
    {
        // Retorna a categoria específica
        var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(
            cat => cat.Id == id
        );

        return Ok(category);
    }

    [HttpPost]
    [Route("")]
    [Authorize(Roles = "employee")]
    public async Task<ActionResult<Category>> Post(
        [FromBody] Category model,
        [FromServices] DataContext context
    )
    {
        // Se a categoria não for valida na model
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Insere os dados no db inMemory caso não ocorra uma exception
        try
        {
            context.Categories.Add(model);
            await context.SaveChangesAsync();
            return Ok(model);
        }
        catch
        {
            return BadRequest(new { message = "Algo inesperado aconteceu!" });
        }
    }

    [HttpPut]
    [Route("{id:int}")]
    [Authorize(Roles = "employee")]
    public async Task<ActionResult<Category>> Put(
        int id,
        [FromBody] Category model,
        [FromServices] DataContext context
    )
    {
        // Se a categoria do body não for igual da url
        if (model.Id != id)
        {
            return NotFound(new { message = "Categoria não encontrada!" });
        }

        // Se a categoria não for valida na model
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Atualiza o dado em memoria
        try
        {
            context.Entry<Category>(model).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Ok(model);
        }
        catch (DbUpdateConcurrencyException)
        {
            return BadRequest(new { message = "Esta categoria já foi atualizada!" });
        }
        catch (Exception)
        {
            return BadRequest(new { message = "Algo inesperado aconteceu!" });
        }
    }

    [HttpDelete]
    [Route("{id:int}")]
    [Authorize(Roles = "employee")]
    public async Task<ActionResult<Category>> Delete(
        int id,
        [FromServices] DataContext context
    )
    {
        // Consulta em memória o registro enviado como paramêtro
        var category = await context.Categories.FirstOrDefaultAsync(cat => cat.Id == id);
        if (category == null)
        {
            return NotFound(new { message = "Categoria não encontrada!" });
        }

        // Remove em memória a categoria informada
        try
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
            return Ok(new { message = "Categoria apagada com sucesso!" });
        }
        catch (System.Exception)
        {
            return BadRequest(new { message = "Algo inesperado aconteceu!" });
        }
    }
}