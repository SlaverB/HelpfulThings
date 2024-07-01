using UnityEngine;

namespace CodeBase.DiceRoll
{
  public struct RandomNumberGenerator : IRandomNumberGenerator
  {
    public int Range(int minInclusive, int maxExclusive)
    {
      return Random.Range(minInclusive, maxExclusive);
    }
  }
}