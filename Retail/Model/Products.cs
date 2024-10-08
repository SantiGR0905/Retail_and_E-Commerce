﻿using System.ComponentModel.DataAnnotations;

namespace Retail.Model
{
    public class Products
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public required string Description { get; set; }
        public required DateTime CreationDate { get; set; }
        public required int Active { get; set; }
        public required string Model3D { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual required Categories Categories { get; set; }
    }
}
