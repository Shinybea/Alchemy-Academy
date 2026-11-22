using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BookEntryGenerator : MonoBehaviour
{    
    [SerializeField] private GameObject bookEntryPrefab;
    [SerializeField] private ObjectSO[] bookEntries;

    public List<ObjectSO> shownObjects;
    public bool isTest = false;

    void Start()
    {
        Clear();
        
        if (!isTest) { return; }
        OnOpen();
    }
    
    
    void Clear()
    { foreach (Transform child in gameObject.transform) { Destroy(child.gameObject); } }
    
    private void OnOpen()
    {
        foreach (ObjectSO obj in shownObjects)
        {
            if (obj is null || !bookEntries.Contains(obj)) { continue; }
            Debug.Log($"{obj.name} is in list");
            SpawnEntry(obj);
        }
    }

    private void SpawnEntry(ObjectSO obj)
    {
        GameObject entry = Instantiate(bookEntryPrefab, gameObject.transform);
        entry.GetComponent<EntryButton>().ApplyObject(obj);
        entry.SetActive(true);
    }
}
