using Microsoft.AspNetCore.Mvc;
using Prenumerationssystem.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Prenumerationssystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrenumerantController : ControllerBase
    {
        // GET: api/<PrenumerantController>
        [HttpGet("getPrenumeranter", Name = "GetPrenumerantWithDataReader")]
        public List<PrenumerantDetalj> GetPrenumerantWithDataReader()
        {
            List<PrenumerantDetalj> prenumerantlista = new List<PrenumerantDetalj>();
            PrenumerantMetoder pm = new PrenumerantMetoder();
            string error = "";
            prenumerantlista = pm.GetPrenumerantWithDataReader(out error);
            return prenumerantlista;
        }

        // GET api/<PrenumerantController>/5
        [HttpGet("GetPrenumerantByPn", Name = "GetPrenumerantByPn")]
        public PrenumerantDetalj GetPrenumerantByPn(int prenumerationsnummer)
        {
            PrenumerantDetalj prenumerant = new PrenumerantDetalj();
            PrenumerantMetoder pm = new PrenumerantMetoder();
            prenumerant = pm.GetPrenumerantByPn(prenumerationsnummer, out string error);
            return (prenumerant);
        }

        // POST api/<PrenumerantController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PrenumerantController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PrenumerantController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
