using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Challenge_AWS.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class MenuController : Controller
    {
        public Models.postgresContext db;
        public MenuController(Models.postgresContext menu)
        {
            db = menu;
        }

        [HttpGet("getMenu")]
        public async Task<IActionResult> getMenu()
        {
            var getdata = await(from m in db.Menus orderby m.Idmenu select new { m.Idmenu, m.Nombre }).ToListAsync();
            return Json(getdata);
        }


        [HttpGet("getProducts{idMenu}")]
        public async Task<IActionResult> getProducts(int idMenu)
        {
            var getdata = await (from p in db.Productos.Where(options => options.Idmenu == idMenu && options.Status == 1)
                          orderby p.Idproducto select new { p.Idproducto, p.Precio, p.Nombre, p.Descripcion, p.Idmenu, p.Url }).ToListAsync();
          
            return Json(getdata);
        }
    }
}
