using UnityEngine;

namespace CodeBase.DiceRoll
{
  static internal class Utills
  {
    public static int GetRandomNumberWithStep (int min, int max, int stepSize) {
      int random = Random.Range(min, max);
      float numSteps = Mathf.Floor (random / stepSize);
      int adjustedNumber = (int)numSteps * stepSize;
 
      return adjustedNumber;
    }
  }
}