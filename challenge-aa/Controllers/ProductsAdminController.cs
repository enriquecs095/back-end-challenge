using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using challenge_aa.Models;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace challenge_aa.Controllers
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
        public IActionResult getProductsByMenu(int id) {
            var data = (from p in db.Productos.Where(option => option.Idmenu == id)
                        select new { p.Idproducto, p.Precio,p.Nombre, p.Idmenu, p.Descripcion, p.Url, p.Status }).ToList();
                return Json(data);
        }

        [HttpGet("getProductById{id}")]
        public IActionResult getProductById(int id) {
            var data = (from p in db.Productos.Where(option => option.Idproducto == id)
                        select new { p.Idproducto, p.Precio, p.Nombre, p.Idmenu, p.Descripcion, p.Url, p.Status }).ToList();
            if (data.Count()==0)
            {
              return Ok(false);
               /// return BadRequest(new { message = "Producto no existe" });
            }
            return Json(data);
        }


        [HttpPost("addProduct")]
        public IActionResult addProduct([FromBody] Producto productos) 
        {
            db.Productos.Add(productos);
            db.SaveChanges();
            return Ok();
        }

        [HttpDelete("deleteProduct{id}")]
        public IActionResult deleteProduct(int id)
        {
            var product = new Producto { Idproducto = id };
            db.Productos.Remove(product);
            db.SaveChanges();
            return Ok();
        }

        [HttpPut("putProduct")]
        public IActionResult putProduct([FromBody] Producto producto)
        {
            db.Productos.Attach(producto);
            db.Entry(producto).State = EntityState.Modified;
            db.SaveChanges();
            return Ok();
        }

        [HttpPut("ProductStatus")]
        public IActionResult ProductStatus([FromBody] Producto producto)
        {
            db.Productos.Attach(producto);
            db.Entry(producto).State = EntityState.Modified;
            db.SaveChanges();
            return Ok();
        }
    }
}
