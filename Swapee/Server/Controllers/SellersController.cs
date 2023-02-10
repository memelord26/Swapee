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
    public class SellersController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public SellersController(ApplicationDbContext context)
        public SellersController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Sellers
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Seller>>> GetSellers()
        public async Task<ActionResult> GetSellers()
        {
            //return await _context.Sellers.ToListAsync();
            var sellers = await _unitOfWork.Sellers.GetAll();
            return Ok(sellers);
        }

        // GET: api/Sellers/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Seller>> GetSeller(int id)
        public async Task<ActionResult> GetSeller(int id)
        {
            //var seller = await _context.Sellers.FindAsync(id);
            var seller = await _unitOfWork.Sellers.Get(q => q.Id == id);

            if (seller == null)
            {
                return NotFound();
            }

            //return seller;
            return Ok(seller);
        }

        // PUT: api/Sellers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeller(int id, Seller seller)
        {
            if (id != seller.Id)
            {
                return BadRequest();
            }

            //_context.Entry(seller).State = EntityState.Modified;
            _unitOfWork.Sellers.Update(seller);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!SellerExists(id))
                if (!await SellerExists(id))
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

        // POST: api/Sellers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Seller>> PostSeller(Seller seller)
        {
            //_context.Sellers.Add(seller);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Sellers.Insert(seller);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetSeller", new { id = seller.Id }, seller);
        }

        // DELETE: api/Sellers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeller(int id)
        {
            //var seller = await _context.Sellers.FindAsync(id);
            var seller = await _unitOfWork.Sellers.Get(q => q.Id == id);
            if (seller == null)
            {
                return NotFound();
            }

            //_context.Sellers.Remove(seller);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Sellers.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool SellerExists(int id)
        private async Task<bool> SellerExists(int id)
        {
            //return _context.Sellers.Any(e => e.Id == id);
            var seller = await _unitOfWork.Sellers.Get(q => q.Id == id);
            return seller != null;
        }
    }
}
