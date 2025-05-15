namespace Knapsack;
public class Program
{
    public static void Main()
    {
        // int[] weights = {3, 1, 6, 4, 2, 5, 1, 7, 2, 4};
        // int[] values  = {7, 4, 9, 15, 13, 10, 2, 6, 12, 16};

        int[] weights = {3, 1, 6, 10, 1, 4, 9, 1, 7, 2, 6, 1, 6, 2, 2, 4, 8, 1, 7, 3, 6, 2, 9, 5, 3, 3, 4, 7, 3, 5, 30, 50};
        int[] values = {7, 4, 9, 18, 9, 15, 4, 2, 6, 13, 18, 12, 12, 16, 19, 19, 10, 16, 14, 3, 14, 4, 15, 7, 5, 10, 10, 13, 19, 9, 8, 5};
        var solver = new KnapsackSolver(weights, values, 75);
        
        var solution = solver.Solve();
        var maxVal = solver.CalculateValue(solution);
        Console.WriteLine(Convert.ToString(solution, 2));
        Console.WriteLine(maxVal);
    }
}