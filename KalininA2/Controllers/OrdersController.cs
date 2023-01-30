using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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
        [HttpDelete("{id}")]
        public IActionResult DeleteOrderForWarehouse(Guid warehouseId, Guid id)
        {
            var warehouse = _repository.Warehouse.GetWarehouse(warehouseId, trackChanges: false);
            if (warehouse == null)
            {
                _logger.LogInfo($"Warehouse with id: {warehouseId} doesn't exist in the database.");
                return NotFound();
            }
            var orderForWarehouse = _repository.Order.GetOrder(warehouseId, id,
            trackChanges: false);
            if (orderForWarehouse == null)
            {
                _logger.LogInfo($"Order with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _repository.Order.DeleteOrder(orderForWarehouse);
            _repository.Save();
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrderForWarehouse(Guid warehouseId, Guid id, [FromBody]
        OrderForUpdateDto order)
        {
            if (order == null)
            {
                _logger.LogError("OrderForUpdateDto object sent from client is null.");
                return BadRequest("OrderForUpdateDto object is null");
            }
            var warehouse = _repository.Warehouse.GetWarehouse(warehouseId, trackChanges: false);
            if (warehouse == null)
            {
                _logger.LogInfo($"Warehouse with id: {warehouseId} doesn't exist in the database.");
                return NotFound();
            }
            var orderEntity = _repository.Order.GetOrder(warehouseId, id, trackChanges:
            true);
            if (orderEntity == null)
            {
                _logger.LogInfo($"Order with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            _mapper.Map(order, orderEntity);
            _repository.Save();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateOrderForWarehouse(Guid warehouseId, Guid id,
        [FromBody] JsonPatchDocument<OrderForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }
            var warehouse = _repository.Warehouse.GetWarehouse(warehouseId, trackChanges: false);
            if (warehouse == null)
            {
                _logger.LogInfo($"Warehouse with id: {warehouseId} doesn't exist in the database.");
                return NotFound();
            }
            var orderEntity = _repository.Order.GetOrder(warehouseId, id, trackChanges:
            true);
            if (orderEntity == null)
            {
                _logger.LogInfo($"Order with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            var orderToPatch = _mapper.Map<OrderForUpdateDto>(orderEntity);
            patchDoc.ApplyTo(orderToPatch);
            _mapper.Map(orderToPatch, orderEntity);
            _repository.Save();
            return NoContent();
        }
    }
}
