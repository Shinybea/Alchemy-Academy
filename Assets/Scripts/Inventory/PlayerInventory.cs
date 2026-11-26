using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    [Header("GameObject References")] 
    [SerializeField] private GameObject _inventoryCanvas;
    [SerializeField] private GameObject _itemsPanel;
    [SerializeField] private GameObject _eqdToolsPanel;
    [SerializeField] private GameObject _eqdPotionsPanel;
    
    [SerializeField] private List<ObjectSO> _allItems = new();   //4
    [SerializeField] private List<ToolSO>   _eqdTools = new();   //3
    [SerializeField] private List<PotionSO> _eqdPotions = new(); //2

    private PlayerData _playerData;
    private ToolSO _currentTool;
    private PotionSO _currentPotion;

    public event Action<List<ToolSO>, ToolSO> OnEquipTool;
    public event Action<List<PotionSO>, PotionSO> OnEquipPotion;
    
    [Header("Test")]
    private List<GameObject> _itemsSlots = new();
    private List<GameObject> _toolsSlots = new();
    private List<GameObject> _potionSlots = new();

    void Start()
    { 
        _playerData = GetComponent<PlayerData>();
        _inventoryCanvas.SetActive(false);
        foreach (Transform child in _itemsPanel.transform)      { _itemsSlots.Add(child.gameObject); }
        foreach (Transform child in _eqdToolsPanel.transform)   { _toolsSlots.Add(child.gameObject); }
        foreach (Transform child in _eqdPotionsPanel.transform) { _potionSlots.Add(child.gameObject); }
        
        ToggleSubscriptions(true);
    }

    void OnDestroy()
    {
        ToggleSubscriptions(false);
    }

    private void ToggleInventory(bool open) { _inventoryCanvas.SetActive(open); }
    
    
    
    
    
    public void UpdateList(ObjectSO obj, int id, bool type)
    {
        switch (id)
        {
            case 4:
                if (type)   { if (!_allItems.Contains(obj)) { _allItems.Add(obj); } } 
                else        { if (_allItems.Contains(obj)) { _allItems.Remove(obj); } }
                break;
            case 3: 
                if (obj is not ToolSO tool) { Debug.LogError($"{obj.name} is not a TOOL"); break; }
                if (type)   { if (!_eqdTools.Contains(tool)) { _eqdTools.Add(tool); } } 
                else        { if (_eqdTools.Contains(tool)) { _eqdTools.Remove(tool); } }
                break;
            case 2:
                if (obj is not PotionSO potion) { Debug.LogError($"{obj.name} is not a POTION"); break; }
                if (type)   { if (!_eqdPotions.Contains(potion)) { _eqdPotions.Add(potion); } } 
                else        { if (_eqdPotions.Contains(potion)) { _eqdPotions.Remove(potion); } }
                break;
        }
    }

    public void Equip<T>(T item)
    {
        switch (item)
        {
            case PotionSO potion: _currentPotion = potion; OnEquipPotion?.Invoke(_eqdPotions, _currentPotion); break;
            case ToolSO tool:     _currentTool = tool;     OnEquipTool?.Invoke(_eqdTools, _currentTool);   break;
            default: Debug.LogError("Invalid object"); break; 
            
        }
    }

    private void ToggleSubscriptions(bool isSubscribed)
    {
        if (isSubscribed)
        {
            _playerData.OnToggleInventory += ToggleInventory;
        }
        else
        {
            _playerData.OnToggleInventory -= ToggleInventory;
        }
    }


}
