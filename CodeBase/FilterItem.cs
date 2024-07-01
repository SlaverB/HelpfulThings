public class FilterTypeItem : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _filterText;
    [SerializeField] private GameObject _selectedIndicator;
    [SerializeField] private GameObject _unselectedIndicator;
    [SerializeField] private Button _selectButton;

    [Inject] private SignalBus _signalBus;
    
    private Enum _characterType;

    private void Start()
    {
      _signalBus.Subscribe<ResetAllFilters>(ResetSelection);
      _selectButton.onClick.AddListener(OnSelectButtonClicked);
    }

    private void ResetSelection()
    {
      ToggleSelection(false);
    }

    private void OnDestroy()
    {

      _selectButton.onClick.RemoveAllListeners();
    }

    private void OnSelectButtonClicked()
    {
      ToggleSelection(_unselectedIndicator.activeSelf);
    }

    public void SetFilterText(string text, Enum characterType)
    {
      _filterText.text = text;
      _characterType = characterType;
    }

    public void ToggleSelection(bool isSelected)
    {
      _selectedIndicator.SetActive(isSelected);
      _unselectedIndicator.SetActive(!isSelected);
      
      _signalBus.Fire(new SetCharacterFilter() {Filters = _characterType});
    }
  }

  public class FilterItemFactory : PlaceholderFactory<FilterTypeItem>
    {
      
    }