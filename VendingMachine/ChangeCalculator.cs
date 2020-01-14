using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    static class ChangeCalculator
    {
        public static float CalculateChange(float money, float cost)
        {
            return money - cost;
        }
    }
}
