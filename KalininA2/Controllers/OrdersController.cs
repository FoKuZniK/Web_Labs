using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace KalininA2.Controllers
{
    [Route("api/warehouses/{warehouseId}/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public OrdersController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetOrderForWarehouse(Guid warehouseId)
        {
            var warehouse = _repository.Warehouse.GetWarehouse(warehouseId, trackChanges: false);
            if (warehouse == null)
            {
                _logger.LogInfo($"Warehouse with id: {warehouseId} doesn't exist in the database.");
                return NotFound();
            }

            var orderFromDb = _repository.Order.GetOrders(warehouseId, trackChanges: false);
            var orderDto = _mapper.Map<IEnumerable<OrderDto>>(orderFromDb);
            return Ok(orderDto);

        }

        [HttpGet("{id}", Name = "GetOrderForWarehouse")]
        public IActionResult GetOrderForWarehouse(Guid warehouseId, Guid id)
        {
            var warehouse = _repository.Warehouse.GetWarehouse(warehouseId, trackChanges: false);
            if (warehouse == null)
            {
                _logger.LogInfo($"Warehouse with id: {warehouseId} doesn't exist in the database.");
                return NotFound();
            }
            var orderDb = _repository.Order.GetOrder(warehouseId, id, trackChanges: false);
            if (orderDb == null)
            {
                _logger.LogInfo($"Order with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var order = _mapper.Map<OrderDto>(orderDb);
            return Ok(order);
        }
        [HttpPost]
        public IActionResult CreateOrderForWarehouse(Guid warehouseId, [FromBody]
        OrderForCreationDto order)
        {
            if (order == null)
            {
                _logger.LogError("OrderForCreationDto object sent from client is null.");
                return BadRequest("OrderForCreationDto object is null");
            }
            var warehouse = _repository.Warehouse.GetWarehouse(warehouseId, trackChanges: false);
            if (warehouse == null)
            {
                _logger.LogInfo($"Warehouse with id: {warehouseId} doesn't exist in the database.");
                return NotFound();
            }
            var orderEntity = _mapper.Map<Order>(order);
            _repository.Order.CreateOrderForWarehouse(warehouseId, orderEntity);
            _repository.Save();
            var orderToReturn = _mapper.Map<OrderDto>(orderEntity);
            return CreatedAtRoute("GetOrderForWarehouse", new
            {
                warehouseId,
                id = orderToReturn.Id
            }, orderToReturn);
        }
    }
}
