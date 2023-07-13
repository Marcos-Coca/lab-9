using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {

        // Get : api/Productos paginados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DB.Producto>>> GetAsync([FromQuery] ProductosFilter filter)
        {
            using (var db = new DB.Lab8Context())
            {
                var query = db.Productos.AsQueryable();

                if (!string.IsNullOrEmpty(filter.Search))
                {
                    query = query.Where(p => p.Nombre.Contains(filter.Search));
                }

                if (!string.IsNullOrEmpty(filter.OrderBy))
                {
                    var propertyInfo = typeof(DB.Producto).GetProperty(filter.OrderBy);
                    if (propertyInfo != null)
                    {
                        if (filter.OrderDirection == "desc")
                        {
                            query = query.OrderByDescending(p => propertyInfo.GetValue(p, null));
                        }
                        else
                        {
                            query = query.OrderBy(p => propertyInfo.GetValue(p, null));
                        }
                    }
                }

                var total = await query.CountAsync();
                var productos = await query.Skip((filter.Page - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();

                return Ok(new
                {
                    Total = total,
                    Productos = productos
                });
            }   

        }
    }
}
