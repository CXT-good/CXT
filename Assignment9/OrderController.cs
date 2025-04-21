using System;

namespace OrderWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        public readonly OrderDbContext orderDbContext;

        public OrderController(OrderDbContext orderDbContext)
        {
            this.orderDbContext = orderDbContext;
        }

        [HttpGet]
        public ActionResult<List<Order>> GetOrders(string? productName, string? customerName, float? amount)
        {
            IQueryable<Order> lst = orderDbContext.Orders;
            if (productName != null)
            {
                lst = lst.Where(order => order.Details.Exists(item => item.ProductName == productName));
            }
            if (customerName != null)
            {
                lst = lst.Where(order => order.CustomerName == customerName);
            }
            if (amount != null)
            {
                lst = lst.Where(order => order.TotalPrice >= amount);
            }
            return lst
        }

        [HttpGet("{id}")]
        public ActionResult<Order?> GetOrder(int id)
        {
            Order? order = orderDbContext.Orders.SingleOrDefault(o => o.OrderId == id);
            return order;
        }

        [HttpPost]
        public ActionResult<Order> PostOrder(Order order)
        {
            try
            {
                orderDbContext.Orders.Add(order);
                orderDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException is null ? "" : e.InnerException.Message);
            }
            return order;
        }

        [HttpPut("{id}")]
        public ActionResult<Order> PutOrder(int id, Order order)
        {
            if (id != order.OrderId) return BadRequest("Id cannot be modified");
            try
            {
                orderDbContext.Entry(order).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                orderDbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException is null ? "" : e.InnerException.Message);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteOrder(int id)
        {
            try
            {
                var toDelete = orderDbContext.Orders.FirstOrDefault(o => o.OrderId == id);
                if (toDelete != null)
                {
                    orderDbContext.Orders.Remove(toDelete);
                    orderDbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException is null ? "" : e.InnerException.Message);
            }
            return NoContent();
        }
    }

}
