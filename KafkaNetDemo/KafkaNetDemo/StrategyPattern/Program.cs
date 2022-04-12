// See https://aka.ms/new-console-template for more information
using StrategyPattern;

int[] a = { 1, 2, 3, 4, 5, 6, 7 };

SortNumber sortAlg = new(new QuickSortStrategy());
sortAlg.Sort(a);

Console.WriteLine("-----------------------");

sortAlg = new(new BubbleSortStrategy());
sortAlg.Sort(a);
Console.ReadLine();