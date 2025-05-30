using bike_store_2.Data;
using bike_store_2.DTO;
using bike_store_2.Entities;
using bike_store_2.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace bike_store_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {

        private readonly IOrderRepository _orderRepository;
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }


        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto createOrder)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var order = await _orderRepository.CreateOrderAsync(createOrder);
                return Ok(new
                {
                    Massage = "Order created successfully.",
                    order = order,
                    orderitems = createOrder.Items.Select(i => new
                    {
                        i.ProductId,
                        i.Listprice,
                        i.Quantity,
                        i.Discount,
                    }).ToList()
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [Authorize(Roles = "User")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderDto orderDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var order = await _orderRepository.UpdateOrderAsync(id, orderDto);
                if (order != null)
                    return Ok(new
                    {
                        Massage = "Order updated successfully.",
                        order = order,
                        orderitems = orderDto.Items.Select(i => new
                        {
                            i.ProductId,
                            i.Listprice,
                            i.Quantity,
                            i.Discount,
                        }).ToList()
                    });

                return NotFound($"No orders found with this id {id}.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }





        [Authorize(Roles = "User")]
        [HttpGet("All Orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var orders = await _orderRepository.GetAllExistingOrdersAsync();
                if (orders == null || !orders.Any())
                    return NotFound("No orders found.");

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [Authorize(Roles = "User")]
        [HttpGet("All Deleted Orders")]
        public async Task<IActionResult> GetAllDeletedOrders()
        {
            try
            {
                var orders = await _orderRepository.GetAllDeletedOrdersAsync();
                if (orders == null || !orders.Any())
                    return NotFound("No orders found.");

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [Authorize(Roles = "User")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrderByID(int id)
        {
            try
            {
                var order = await _orderRepository.GetOrderByIdAsync(id);
                if (order != null)
                    return Ok(order);

                return NotFound($"No orders found with this id {id}.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [Authorize(Roles = "User")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _orderRepository.DeleteOrderAsync(id);                
                return Ok("Order deleted successfully.");
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }





        }
    }
}
