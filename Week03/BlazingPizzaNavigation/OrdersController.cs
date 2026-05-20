using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazingPizza;

[Route("orders")]
[ApiController]
public class OrdersController : Controller
{
    private readonly PizzaStoreContext _db;

    public OrdersController(PizzaStoreContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<ActionResult<int>> PlaceOrder(Order order)
    {
        order.CreatedTime = DateTime.Now;

        foreach (var pizza in order.Pizzas)
        {
            pizza.Special = null;
        }

        _db.Orders.Attach(order);
        await _db.SaveChangesAsync();

        return order.OrderId;
    }

    [HttpGet]
    public async Task<ActionResult<List<Order>>> GetOrders()
    {
        return await _db.Orders
            .Include(o => o.Pizzas)
            .ThenInclude(p => p.Special)
            .ToListAsync();
    }

    [HttpGet("{orderId}")]
    public async Task<ActionResult<Order>> GetOrder(int orderId)
    {
        var order = await _db.Orders
            .Include(o => o.Pizzas)
            .ThenInclude(p => p.Special)
            .SingleOrDefaultAsync(o => o.OrderId == orderId);

        if (order == null)
        {
            return NotFound();
        }

        return order;
    }
}

