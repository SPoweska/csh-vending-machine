using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    /// <summary>
    /// Calculator for user change
    /// </summary>
    static class ChangeCalculator
    {
        /// <summary>
        /// Calculate user change
        /// </summary>
        /// <param name="money">user money</param>
        /// <param name="cost">product cost</param>
        /// <returns>change</returns>
        public static float CalculateChange(float money, float cost)
        {
            return money - cost;
        }
    }
}
