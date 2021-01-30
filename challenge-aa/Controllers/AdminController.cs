using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using challenge_aa.Models;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace challenge_aa.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController:ControllerBase {
        public Models.postgresContext db;

        public AdminController(Models.postgresContext context) {
            db = context;
        }
        /// <summary>  
        /// Check and register the new user.  
        /// </summary>  
        /// <returns>Returns the data</returns>  
        // POST API/blablala
        [HttpPost("Registro")]
        public IActionResult RegistrarUsuario([FromBody] Usuario model) {//Registro de usuarios
            var data = db.Usuarios.Where(options => options.Correo == model.Correo.ToUpper()).ToList();
            if (data.Count >= 1) {
                return BadRequest(new { message = "Correo ya existe!" });
            }
            model.Correo = model.Correo.ToUpper();
            model.Rol = 1;
            db.Usuarios.Add(model);
            db.SaveChanges();
            var usuario = new Cliente {
                Idusuario = model.Idusuario,
                Telefono = "999999"
            };
            db.Clientes.Add(usuario);
            db.SaveChanges();
            return Ok(data);
        }

        [HttpPost("Login")]
        public IActionResult ValidarUsuario([FromBody] Usuario model) {//Registro de usuarios
            var data = (from m in db.Usuarios.Where(options => options.Correo == model.Correo.ToUpper() && 
                        options.Contrasena == model.Contrasena) select new { m.Correo,m.Idusuario,m.Nombre,m.Rol}).ToList();
            if (data.Count >= 1) {
                return Ok(data);
            }
            return BadRequest(new { message = "Usuario Invalido!" }); ;
        }

        [HttpPost("getOrder/{id}")]
        public IActionResult getOrden(int id) {
            var data = db.Clientes.FirstOrDefault(x => x.Idusuario == id);
            var orders = db.Ordenes.Where(x => x.Idcliente == data.Idcliente)
                .Select(x => new { x.Idorden ,x.Totalorden ,x.Fechaorden}).ToList();
            return Ok(orders);
        }

        [HttpPost("getDetails/{id}")]
        public IActionResult getDetails(int id) {
            var productos = db.OrdenesProductos.Where(x => x.IdOrden == id)
                .Select(x => new { x.IdProducto, x.IdProductoNavigation.Nombre, x.Cantidad, x.TotalProducto, x.IdProductoNavigation.Precio, x.IdProductoNavigation.Url }).ToList();
            return Ok(productos);
        }
    }
}
