public interface ISavable<T> : ISavable
{
      
}

public interface ISavable
{
    string GetKey { get; }
    void AddToRegister(); //dictionary.add,
    void RemoveFromRegister(); //dictionary.remove
    void ForceSave(); // write to file execute Save in the SaveManager
    void SetDefaultValues();
    void Load();
}
