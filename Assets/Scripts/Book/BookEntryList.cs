using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BookEntryList : MonoBehaviour
{    
    [SerializeField] private GameObject bookEntryPrefab;
    [SerializeField] private ObjectSO[] bookEntries;
    [SerializeField] private GameObject list;
    [SerializeField] private ScrollRect scrollRect;
    private BookEntryDetails _entryDetails;

    
    
    public List<ObjectSO> shownObjects;
    public bool isTest = false;

    void Start()
    {
        _entryDetails = GetComponent<BookEntryDetails>();
        PrepareList();
        list.SetActive(true);
        scrollRect.verticalNormalizedPosition = 1;
    }
    
    private void PrepareList()
    {
        foreach (Transform child in list.transform) { Destroy(child.gameObject); } //clear
        foreach (ObjectSO obj in shownObjects)
        {
            if (obj is null || !bookEntries.Contains(obj)) { continue; }
            SpawnEntry(obj, list);
        }
    }

    private void SpawnEntry(ObjectSO obj, GameObject parent)
    {
        GameObject entry = Instantiate(bookEntryPrefab, gameObject.transform);
        entry.transform.SetParent(parent.transform);
        EntryButton butt = entry.GetComponent<EntryButton>();
        butt.ApplyObject(obj, _entryDetails);
        entry.SetActive(true);
    }
    
    
}
