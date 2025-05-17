namespace Knapsack;

public class DefaultProbabilityFunction : IProbabilityFunction
{
    public bool TakeOrNotToTake(double temperature, double delta)
    {
        float probability = 1f / (1f + MathF.Exp((float)(delta / temperature)));
        return probability > 0.5f;
    }
}