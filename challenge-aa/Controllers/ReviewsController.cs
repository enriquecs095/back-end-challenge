using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using challenge_aa.Models;

namespace challenge_aa.Controllers
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
        public IActionResult postReviews(int id)
        {
            var getdata = (from r in db.Reviews.Where(options => options.Idproducto == id)
                           select new { r.Mensaje, r.Fecha, r.Valoracion, r.Idproducto }).ToList();
            return Json(getdata);
        }

        [HttpGet("getReviews{id}")]
        public IActionResult getReviews(int id)
        {
            var getdata = (from r in db.Reviews.Where(options => options.Idproducto == id)
                           select new { r.Mensaje, r.Fecha, r.Valoracion, r.Idproducto }).ToList();
            return Json(getdata);
        }

        [HttpPost("postAddReviews")]
        public IActionResult postAddReviews([FromBody] Review reviews)
        {
            reviews.Fecha = DateTime.Now;
            db.Reviews.Add(reviews);
            db.SaveChanges();
            return Ok();
        }

        [HttpGet("getAddReviews")]
        public IActionResult getAddReviews([FromBody] Review reviews)
        {
            db.Reviews.Add(reviews);
            db.SaveChanges();
            return Ok();
        }
    }
}
