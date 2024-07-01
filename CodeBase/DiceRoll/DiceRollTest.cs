using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CodeBase.DiceRoll
{
  public class DiceRollTest: MonoBehaviour
  {
    [FormerlySerializedAs("_diceRoll")] [SerializeField] private Dice dice;
    [SerializeField] private Animation _rollAnimation;
    
    public Button RollButton;
    
    private DiceRollSystem _diceRollSystem;
    
    private void Start()
    {
      RollButton.onClick.AddListener(OnRollButtonClick);
    }

    private void OnDestroy()
    {
      RollButton.onClick.RemoveListener(OnRollButtonClick);
    }

    private void OnRollButtonClick()
    {
      dice = new Dice(20);
      _diceRollSystem = new DiceRollSystem();
      int result = _diceRollSystem.Roll(dice);
      Debug.Log($"Rolled {dice} and got {result}");
      _rollAnimation.Play("Rotate"+result);
    }
  }
}