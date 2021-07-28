using Microsoft.AspNetCore.Mvc;
using NamR.Server.Data;
using NamR.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NamR.Server.Controllers
{
    [Route("api/list/compare")]
    [ApiController]
    public class CompareController : ControllerBase
    {
        private readonly ListContext _context;

        public CompareController(
            ListContext context
        )
        {
            _context = context;
        }

        [HttpGet("{compareId:Guid}")]
        public ActionResult<IEnumerable<CompareListItemModel>> GetListForComparing([FromRoute] Guid compareId)
        {
            var items = _context.GetListForComparing(compareId).ToList();
            return items;
        }
    }
}
