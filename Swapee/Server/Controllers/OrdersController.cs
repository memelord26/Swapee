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
    public class OrdersController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public OrdersController(ApplicationDbContext context)
        public OrdersController(IUnitOfWork unitOfWork)
        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Orders
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        public async Task<ActionResult> GetOrders()
        {
            //return await _context.Orders.ToListAsync();
            var orders = await _unitOfWork.Orders.GetAll(includes: q => q.Include(x => x.Buyer).Include(x => x.Product));
            return Ok(orders);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Order>> GetOrder(int id)
        public async Task<ActionResult> GetOrder(int id)
        {
            //var order = await _context.Orders.FindAsync(id);
            var order = await _unitOfWork.Orders.Get(q => q.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            //return order;
            return Ok(order);
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            //_context.Entry(order).State = EntityState.Modified;
            _unitOfWork.Orders.Update(order);

            try
            {
                //await _context.SaveChangesAsync();
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!OrderExists(id))
                if (!await OrderExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            //_context.Orders.Add(order);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Orders.Insert(order);
            await _unitOfWork.Save(HttpContext);

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            //var order = await _context.Orders.FindAsync(id);
            var order = await _unitOfWork.Orders.Get(q => q.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            //_context.Orders.Remove(order);
            //await _context.SaveChangesAsync();
            await _unitOfWork.Orders.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        //private bool OrderExists(int id)
        private async Task<bool> OrderExists(int id)
        {
            //return _context.Orders.Any(e => e.Id == id);
            var order = await _unitOfWork.Orders.Get(q => q.Id == id);
            return order != null;
        }
    }
}
