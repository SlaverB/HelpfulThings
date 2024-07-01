using System;

namespace CodeBase.DiceRoll
{
  [Serializable]
  public partial struct Dice
  {
    public int count;
    public int sides;
    public int bonus;
    
    //d20
    public Dice(int sides)
    {
      this.count = 1;
      this.sides = sides;
      this.bonus = 0;
    }
    
    //4d6
    public Dice(int count, int sides)
    {
      this.count = count;
      this.sides = sides;
      this.bonus = 0;
    }
    
    //2d10+4
    public Dice(int count, int sides, int bonus)
    {
      this.count = count;
      this.sides = sides;
      this.bonus = bonus;
    }
    
    public static readonly Dice D6 = new Dice(6);
    public static readonly Dice D20 = new Dice(20);
  }
}