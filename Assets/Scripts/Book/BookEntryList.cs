using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BookEntryList : MonoBehaviour
{    
    [SerializeField] private GameObject bookEntryPrefab;
    [SerializeField] private GameObject bookCanvas;
    [SerializeField] private GameObject bookIndex;
    [SerializeField] private GameObject anchor;
    [SerializeField] private GameObject itemDetailsScreen;
    [SerializeField] private ScrollRect scrollRect;
    private BookEntryDetails _entryDetails;
    
    
    [SerializeField] private ObjectSO[] creatureEntries; //0
    [SerializeField] private ObjectSO[] materialEntries; //1
    [SerializeField] private ObjectSO[] potionEntries;   //2
    [SerializeField] private ObjectSO[] toolEntries;     //3
    
    
    //TEST//
    public List<ObjectSO> shownObjects;

    void Start()
    {
        _entryDetails = GetComponent<BookEntryDetails>();
        bookCanvas.SetActive(false); 
    }
    
  
    
    private void PrepareList(ObjectSO[] objs)
    {
        foreach (Transform child in anchor.transform) { Destroy(child.gameObject); } //clear
        foreach (ObjectSO obj in shownObjects)
        {
            if (obj is null || !objs.Contains(obj)) { continue; }
            SpawnEntry(obj, anchor);
        }
        scrollRect.verticalNormalizedPosition = 1;
        bookIndex.SetActive(false);
        itemDetailsScreen.SetActive(true);
    }

    private void SpawnEntry(ObjectSO obj, GameObject parent)
    {
        GameObject entry = Instantiate(bookEntryPrefab, gameObject.transform);
        entry.transform.SetParent(parent.transform);
        EntryButton butt = entry.GetComponent<EntryButton>();
        butt.ApplyObject(obj, _entryDetails);
        entry.SetActive(true);
    }

    public void OpenChapter(int type)
    {
        switch (type)
        {
            case 0: PrepareList(creatureEntries);   break;
            case 1: PrepareList(materialEntries);   break;
            case 2: PrepareList(potionEntries);     break;
            case 3: PrepareList(toolEntries);       break;
            default: Debug.Log("Not implemented");  break;
        }
    }

    public void CloseChapter()
    {
        itemDetailsScreen.SetActive(false);
        bookIndex.SetActive(true);
    }
    
}

