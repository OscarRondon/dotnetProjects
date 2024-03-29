﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceShared
{
    public class CartProductResponse
    {
        public int ProductId { get; set; }
        public string Tittle { get; set; } = string.Empty;
        public int ProductTypeId { get; set; }
        public string ProductType { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0m;
        public int Quantity { get; set; }
        public List<Image>? Images { get; set; } = null;
    }
}
