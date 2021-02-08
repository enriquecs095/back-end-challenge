using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Challenge_AWS.Models;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Challenge_AWS.Controllers {
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
        public async Task<IActionResult> ValidarUsuario([FromBody] Usuario model) {

            if (model == null){ 
                return BadRequest(new { message = "Usuario Invalido!" }); ;
            }

            var data = await getUserLog(model);

            if (data!=null)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "http://angular-deploy.s3-website.us-east-2.amazonaws.com",
                    audience: "http://angular-deploy.s3-website.us-east-2.amazonaws.com",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString });
            }
            else {
                  return BadRequest(new { message = "Usuario Invalido!" });
            }
        }


        private async Task<Object> getUserLog(Usuario model) {
              Object data = await db.Usuarios.Where(options => options.Correo == model.Correo.ToUpper() &&
                                                                  options.Contrasena == model.Contrasena)
                    .Select(u => new { u.Idusuario, u.Nombre, u.Correo, u.Rol }).SingleOrDefaultAsync();
            return data;
        }



        [HttpPost("getUser")]
        public async Task<IActionResult> getUser([FromBody] Usuario model)
        {
            var data = await getUserLog(model);

            if (data!=null)
            {
                return  Ok(data);

            }
            return BadRequest( "Error");
        }


        [HttpPost("getOrder/{idUsuario}")]
        public async Task<IActionResult> getOrden(int idUsuario) {
            var data = await db.Clientes.FirstOrDefaultAsync(x => x.Idusuario == idUsuario);
            var orders = await (from o in db.Ordenes.Where(options => options.Idcliente == data.Idcliente)
                                 orderby o.Idorden descending
                                 select new { o.Idorden, o.Totalorden, o.Fechaorden}).ToListAsync();
            return Ok(orders);
        }



        [HttpPost("getDetails/{idOrden}")]
        public async Task<IActionResult> getDetails(int idOrden) {
            var productos = await (from op in db.OrdenesProductos.Where(options => options.IdOrden == idOrden)
                                orderby op.IdProducto
                                select new { op.IdProducto, op.IdProductoNavigation.Nombre, op.Cantidad, op.TotalProducto, op.IdProductoNavigation.Precio, op.IdProductoNavigation.Url }).ToListAsync();
            return Ok(productos);
        }
    }
}
