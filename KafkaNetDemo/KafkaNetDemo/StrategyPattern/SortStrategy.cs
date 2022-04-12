using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategyPattern;

internal interface ISortStrategy
{
    void Sort(int[] arr);
}

internal class BubbleSortStrategy : ISortStrategy
{
    public void Sort(int[] arr)
    {
        Console.WriteLine("Buble sort");
    }
}

internal class QuickSortStrategy : ISortStrategy
{
    public void Sort(int[] arr)
    {
        Console.WriteLine("Quick sort");
    }
}