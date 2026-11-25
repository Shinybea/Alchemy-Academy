using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_UsableObjsManager : MonoBehaviour
{
    
    [SerializeField] private PlayerInventory playerInventory;

    [Header("Slots references")]
    
    
    // [SerializeField] private Slots slots;
    [SerializeField] private Slots tools;
    [SerializeField] private Slots potions;

    private ToolSO _currentTool;
    private PotionSO _currentPotion;

    
    //logic:
    //use pouch list, find items previous and next to the current and apply image
    //current tool = equipped, base is wand (cannot remove from inventory, can be replaced between runs)

    public void EquipAutomatic(Object obj)
    {
        int i = 0;
        if (obj.GetType() == typeof(PotionSO) ) { _currentPotion = obj as PotionSO; i = 2; }
        if (obj.GetType() == typeof(ToolSO) )   { _currentTool   = obj as ToolSO; i = 3; }
        CheckCloseItems(i);
    }

    private void CheckCloseItems(int i)
    {
        
        switch (i)
        {
            
            case 0: Debug.LogError("Equipped not assigned"); break;
            case 2: 
                //equip potion
                var pP = FindPrevInList(playerInventory.EqdPotions, _currentPotion);
                var nP =  FindNextInList(playerInventory.EqdPotions, _currentPotion);
                ChangeCloseItems(pP, pP);
                break;
            case 3: 
                //equip tool
                var pT = FindPrevInList(playerInventory.EqdTools, _currentTool);
                var nT = FindNextInList(playerInventory.EqdTools, _currentTool);
                ChangeCloseItems(pT, pT);
                break;
        }
    }

    private int FindPrevInList<T>(List<T> list, T obj)
    {
        int index = list.IndexOf(obj);
        int count = list.Count;

        return list.FindIndex[(index - 1 + count) % count];
    }

    private T FindNextInList<T>(List<T> list, T obj)
    {
        int index = list.IndexOf(obj);
        int count = list.Count;

        return list[(index + 1) % count];

    }

    private void ChangeCloseItems<T>(T prev, T next)
    {
        if (prev is PotionSO && next is PotionSO)
        {
            potions.prev = prev.
        }
    }
    
}

[System.Serializable]
public class Slots
{
    public Image current;
    public Image prev;
    public Image next;
}