using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMMiniProject.Dtos.ProductDtos
{
    public class AddProductDto
    {
        public string Name { get; set; } = null!;

        public decimal Price { get; set; }
    }
}
