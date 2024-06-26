public class StateMachine : IInitializable, ILateDisposable
    {
        private Dictionary<Type, IState> _states;
        private IState _activeState;

        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly LevelModel _levelModel;
        [Inject] private readonly SceneLoader _sceneLoader;

        public GameStateMachine(BootstrapInitialState.Factory initial,
            BootstrapStateWorld.Factory boot,
            SwitchSceneState.Factory loadLevel,
            GameLoopState.Factory loop)
        {
            _states = new Dictionary<Type, IState>
            {
                [typeof(BootstrapInitialState)] = initial.Create(this),
                [typeof(BootstrapStateWorld)] = boot.Create(this),
                [typeof(SwitchSceneState)] = loadLevel.Create(this),
                [typeof(GameLoopState)] = loop.Create(this)
            };
        }

        public void Initialize()
        {
            _signalBus.Subscribe<UploadSceneSignal>(LoadLevel);
            Enter<BootstrapInitialState>();
        }

        public void LateDispose()
        {
            _signalBus.Unsubscribe<UploadSceneSignal>(LoadLevel);
        }

        public void Enter<TState>() where TState : class, IState
        {
            _activeState?.Exit();
            _activeState = GetState<TState>();
            _activeState.Enter();
        }

        public TState GetState<TState>() where TState : class, IState
        {
            return _states[typeof(TState)] as TState;
        }

        private void LoadLevel(UploadSceneSignal scene)
        {
            if (_sceneLoader.IsCurrentScene(scene.SceneType))
            {
                _sceneLoader.ResetWindows();
                return;
            }

            _levelModel.SetLevel(scene.SceneType);

            else if (scene.SceneType == SceneType.Initial)
            {
                Enter<BootstrapInitialState>();
            }
        }
