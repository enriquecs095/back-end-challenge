using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Challenge_AWS.Models;
using Microsoft.EntityFrameworkCore;



namespace Challenge_AWS.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PagosController:ControllerBase {
        public Models.postgresContext db;

        public PagosController(Models.postgresContext context) {
            db = context;
        }

        [HttpPost("IngresarOrden")]
        public async Task<int> ingresarCompra([FromBody] Ordene model) {
            var data = await db.Clientes.SingleOrDefaultAsync(x => x.Idusuario == model.Idcliente);
            model.Fechaorden = DateTime.Now;
            model.Idcliente = data.Idcliente;
             db.Add(model);
             db.SaveChanges();
            return model.Idorden;
        }



        [HttpPost("IngresarOrdenDetalle/{idusuario}/{idorden}")]
        public async  Task<IActionResult> ingresarOrdenes(int idusuario, int idorden) {
            OrdenesProducto model;
            var data = await (from lc in db.ListaCarritos.Where(option => option.Idusuario == idusuario)
                        select new
                        {
                            lc.Idproducto,
                            lc.IdLista,
                            lc.IdproductoNavigation.Nombre,
                            lc.IdproductoNavigation.Descripcion,
                            lc.IdproductoNavigation.Precio,
                            lc.IdproductoNavigation.Url,
                            lc.Cantidad
                        }).ToListAsync();
            if (data.Count() == 0)
            {
                return Ok(false);
            }

            for (int i = 0; i < data.Count(); i++)
            {
                model = new OrdenesProducto();
                model.IdOrden = idorden;
                model.IdProducto = data[i].Idproducto;
                model.TotalProducto = data[i].Cantidad * (data[i].Precio);
                model.Cantidad = data[i].Cantidad;
                db.OrdenesProductos.Add(model);
                db.SaveChanges();
            }
            return Ok(true);
        }


        [HttpPost("AgregarAlCarrito")]
        public async Task<IActionResult> agregarAlCarrito([FromBody] ListaCarrito model)
        {
            var data = await(from lc in db.ListaCarritos.Where(option =>
                        option.Idusuario == model.Idusuario &&
                        option.Idproducto == model.Idproducto)
                        select new { lc.IdLista }).ToListAsync();
            if (data.Count() > 0)
            {
                return Ok(false);
            }
            model.Cantidad = 1;
            await db.ListaCarritos.AddAsync(model);
            await db.SaveChangesAsync();
            return Ok(true);
        }


        [HttpGet("GetListaCarrito{idusuario}")]
        public async Task<IActionResult> getListaCarrito(int idusuario)
        {
            var data = await (from lc in db.ListaCarritos.Where(option => option.Idusuario == idusuario)
                        select new { lc.Idproducto,
                                     lc.IdLista,
                                    lc.IdproductoNavigation.Nombre,
                                    lc.IdproductoNavigation.Descripcion,
                                    lc.IdproductoNavigation.Precio,
                                    lc.IdproductoNavigation.Url,
                                    lc.Cantidad}).ToListAsync();
            if (data.Count() == 0)
            {
                return Ok(0);
            }
            return Ok(data);
        }

        [HttpGet("GetTotalCarrito{idusuario}")]
        public async Task<IActionResult> getTotalCarrito(int idusuario)
        {
            decimal? total = 0;
            var data = await (from lc in db.ListaCarritos.Where(option => option.Idusuario == idusuario)
                        select new
                        {
                            lc.Idproducto,
                            lc.IdLista,
                            lc.IdproductoNavigation.Nombre,
                            lc.IdproductoNavigation.Descripcion,
                            lc.IdproductoNavigation.Precio,
                            lc.IdproductoNavigation.Url,
                            lc.Cantidad
                        }).ToListAsync();
            if (data.Count() == 0)
            {
                return Ok(false);
            }
            
            for (int i=0; i < data.Count(); i++) {
                total += data[i].Cantidad * (data[i].Precio);
            }
            return Ok(total);
        }


        [HttpDelete("EliminarDelCarrito/{idproducto}/{idusuario}")]
        public async Task<IActionResult> eliminarDelCarrito(int idproducto, int idusuario)
        {
            var data = await db.ListaCarritos.FirstOrDefaultAsync(x => 
                            x.Idproducto == idproducto &&
                            x.Idusuario==idusuario);
            db.ListaCarritos.Remove(data);
            await db.SaveChangesAsync();
            return Ok();
        }


        [HttpPut("ActualizarTotal/{idusuario}/{idproducto}/{cantidad}")]
        public async Task<IActionResult> ActualizarTotal(int idusuario, int idproducto, int cantidad)
        {
            var data = await db.ListaCarritos.SingleOrDefaultAsync(x => 
                         x.Idusuario == idusuario &&
                         x.Idproducto==idproducto);
            data.Cantidad = cantidad;
            db.ListaCarritos.Attach(data);
            db.Entry(data).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete("VaciarCarrito/{idusuario}")]
        public async Task<IActionResult> VaciarCarrito(int idusuario)
        {
            var data =  await db.ListaCarritos.Where(option => option.Idusuario == idusuario).ToListAsync();
            if (data.Count() == 0)
            {
                return Ok(false);
            }

            for (int i = 0; i < data.Count(); i++)
            {
                db.ListaCarritos.Remove(data[i]);
                await db.SaveChangesAsync();
            }
            return Ok();
        }


        [HttpPost("enviarEmail/{destino}/{id}/{total}")]
        public async Task<IActionResult> enviarEmail(string destino,int id,decimal total) {

            string _sender = "enrik_cs@hotmail.com";
            string _password = "Capillas";

            SmtpClient client = new SmtpClient("smtp-mail.outlook.com");

            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential(_sender, _password);
            client.EnableSsl = true;
            client.Credentials = credentials;
            MailMessage message = new MailMessage(_sender,destino);
            message.Subject = "Crunchy Roll Pizza";
            message.Body = "Felicidades has completado tu orden en Crunchy roll piza\nAgradecemos tu confiabilidad\nEste es tu ID de Orden:"+id + "\nCon un Total de :"+total+"\nEl repartidor llegara en aproximadamente 30 Minutos\nGracias por comprar en Crunchy Roll!";
            await client.SendMailAsync(message);
            client.Dispose();
            return Ok();
        }
    }
}
