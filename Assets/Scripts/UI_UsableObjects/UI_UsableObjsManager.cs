using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_UsableObjsManager : MonoBehaviour
{
    
    [SerializeField] private PlayerInventory playerInventory;

    [Header("Slots references")]
    [SerializeField] private Slots tools;
    [SerializeField] private Slots potions;


    void Start()
    { ToggleSubscriptions(true); }
    void OnDestroy()
    { ToggleSubscriptions(false); }

    private void ChangeSelected<T>(List<T> list, T obj) where T : ObjectSO
    {
        int index = list.IndexOf(obj);
        int count = list.Count;
        ChangeCloseItems(list[(index - 1 + count) % count], list[(index + 1) % count]); 
    }

    private void ChangeSelectedTool(List<ToolSO> list, ToolSO obj) { ChangeSelected(list, obj); }
    private void ChangeSelectedPotion(List<PotionSO> list, PotionSO obj) { ChangeSelected(list, obj); }

    private void ChangeCloseItems<T>(T prev, T next) where T : ObjectSO
    {
        if (prev is PotionSO != next is PotionSO) { return; }
        switch (prev)
        {
            case PotionSO:
                potions.prev.sprite = prev.objIcon;
                potions.next.sprite = next.objIcon;
                break;
            case ToolSO:
                tools.prev.sprite = prev.objIcon;
                tools.next.sprite = prev.objIcon;
                break;
        }
    }

    private void ToggleSubscriptions(bool isSubscribed)
    {
        if (isSubscribed)
        {
            playerInventory.OnEquipPotion += ChangeSelectedPotion;
            playerInventory.OnEquipTool += ChangeSelectedTool;
        }
        else
        {
            playerInventory.OnEquipTool -= ChangeSelectedTool;
            playerInventory.OnEquipPotion -= ChangeSelectedPotion;
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

//logic:
//use bag list, find items previous and next to the current and apply image
//current tool = equipped, base is wand (cannot remove from inventory, can be replaced between runs)

// public void ChangeEquipped<T>(T obj)
// {
//     int index = 0;
//     int count = 0;
//     
//     switch (obj)
//     {
//         case PotionSO:
//             index = playerInventory.EqdPotions.IndexOf(playerInventory.CurrentPotion);
//             count = playerInventory.EqdPotions.Count;
//             ChangeCloseItems(
//                 FindCloseInList(playerInventory.EqdPotions, index, count, false), 
//                 FindCloseInList(playerInventory.EqdPotions, index, count, true)); 
//             break;
//         case ToolSO: 
//             index = playerInventory.EqdTools.IndexOf(playerInventory.CurrentTool);
//             count = playerInventory.EqdTools.Count;
//             ChangeCloseItems(
//                 FindCloseInList(playerInventory.EqdTools, index, count, false), 
//                 FindCloseInList(playerInventory.EqdTools, index, count, true));
//             break;
//         default: Debug.LogError("Invalid object"); break;
//     }
// }

// private T FindCloseInList<T>(List<T> list, int index, int count, bool next)
// { return next ? list[(index + 1) % count] : list[(index - 1 + count) % count]; }




