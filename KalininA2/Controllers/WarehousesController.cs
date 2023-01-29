using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace KalininA2.Controllers
{
    [Route("api/warehouses")]
    [ApiController]
    public class WarehousesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public WarehousesController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetWarehouse()
        {
            var warehouse = _repository.Warehouse.GetAllWarehouse(trackChanges: false);
            var warehouseDto = _mapper.Map<IEnumerable<WarehouseDto>>(warehouse);
            return Ok(warehouseDto);
        }
        [HttpGet("{id}")]
        public IActionResult GetWarehouse(Guid id)
        {
            var warehouse = _repository.Warehouse.GetWarehouse(id, trackChanges: false);
            if (warehouse == null)
            {
                _logger.LogInfo($"Warehouse with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var warehouseDto = _mapper.Map<WarehouseDto>(warehouse);
                return Ok(warehouseDto);
            }
        }
    }
}
