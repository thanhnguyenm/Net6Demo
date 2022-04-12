using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyPattern
{
    internal class SortNumber
    {
        private readonly ISortStrategy sortStrategy;

        public SortNumber(ISortStrategy sortStrategy)
        {
            this.sortStrategy = sortStrategy;
        }

        public void Sort(int[] array)
        {
            sortStrategy.Sort(array);
        }
    }
}
