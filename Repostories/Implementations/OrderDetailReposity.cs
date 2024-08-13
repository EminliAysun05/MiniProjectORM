using ORMMiniProject.Models;
using ORMMiniProject.Repostories.Implementations.Generic;
using ORMMiniProject.Repostories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMMiniProject.Repostories.Implementations
{
    public class OrderDetailReposity: Reposity<OrderDetail>,IOrderDetail
    {
    }
}
