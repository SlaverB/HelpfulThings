using CodeBase.Infrastructure.Dependency;
using UnityEngine;

namespace CodeBase.DiceRoll
{
  public class DiceRollSystem : IDiceRollSystem
  {
    public int Roll(Dice dice)
    {
      int result = dice.bonus;
      for (int i = 0; i < dice.count; i++)
        result += Random.Range(1, dice.sides + 1);;
      return result;
    }
  }
  
  public interface IDiceRollSystem : IDependency<IDiceRollSystem>
  {
    int Roll(Dice dice);
  }
  
  public partial struct Dice
  {
    public int Roll()
    {
      return IDiceRollSystem.Resolve().Roll(this);
    }
  }
}