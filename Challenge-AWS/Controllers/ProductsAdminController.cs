using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Challenge_AWS.Models;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Challenge_AWS.Controllers
{
    [ApiController]
    [Route("[controller]")]
     public class ProductsAdminController : Controller
    {
        
        public postgresContext db;
        public ProductsAdminController(Models.postgresContext products) {
            db = products;
        }

        [HttpGet("getProductsByMenu{id}")]
        public async Task<IActionResult> getProductsByMenu(int id) {
            var data = await (from p in db.Productos.Where(option => option.Idmenu == id)
                       orderby p.Idproducto select new { p.Idproducto, p.Precio,p.Nombre, p.Idmenu, p.Descripcion, p.Url, p.Status }).ToListAsync();
                return Json(data);
        }

        [HttpGet("getProductById{id}")]
        public async Task<IActionResult> getProductById(int id) {
            var data = await (from p in db.Productos.Where(option => option.Idproducto == id)
                        select new { p.Idproducto, p.Precio, p.Nombre, p.Idmenu, p.Descripcion, p.Url, p.Status }).ToListAsync();
            if (data.Count()==0)
            {
              return Ok(false);
            }
            return Json(data);
        }


        [HttpPost("addProduct")]
        public async Task<IActionResult> addProduct([FromBody] Producto productos) 
        {
            await db.Productos.AddAsync(productos);
            await db.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete("deleteProduct{id}")]
        public async Task<IActionResult> deleteProduct(int id)
        {
            var product = new Producto { Idproducto = id };
            db.Productos.Remove(product);
            await db.SaveChangesAsync();
            return Ok();
        }



        [HttpPut("putProduct")]
        public async Task<IActionResult> putProduct([FromBody] Producto producto)
        {
            db.Productos.Attach(producto);
            db.Entry(producto).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("ProductStatus")]
        public async Task<IActionResult> ProductStatus([FromBody] Producto producto)
        {
            db.Productos.Attach(producto);
            db.Entry(producto).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}
