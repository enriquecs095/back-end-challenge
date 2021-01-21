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

        [HttpGet("getProducts{idMenu}")]
        public IActionResult getProducts(int idMenu)
        {
            var getdata = (from p in db.Productos.Where(options => options.Idmenu == idMenu && options.Status == 1)
                           select new { p.Idproducto, p.Precio, p.Nombre, p.Descripcion, p.Idmenu, p.Url }).ToList();
            return Json(getdata);
        }
    }
}
