using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using challenge_aa.Models;

namespace challenge_aa.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController:ControllerBase {

        public Models.postgresContext db;
        public ChatsController(Models.postgresContext context) {
            db = context;
        }

        [HttpGet("ChatsActivos")]
        public IActionResult getChatsActivos() {//Registro de usuariosq
            var data = (from a in db.Chats
                        join b in db.Clientes on a.Idcliente equals b.Idcliente
                        select new { a.Idchat, b.Idusuario, b.IdusuarioNavigation.Nombre, a.Status, b.IdusuarioNavigation.Rol }).ToList();
            return Ok(data);
        }
        [HttpGet("ChatCliente/{id}")]
        public IActionResult getChatClient(int id) {//Registro de usuariosq
            var data = db.Chats.Where(res => res.IdclienteNavigation.Idusuario == id)
                .Select(x => new { x.IdadministradorNavigation.IdusuarioNavigation.Nombre, x.Idchat, x.Idcliente, x.Idadministrador }).ToList();
            if (data != null) {
                return Ok(data);
            }
            return Ok();
        }

        [HttpGet("getMessageClient/{id}")]
        public IActionResult getMessageClient(int id) {//Registro de usuariosq
            var data = db.Mensajes.Where(res => res.Idchat == id)
                .Select(x => new { x.Idmensaje, x.Status, x.IdchatNavigation.IdclienteNavigation.IdusuarioNavigation.Rol, x.Mensaje1 }).ToList();

            if (data != null) {
                return Ok(data);
            }
            return Ok();
        }
        [HttpPost("InsertarMensajeC")]
        public IActionResult postMessageClient([FromBody] Mensaje msj) {
            msj.Fechamensaje = DateTime.Now;
            msj.Status = 1;
            db.Mensajes.Add(msj);
            db.SaveChanges();
            return Ok();
        }

        [HttpPost("InsertarMensajeA")]
        public IActionResult postMessageAdmin([FromBody] Mensaje msj) {
            msj.Fechamensaje = DateTime.Now;
            msj.Status = 0;
            db.Mensajes.Add(msj);
            db.SaveChanges();
            return Ok();
        }

    }
}

