using UnityEngine;

namespace CyberSeedSetter;

public sealed class SeededRandom(int seed)
{
    public int Seed { get; } = seed;
    private readonly System.Random random = new(seed);
    public SeededRandom() : this(Random.Range(int.MinValue, int.MaxValue)) { }
    public int Range(int minValue, int maxValue)
    {
        return random.Next(minValue, maxValue);
    }

    public float Range(float minValue, float maxValue)
    {
        return ((float)random.NextDouble() * (maxValue - minValue)) + minValue;
    }
}