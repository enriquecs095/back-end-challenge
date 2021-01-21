using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using challenge_aa.Models;

namespace challenge_aa.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class PagosController:ControllerBase {
        public Models.postgresContext db;

        public PagosController(Models.postgresContext context) {
            db = context;
        }

        [HttpPost("IngresarCompra")]
        public int ingresarCompra([FromBody] Ordene model) {
            var data = db.Clientes.SingleOrDefault(x => x.Idusuario == model.Idcliente);
            model.Fechaorden = DateTime.Now;
            model.Idcliente = data.Idcliente;
            db.Add(model);
            db.SaveChanges();
            return model.Idorden;
        }

        [HttpPost("IngresarOrdenes")]
        public IActionResult ingresarOrdenes([FromBody] OrdenesProducto model) {
            db.OrdenesProductos.Add(model);
            db.SaveChanges();
            return Ok();
        }

        [HttpPost("enviarEmail/{destino}/{id}/{total}")]
        public IActionResult enviarEmail(string destino,int id,int total) {

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
            client.Send(message);
            client.Dispose();
            //db.OrdenesProductos.Add(model);
            //db.SaveChanges();
            return Ok();
        }
    }
}
