using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TellDontAskKata.Main.Domain;

namespace TellDontAskKata.Tests;
internal static class OrderData
{
    public static Order Generate(OrderStatus status)
    {
        return new Order(status)
        {
            Id = 1
        };
    }

}
