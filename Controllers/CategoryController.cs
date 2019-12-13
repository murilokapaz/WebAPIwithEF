using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiEf.Data;
using WebApiEf.api.Models;

namespace WebApiEf.api.Controllers
{
    [ApiController]
    [Route("v1/categories")]//se não tem uma página inicial ele inicia aqui
    public class CategoryController: ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Get([FromServices] DataContext context)//from services pega o datacontext que esta na memoria (elimina o _context)
        {
            var categories = await context.Categories.ToListAsync();
            return categories;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Category>> Post(
            [FromServices] DataContext context,
            [FromBody] Category model)
        {
            if(ModelState.IsValid)//valida se possui todas categorias colocadas na model ex: [required], [maxlength]
            {
                context.Categories.Add(model);
                await context.SaveChangesAsync();
                return model;
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        
    }
}