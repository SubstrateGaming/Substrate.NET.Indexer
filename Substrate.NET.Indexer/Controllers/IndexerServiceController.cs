using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Substrate.NET.Indexer.Data;
using Substrate.NET.Indexer.Model;

namespace Substrate.NET.Indexer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IndexerServiceController : ControllerBase
    {
        private readonly IndexerContext _context;

        private readonly ILogger<IndexerServiceController> _logger;

        public IndexerServiceController(IndexerContext context, ILogger<IndexerServiceController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("Block/{blockNumber}", Name = "Block")]
        public ActionResult<DbBlock> Block(int blockNumber)
        {
            var block = _context.Blocks
                                 .FirstOrDefault(s => s.BlockNumber == blockNumber);

            if (block == null)
            {
                return NotFound("No block found!");
            }

            return block;
        }

        [HttpGet("Events/{blockNumber}", Name = "Events")]
        public ActionResult<IEnumerable<DbEvent>> Events(int blockNumber)
        {
            var block = _context.Blocks
                .Include(b => b.Events)
                                 .FirstOrDefault(s => s.BlockNumber == blockNumber);

            if (block?.Events == null)
            {
                return NotFound("No block or event found!");
            }

            return block.Events.ToList();
        }
    }
}