using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BHopClassLib;
using BHopsREST.Managers;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace BHopsREST.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class HopsController : Controller
    {
        // Uden anvendelse af database
        //private readonly HopsManager _manager = new HopsManager();

        private readonly HopsManager _manager;

        public HopsController(HopsContext context)
        {
            _manager = new HopsManager(context);
        }

        private int? Convert(string str)
        {
            if (str == null) return null;
            try { return int.Parse(str); }
            catch (FormatException) { return null; }
        }

        // GET: api/<HopsController>
        // Filtrer efter max pris fx: /api/hops?maxprice=69
        [HttpGet]
        [ProducesResponseType(Status200OK)]
        public IEnumerable<Hop> Get([FromQuery] string sortBy, [FromQuery] string maxPrice)
        {
            int? maxPriceInt = Convert(maxPrice);
            IEnumerable<Hop> hops = _manager.GetAll(sortBy, maxPriceInt);
            return hops;
        }

        // GET: api/<HopsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(Status200OK)]
        [ProducesResponseType(Status404NotFound)]
        public ActionResult<Hop> Get(int id)
        {
            Hop hop = _manager.GetById(id);
            if (hop == null) return NotFound("No hop id: " + id);
            return Ok(hop);
        }

        // POST
        [HttpPost]
        [ProducesResponseType(Status201Created)]
        [ProducesResponseType(Status400BadRequest)]
        public ActionResult<Hop> Add([FromBody] Hop hop)
        {
            Hop newHop = _manager.Add(hop);
            string uri = Url.RouteUrl(RouteData.Values) + "/" + newHop.Id;
            return Created(uri, hop);
        }

        // DELETE
        [HttpDelete("{id}")]
        [ProducesResponseType(Status200OK)]
        [ProducesResponseType(Status404NotFound)]
        public ActionResult<Hop> Delete(int id)
        {
            Hop hop = _manager.Delete(id);
            if (hop == null) return NotFound("No hop id: " + id);
            return Ok(hop);
        }

        // UPDATE
        [HttpPut("{id}")]
        [ProducesResponseType(Status200OK)]
        [ProducesResponseType(Status404NotFound)]
        public ActionResult<Hop> Update(int id, Hop updates)
        {
            Hop hop = _manager.Update(id, updates);
            if (hop == null) return NotFound("No hop id: " + id);
            return Ok(hop);
        }
    }
}
