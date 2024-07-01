using CodeBase.Infrastructure.Dependency;

namespace CodeBase.DiceRoll
{
  public interface IRandomNumberGenerator : IDependency<IRandomNumberGenerator>
  {
    public int Range(int minInclusive, int maxExclusive);
  }
}