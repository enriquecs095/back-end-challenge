using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Challenge_AWS.Models;
using Microsoft.EntityFrameworkCore;


namespace Challenge_AWS.Controllers
{
    [ApiController]
    [Route("[controller]")]
     public class ReviewsController : Controller
    {
        
        public Models.postgresContext db;
        public ReviewsController(Models.postgresContext reviews)
        {
            db = reviews;
        }

        [HttpPost("postReviews{id}")]
        public async Task<IActionResult> postReviews(int id)
        {
            var getdata = await (from r in db.Reviews.Where(options => options.Idproducto == id)
                           select new { r.Mensaje, r.Fecha, r.Valoracion, r.Idproducto }).ToListAsync();
            return Json(getdata);
        }

        [HttpGet("getReviews{id}")]
        public async Task<IActionResult> getReviews(int id)
        {
            var getdata = await (from r in db.Reviews.Where(options => options.Idproducto == id)
                           select new { r.Mensaje, r.Fecha, r.Valoracion, r.Idproducto }).ToListAsync();
            return Json(getdata);
        }

        [HttpPost("postAddReviews")]
        public async Task<IActionResult> postAddReviews([FromBody] Review reviews)
        {
            reviews.Fecha = DateTime.Now;
            await db.Reviews.AddAsync(reviews);
            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("getAddReviews")]
        public async Task<IActionResult> getAddReviews([FromBody] Review reviews)
        {
            await db.Reviews.AddAsync(reviews);
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}
