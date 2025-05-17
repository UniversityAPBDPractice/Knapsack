using System.Security.Cryptography;

namespace Knapsack;

public class KnapsackSolverSa : KnapsackSolver
{
    private long _current;
    private double _temperature;
    private readonly IProbabilityFunction _probabilityFunction;
    private static readonly Random _rand = new();
    private long Best;
    public KnapsackSolverSa(int[] weights, int[] values, int capacity)
    {
        Size = weights.Length;
        Weights = weights;
        Values = values;
        Capacity = capacity;
        _current = GetRandomBits(Size);
        while (CalculateWeight(_current) > Capacity)
        {
            _current = GetRandomBits(Size); // Avoid assigning putting too much due to randomness.
        }
        _probabilityFunction = new DefaultProbabilityFunction();
        _temperature = 100.0;
        Best = _current;
    }
    
    private static long GetRandomBits(int n)
    {
        if (n < 1 || n > 64)
            throw new ArgumentOutOfRangeException(nameof(n), "n must be between 1 and 64");

        byte[] bytes = new byte[8]; // 8 bytes = 64 bits
        RandomNumberGenerator.Fill(bytes);

        long value = BitConverter.ToInt64(bytes, 0);

        // Mask off extra bits to ensure only n bits are used
        long mask = (n == 64) ? -1L : (1L << n) - 1;
        return value & mask;
    }
    
    public new long Solve()
    {
        return Solve(10000);
    }

    private void LowerTemperature()
    {
        _temperature *= 0.998;
    }

    private long[] GenerateNeighbours(long knapsack)
    {
        long[] neighbours = new long[Size];
        for (int i = 0; i < Size; i++)
            neighbours[i] = FlipBit(knapsack, i);
        return neighbours;
    }

    private double CalculateWeight(long knapsack)
    {
        int weight = 0;
        for (int i = 0; i < Size; i++)
        {
            if (((knapsack >> i) & 1) == 1)
            {
                weight += Weights[i];
            }
        }

        return weight;
    }
    
    private long Solve(int iterations)
    {
        var i = 0;
        while (i < iterations)
        {
            long[] neighbours = GenerateNeighbours(_current);
            long randomNeighbour = neighbours[_rand.Next(neighbours.Length)];
            if (CalculateWeight(randomNeighbour) <= Capacity)
            {
                var curVal = CalculateValue(_current);
                var neiVal = CalculateValue(randomNeighbour);
                if (neiVal > curVal)
                    _current = randomNeighbour; 
                else
                {
                    bool toTake = _probabilityFunction.TakeOrNotToTake(_temperature, Math.Abs(curVal - neiVal));
                    if (toTake) _current = randomNeighbour;
                }
            }
            if (CalculateValue(_current) > CalculateValue(Best))
            {
                Best = _current;
            }
            LowerTemperature();
            i++;
            Console.WriteLine(i);
        }

        return Best;
    }
}