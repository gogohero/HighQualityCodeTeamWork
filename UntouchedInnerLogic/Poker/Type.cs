using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public class Type
    {
        public Type(double power, double current)
        {
            this.Power = power;
            this.Current = current;
        }

        public Type()
        {
        }

        public double Power { get; set; }

        public double Current { get; set; }
    }
}
