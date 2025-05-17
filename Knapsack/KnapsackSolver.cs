using System.Numerics;

namespace Knapsack;

public class KnapsackSolver
{
    protected int Size { get; set; }
    protected int[] Weights { get; set; }
    protected int[] Values { get; set; }
    protected int Capacity { get; set; }
    public KnapsackSolver(){}
    
    public KnapsackSolver(int[] weights, int[] values, int capacity)
    {
        Size = weights.Length;
        Weights = weights;
        Values = values;
        Capacity = capacity;
    }

    public long Solve()
    {
        return Solve(0, Capacity, 0b0);
    }
    
    public long SetBit(long number, int index, bool value)
    {
        if (value)
            return number | (1L << index);
        else
            return number & ~(1L << index);
    }

    public long FlipBit(long number, int index)
    {
        return number ^ (1L << index);
    }

    public int CalculateValue(long knapsack)
    {
        int value = 0;
        for (int i = 0; i < Size; i++)
        {
            if (((knapsack >> i) & 1) == 1)
            {
                value += Values[i];
            }
        }

        return value;
    }

    public long Solve(int n, int c, long knapsack)
    {
        if (n == Size || c == 0) return knapsack;
        int itemWeight = Weights[n];
        if (c < itemWeight)
        {
            return Solve(n + 1, c, knapsack);
        }
        
        long taken = Solve(n + 1, c - itemWeight, SetBit(knapsack, n, true));
        long skipped = Solve(n + 1, c, knapsack);

        if (CalculateValue(taken) > CalculateValue(skipped))
            return taken;
        return skipped;
    }
}