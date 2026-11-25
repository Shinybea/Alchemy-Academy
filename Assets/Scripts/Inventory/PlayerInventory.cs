using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    [SerializeField] private List<ObjectSO> _allItems = new();   //4
    [SerializeField] private List<ToolSO>   _eqdTools = new();   //3
    [SerializeField] private List<PotionSO> _eqdPotions = new(); //2
    
    private ToolSO _currentTool;
    private PotionSO _currentPotion;

    public event Action<List<ToolSO>, ToolSO> OnEquipTool;
    public event Action<List<PotionSO>, PotionSO> OnEquipPotion;
    
    public List<ObjectSO> AllItems => _allItems;
    public List<ToolSO> EqdTools => _eqdTools;
    public List<PotionSO> EqdPotions => _eqdPotions;
    public ToolSO CurrentTool => _currentTool;
    public PotionSO CurrentPotion => _currentPotion;
    
    
    
    
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


}
