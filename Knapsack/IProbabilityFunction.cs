namespace Knapsack;

public interface IProbabilityFunction
{
    bool TakeOrNotToTake(double temperature, double delta);
}