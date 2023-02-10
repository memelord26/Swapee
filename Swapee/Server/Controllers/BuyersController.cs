using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swapee.Server.Data;
using Swapee.Server.IRepository;
using Swapee.Server.Repository;
using Swapee.Shared.Domain;

namespace Swapee.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyersController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public BuyersController(ApplicationDbContext context)
        public BuyersController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Buyers
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Buyer>>> GetBuyers()
        public async Task<ActionResult> GetBuyers()
        {
            //return await _context.Buyers.ToListAsync();
            var buyers = await _unitOfWork.Buyers.GetAll();
            return Ok(buyers);
        }

        // GET: api/Buyers/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Buyer>> GetBuyer(int id)
        public async Task<ActionResult> GetBuyer(int id)
        {
            //var buyer = await _context.Buyers.FindAsync(id);
            var buyer = await _unitOfWork.Buyers.Get(q => q.Id == id);

            if (buyer == null)
            {
                return NotFound();
            }

            //return buyer;
            return Ok(buyer);
        }

        // PUT: api/Buyers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuyer(int id, Buyer buyer)
        {
            if (id != buyer.Id)
            {
                return BadRequest();
            }

            //_context.Entry(buyer).State = EntityState.Modified;
            _unitOfWork.Buyers.Update(buyer);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!BuyerExists(id))
                if (!await BuyerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Buyers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Buyer>> PostBuyer(Buyer buyer)
        {
            //_context.Buyers.Add(buyer);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Buyers.Insert(buyer);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetBuyer", new { id = buyer.Id }, buyer);
        }

        // DELETE: api/Buyers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuyer(int id)
        {
            //var buyer = await _context.Buyers.FindAsync(id);
            var buyer = await _unitOfWork.Buyers.Get(q => q.Id == id);
            if (buyer == null)
            {
                return NotFound();
            }

            //_context.Buyers.Remove(buyer);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Buyers.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool BuyerExists(int id)
        private async Task<bool> BuyerExists(int id)
        {
            //return _context.Buyers.Any(e => e.Id == id);
            var buyer = await _unitOfWork.Buyers.Get(q => q.Id == id);
            return buyer != null;
        }
    }
}
