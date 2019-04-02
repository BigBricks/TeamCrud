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
        [System.Web.Http.HttpPost]
        public IActionResult Create(TeamItem item)
        {
            Guid g;
            g = Guid.NewGuid();
            var name = item.name;
            var nameFC = _context.TeamItems.Where(e => e.name == name).ToList();
            var location = item.location;
            var locationFC = _context.TeamItems.Where(e => e.location == location).ToList();
            var wht = _context.TeamItems.Where(e => e.location == location && e.name == name).ToList();
            if (item.players != null)
            {
                if (item.players.Count >= 24)
                {
                    var reasonPhrase = "Too Many Players";
                    var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                      Content = new StringContent(string.Format("Over 25 players", item.players)),
                    ReasonPhrase = "Too Many Players"
                    };
                    //throw new HttpResponseException(resp);
                    return BadRequest(reasonPhrase);
                }
            }
                else if (nameFC.Count > 0)
                {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Cannot be the Same Name and Location as another Team"),
                    ReasonPhrase = "Name and Location cannot be the same for multiple teams"
                };
                //throw new HttpResponseException(resp);
                Console.WriteLine(nameFC);
                return BadRequest();
            }
                item.id = g;
                _context.TeamItems.Add(item);
                _context.SaveChanges();
                return Ok(nameFC);

        }

        // PUT api/<controller>/5
        [Microsoft.AspNetCore.Mvc.HttpPut("{id}")]
        public IActionResult Update(Guid id, TeamItem item)
        {
            Guid g;
            g = Guid.NewGuid();
            var name = item.name;
            var nameFC = _context.TeamItems.Where(e => e.name == name).ToList();
            var location = item.location;
            var locationFC = _context.TeamItems.Where(e => e.location == location).ToList();
            var wht = _context.TeamItems.Where(e => e.location == location && e.name == name).ToList();
            if (item.players != null)
            {
                if (item.players.Count >= 24)
                {
                    var reasonPhrase = "Too Many Players";
                    var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                    {
                        Content = new StringContent(string.Format("Over 25 players", item.players)),
                        ReasonPhrase = "Too Many Players"
                    };
                    return BadRequest(reasonPhrase);
                }
            }
            else if (nameFC.Count > 0)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Cannot be the Same Name and Location as another Team"),
                    ReasonPhrase = "Name and Location cannot be the same for multiple teams"
                };
                return BadRequest();
            }
            item.id = g;
            _context.TeamItems.Add(item);
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
