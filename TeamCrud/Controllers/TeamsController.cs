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
    public class TeamsController : ControllerBase
    {
        private readonly TeamContext _context;
        public TeamsController(TeamContext context)
        {
            _context = context;
        }
        // GET: api/<controller>
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public ActionResult<List<TeamItem>> GetAll()
        {
            return _context.TeamItems.ToList();
        }

        // GET api/<controller>/5
        [Microsoft.AspNetCore.Mvc.HttpGet("{id}")]
        public ActionResult<TeamItem> Get(Guid id)
        {
            var team = _context.TeamItems.Find(id);
            if(team == null)
            {
                return NotFound();
            }
            return team;
        }

        // POST api/<controller>
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public IActionResult Create(TeamItem item)
        {
            Guid g;
            g = Guid.NewGuid();
            var name = item.name;
            var nameFC = _context.TeamItems.Where(e => e.name == name);
            var location = item.location;
            var locationFC = _context.TeamItems.Where(e => e.location == location);
            if (item.players != null)
            {
                if (item.players.Count > 24)
                {
                    var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                      Content = new StringContent(string.Format("Over 25 players", item.players)),
                    ReasonPhrase = "Too Many Players"
                    };
                    //throw new HttpResponseException(resp);
                    return BadRequest();
                }
            }
                else if (nameFC == locationFC)
                {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Cannot be the Same Name and Location as another Team"),
                    ReasonPhrase = "Name and Location cannot be the same for multiple teams"
                };
                //throw new HttpResponseException(resp);
                return BadRequest();
            }
                item.id = g;
                _context.TeamItems.Add(item);
                _context.SaveChanges();
                return CreatedAtRoute("GetItem", new { id = g }, item);

        }

        // PUT api/<controller>/5
        [Microsoft.AspNetCore.Mvc.HttpPut("{id}")]
        public IActionResult Update(Guid id, TeamItem item)
        {
            var team = _context.TeamItems.Find(id);
            if (item.players.Count > 24)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("Over 25 players", item.players)),
                    ReasonPhrase = "Too Many Players"
                };
                throw new HttpResponseException(resp);
            }
            var name = item.name;
            var nameFC = _context.TeamItems.Find(name);
            if (nameFC != null)
            {
                var location = item.location;
                if (nameFC.location == location)
                {
                    if (nameFC.id != item.id)
                    {
                        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                        {
                            Content = new StringContent("Cannot be the Same Name and Location as another Team"),
                            ReasonPhrase = "Name and Location cannot be the same for multiple teams"
                        };
                        throw new HttpResponseException(resp);
                    }
                }
            }
            team.name = item.name;
            team.location = item.location;
            team.players = item.players;
            _context.TeamItems.Update(team);
            _context.SaveChanges();
            return NoContent();
        }

        // DELETE api/<controller>/5
        [Microsoft.AspNetCore.Mvc.HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
