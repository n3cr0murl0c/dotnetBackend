using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.DTOS;

namespace Play.Catalog.Service.Controllers{

    [ApiController]//Atributo model validation errors, bind inc reqs
    [Route("items")]//route como en express app.route('/apiEndpoint',EndpointController)
    public class ItemsController:ControllerBase{
        // each API controller should inherit ControllerBase
        //reaonly para que solo se muestre en construccion
        private static readonly List<ItemDto> items = [
            new ItemDto(Guid.NewGuid(),"Potion","Restores a small amount of HP",5, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(),"Antidote","Cures poison",5, DateTimeOffset.UtcNow),
            new ItemDto(Guid.NewGuid(),"Poison","Poison the enemy to reduce 1% of HP each minute ",5, DateTimeOffset.UtcNow),

        ];

        [HttpGet]
        public IEnumerable<ItemDto> Get(){
            return items;
        }

        // Get /items/{id}
        [HttpGet("{id}")]
        public ItemDto GetById(Guid Id){
            var item = items.Where(item=>item.Id==Id).SingleOrDefault();
            return item;
        }
        

        [HttpPost]
        public ActionResult<ItemDto> Post(CreateItemDto createItemDto){
            //Action Result Type allows return a type once of the several HTTP status codes or a DTO type
            var item = new ItemDto(Guid.NewGuid(),createItemDto.Name, createItemDto.Description, createItemDto.Price,DateTimeOffset.UtcNow);
            
            items.Add(item);

            return CreatedAtAction(nameof(GetById),new{id=item.Id},item);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, UpdateItemDto updateItem){
        //IAACtionResult no retorna nada ALSO PUT requiere todos los campos
        var existingItem = items.Where(item=>item.Id==id).SingleOrDefault();

        var updatedItem= existingItem with {
            Name=updateItem.Name,
            Description=updateItem.Description,
            Price=updateItem.Price
        };
        var index = items.FindIndex(existingItem=>existingItem.Id==id);

        items[index]=updatedItem;
        return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id){
            var index = items.FindIndex(existingItem=>existingItem.Id==id);
            items.RemoveAt(index);
            return NoContent();
        }
    }
}
