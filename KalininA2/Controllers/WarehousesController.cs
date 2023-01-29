using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using KalininA2.ModelBinders;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Web.Http.ModelBinding.Binders;

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
        [HttpGet("{id}", Name = "WarehouseById")]
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

        [HttpGet("collection/({ids})", Name = "WarehouseCollection")]
        public IActionResult GetWarehouseCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null");
                return BadRequest("Parameter ids is null");
            }
            var warehouseEntities = _repository.Warehouse.GetByIds(ids, trackChanges: false);
            if (ids.Count() != warehouseEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }
            var warehouseToReturn =
           _mapper.Map<IEnumerable<WarehouseDto>>(warehouseEntities);
            return Ok(warehouseToReturn);
        }

        [HttpPost("collection")]
        public IActionResult CreateWarehouseCollection([FromBody]
        IEnumerable<WarehouseForCreationDto> warehouseCollection)
        {
            if (warehouseCollection == null)
            {
                _logger.LogError("Warehouse collection sent from client is null.");
                return BadRequest("Warehouse collection is null");
            }
            var warehouseEntities = _mapper.Map<IEnumerable<Warehouse>>(warehouseCollection);
            foreach (var warehouse in warehouseEntities)
            {
                _repository.Warehouse.CreateWarehouse(warehouse);
            }
            _repository.Save();
            var warehouseCollectionToReturn =
            _mapper.Map<IEnumerable<WarehouseDto>>(warehouseEntities);
            var ids = string.Join(",", warehouseCollectionToReturn.Select(c => c.Id));
            return CreatedAtRoute("WarehouseCollection", new { ids },
            warehouseCollectionToReturn);
        }
    }
}
