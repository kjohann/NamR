using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NamR.Server.Data;
using NamR.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NamR.Server.Controllers
{
    [Route("api/list")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly ListContext _context;

        public ListController(
            ListContext context    
        )
        {
            _context = context;
        }

        [HttpGet("{listId:Guid}")]
        public ActionResult<IEnumerable<ListItemModel>> Get([FromRoute]Guid listId)
        {
            var items = _context.GetList(listId).ToList();
            return items;
        }

        [HttpPost("item")]
        public async Task<ActionResult<ListItemModel>> Add([FromBody]NewListItemModel item)
        {
            var res = await _context.Add(item);
            return res;
        }

        [HttpDelete("{listId:Guid}/item/{listItemId:Guid}")]
        public async Task<ActionResult> Remove(Guid listId, Guid listItemId)
        {
            await _context.Remove(listId, listItemId);
            return NoContent();
        }
    }
}
