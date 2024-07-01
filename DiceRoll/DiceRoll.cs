using CodeBase.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.DiceRoll
{
    public class DiceRoll : MonoBehaviour
    {
        private const int DiceRollPrice = 0;
        private const int SidesNumber = 20;

        [SerializeField] private Animation _rollAnimation;
        [SerializeField] private Button _rollDiceButton;

        private Dice _dice;
        private DiceRollSystem _diceRollSystem;

        [Inject] private SignalBus _signalBus;
        [Inject] private DiceController _diceController;

        private void OnEnable()
        {
            _rollDiceButton.onClick.AddListener(RollDice);
            _diceController.ExecuteDiceRoll += ExecuteDiceRoll;
        }

        private void OnDisable()
        {
            _rollDiceButton.onClick.RemoveListener(RollDice);
            _diceController.ExecuteDiceRoll -= ExecuteDiceRoll;
        }

        public void RollDice()
        {
            _diceController.OnTryDiceRolled(DiceRollPrice);
        }

        private void ExecuteDiceRoll()
        {
            OnRollButtonClick();
        }

        private void OnRollButtonClick()
        {
            InitDice();

            int result = _diceRollSystem.Roll(_dice);
            _rollAnimation.Play("RotateTo" + result);

            NotifyListeners(result);

        }

        private void NotifyListeners(int result)
        {
            _signalBus.Fire(new DiceRolledSignal { DiceRolled = result });
        }

        private void InitDice()
        {
            _dice = new Dice(SidesNumber);
            _diceRollSystem = new DiceRollSystem();
        }   
    }
}