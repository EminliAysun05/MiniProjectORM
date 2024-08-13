using ORMMiniProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMMiniProject.Dtos.UserDtos
{
    public class OrderDtoForExcel
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

    }
}
