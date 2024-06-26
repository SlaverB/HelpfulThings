public abstract class SaveModel<T> : ISavable<T>, IInitializable, ILateDisposable
    {
        public string GetKey => GetType().Name;
        protected T _saveData;
        [Inject] protected SaveService _saveService;

        public abstract void SetDefaultValues();

        public virtual void AddToRegister()
        {
            _saveService.AddToRegister(this, ref _saveData);
        }

        public void RemoveFromRegister()
        {
            _saveService.RemoveFromRegister(this);
        }

        public void Load()
        {
            _saveService.LoadData(this, ref _saveData);
        }

        public void ForceSave()
        {
            _saveService.SaveDataDirectly(GetKey, _saveData);
        }

        //will be used for subscribe and unsubscribe
        public virtual void Initialize()
        {
            AddToRegister();
        }

        public virtual void LateDispose()
        {
            RemoveFromRegister();
        }
    }
