using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiEf.Data;
using WebApiEf.api.Models;
using Microsoft.AspNetCore.Http;

namespace WebApiEf.api.Controllers
{
    [ApiController]
    [Route("api/v1/{controller}")]//se não tem uma página inicial ele inicia aqui
    public class CategoryController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public CategoryController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet, Route("{id}")]
        public ActionResult<Category> Get([FromQuery] int id)//from services pega o datacontext que esta na memoria (elimina o _context)
        {
            var category = _dataContext.Categories.Find(id);
            return Ok(category);
        }

        [HttpGet, Route("list")]
        public async Task<ActionResult<List<Category>>> GetList()//from services pega o datacontext que esta na memoria (elimina o _context)
        {
            var categories = await _dataContext.Categories.ToListAsync();
            return Ok(categories);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> Post([FromBody] Category model)
        {
            if (ModelState.IsValid)//valida se possui todas categorias colocadas na model ex: [required], [maxlength]
            {
                _dataContext.Categories.Add(model);
                await _dataContext.SaveChangesAsync();

                return StatusCode(StatusCodes.Status201Created, model);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

    }
}