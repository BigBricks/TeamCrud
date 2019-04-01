using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using TeamCrud.Models;
using System.Web.Http.Cors;

namespace TeamCrud.Controllers
{
    [EnableCorsAttribute("*", "*", "*")]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class PlayersController : Controller
    {
        private readonly TeamContext _context;
        public PlayersController(TeamContext context)
        {
            _context = context;
        }
        // GET api/values
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public ActionResult<List<PlayerItem>> GetAll()
        {
            return _context.PlayerItems.ToList();
        }

        // GET api/values/5
        [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
        public ActionResult<PlayerItem> Get(Guid id)
        {
            var person = _context.PlayerItems.Find(id);
            if (person == null)
            {
                return NotFound();
            }
            return person;
        }

        // POST api/values
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult Create(PlayerItem item)
        {
            Guid g;
            g = Guid.NewGuid();
            item.id = g;
            _context.PlayerItems.Add(item);
            _context.SaveChanges();
            return CreatedAtRoute("GetPlayerItem", item);
        }

        // PUT api/values/5
        [Microsoft.AspNetCore.Mvc.HttpPut("{id}")]
        public IActionResult Update(Guid id, PlayerItem item)
        {
            return NoContent();
        }

        // DELETE api/values/5
        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
