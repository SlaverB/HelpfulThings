public interface IState
    {
        void Exit();
        void Enter();
    }

    public abstract class AState : IState
    {

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }
    }