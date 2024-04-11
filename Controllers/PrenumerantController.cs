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
        [HttpPost("PostPrenumerant", Name = "PostPrenumerant")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public string Post([FromBody] PrenumerantDetalj pd)
        {
            PrenumerantMetoder pm = new PrenumerantMetoder();
            string errormsg = "";
            int i = pm.PostPrenumerant(pd, out errormsg);
            if (errormsg == null)
            {
                return i.ToString();
            }
            else return errormsg;
        }

        // PUT api/<PrenumerantController>/5
        [HttpPut("EditPrenumerant", Name = "EditPrenumerant")]
        public IActionResult EditPrenumerant(int prenumerationsnummer, [FromBody] PrenumerantDetalj updatedPrenumerant)
        {
            if (updatedPrenumerant == null || updatedPrenumerant.pr_prenumerationsnummer != prenumerationsnummer)
            {
                return BadRequest("Invalid prenumerant data");
            }

            PrenumerantMetoder pm = new PrenumerantMetoder();
            string error = "";
            PrenumerantDetalj editedPrenumerant = pm.EditPrenumerant(updatedPrenumerant, prenumerationsnummer, out error);

            if (editedPrenumerant == null)
            {
                return NotFound("Prenumerant not found");
            }

            return Ok(editedPrenumerant);
        }

        // DELETE api/<PrenumerantController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
