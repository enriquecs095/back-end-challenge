using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace challenge_aa.Controllers
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
        public IActionResult getMenu()
        {
            var getdata = (from m in db.Menus select new { m.Idmenu, m.Nombre }).ToList();
            return Json(getdata);
        }

        [HttpGet("getProducts{idMenu}")]
        public IActionResult getProducts(int idMenu)
        {
            var getdata = (from p in db.Productos.Where(options => options.Idmenu == idMenu && options.Status == 1)
                           select new { p.Idproducto, p.Precio, p.Nombre, p.Descripcion, p.Idmenu, p.Url }).ToList();
          
            return Json(getdata);
        }
    }
}
