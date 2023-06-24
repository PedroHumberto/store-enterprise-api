using Core.APP.Data;
using Core.APP.DomainObjects;
using System;

namespace Catalog.API.Models
{
    public class Product : Entity, IAggregateRoot
    {
        public string Name {get; set;}
        public string Description { get; set; }
        public bool Actvie { get; set; }
        public decimal Price { get; set; }
        public DateTime RegisterDate { get; set; }
        public string Image { get; set; }
        public int InventoryQuantity { get; set; }
    }
}
