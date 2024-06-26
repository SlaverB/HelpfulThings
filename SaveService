
    public class SaveService : IInitializable, ILateDisposable
    {
        private List<ISavable> _registerOfAllSavables = new List<ISavable>();
        private Dictionary<string, object> _cashedDataForSave = new Dictionary<string, object>();

        private string _persistentPathFile;
        private string _persistentPathDirectory;
        private string _playerID = string.Empty;

        private const string SaveFolderName = "";

        [Inject] private SignalBus _signalBus;

        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            Formatting = Formatting.Indented // For better readability of the saved JSON
        };

        public void Initialize()
        {           
            _signalBus.Subscribe<AuthorizatedSignal>(LoadFromFile);
        }

        public void LateDispose()
        {
            _signalBus.TryUnsubscribe<AuthorizatedSignal>(LoadFromFile);
            SaveAllRegisteredData();
        }

        public void SaveDataDirectly<T>(string key, T data)
        {
            if (_cashedDataForSave.ContainsKey(key))
            {
                _cashedDataForSave[key] = data;
            }
            else
            {
                _cashedDataForSave.Add(key, data);
            }
            SaveToFile();
        }

        public void AddToRegister<T>(ISavable register, ref T data)
        {
            if (!_registerOfAllSavables.Contains(register))
            {
                _registerOfAllSavables.Add(register);
            }
            if (_cashedDataForSave.ContainsKey(register.GetKey))
            {
                data = (T)_cashedDataForSave[register.GetKey];
                if (data == null)
                    register.SetDefaultValues();
            }
            else
            {
                register.SetDefaultValues();
            }
        }

        public void LoadData<T>(ISavable savable, ref T data)
        {
            if (_cashedDataForSave.ContainsKey(savable.GetKey))
            {
                data = (T)_cashedDataForSave[savable.GetKey];
            }
            else
            {
                savable.SetDefaultValues();
            }
        }

        public void RemoveFromRegister(ISavable register)
        {
            SaveAllRegisteredData();
            _registerOfAllSavables.Remove(register);
        }

        public void ClearSavesForUser()
        {
            foreach (var item in _registerOfAllSavables)
            {
                item.SetDefaultValues();
            }

            _cashedDataForSave.Clear();

            if (string.IsNullOrEmpty(_persistentPathDirectory))
                SetPath();

            DeleteFolder(_persistentPathDirectory);
            DeleteFolder(Path.Combine(Application.persistentDataPath, RenderPath));
            DeleteFolder(Path.Combine(Application.persistentDataPath, ArtToyTexturePath));
        }

        private void SaveAllRegisteredData()
        {
            foreach (var register in _registerOfAllSavables)
            {
                register.ForceSave();
            }
        }

        private void SaveToFile()
        {
            if (string.IsNullOrEmpty(_playerID))
                return;

            string data = JsonConvert.SerializeObject(_cashedDataForSave, _settings);
            if (string.IsNullOrEmpty(_persistentPathDirectory))
                SetPath();
            if (!Directory.Exists(_persistentPathDirectory))
            {
                Directory.CreateDirectory(_persistentPathDirectory);
            }
            File.WriteAllText(_persistentPathFile, data);
        }      

        private void LoadFromFile(AuthorizatedSignal authorizated)
        {
            _playerID = authorizated.PlayerId;
            SetPath();
            if (!File.Exists(_persistentPathFile)) 
            {
                _cashedDataForSave = new Dictionary<string, object>();
                UpdateSaveData();
                return;
            }        

            using StreamReader reader = new StreamReader(_persistentPathFile);
            string json = reader.ReadToEnd();
            try
            {
                _cashedDataForSave = JsonConvert.DeserializeObject<Dictionary<string, object>>(json, _settings);
                UpdateSaveData();
            }
            catch (Exception e)
            {
                Debug.LogError($"Error to DeserializeObject: {e.Message}");
                reader.Close();
                ClearSavesForUser();
            }
        }

        private void UpdateSaveData()
        {
            foreach (var item in _registerOfAllSavables)
            {
                item.Load();
            }
        }

        private void SetPath()
        {
            _persistentPathDirectory = Path.Combine(Application.persistentDataPath, SaveFolderName);
            _persistentPathFile = Path.Combine(_persistentPathDirectory, _playerID+".json");
        }

        private static void DeleteFolder(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }  
    }
}
