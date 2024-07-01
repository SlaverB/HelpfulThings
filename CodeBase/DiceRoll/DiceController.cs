using CodeBase.Karma;
using CodeBase.Player;
using System;
using Zenject;

namespace CodeBase.DiceRoll
{
    public class DiceController
    {
        public event Action ExecuteDiceRoll;

        [Inject] private SignalBus _signalBus;

        public void OnTryDiceRolled(int price)
        {
            if (_playerWorldController.IsFrozen)
                return;

            if (_karmaController.CanSpend(price))
            {
                _karmaController.SpendKarma(price);
                ExecuteDiceRoll?.Invoke();               
            }
        }
    }
}
