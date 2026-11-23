using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BookManager : MonoBehaviour
{    
    #region REFERENCES - BOOK
        [Header("Book")]
        [SerializeField] private GameObject bookCanvas;
        [SerializeField] private GameObject bookEntryPrefab;
        [SerializeField] private GameObject bookIndex;
        [SerializeField] private GameObject anchor;
        [SerializeField] private GameObject itemDetailsScreen;
        [SerializeField] private ScrollRect scrollRect;
        // private BookEntryDetails _entryDetails;
    #endregion
    
    #region REFERENCES - DETAILS
        [Header("Entry Details")]
        [SerializeField] private GameObject entryDetailsPanel;
        [SerializeField] private TextMeshProUGUI entryName;
        [SerializeField] private TextMeshProUGUI entryDescription;
        [SerializeField] private Image entryIcon;
    #endregion
    
    #region CHAPTER ENTRIES
        [Header("Chapter Entries")]
        [SerializeField] private ObjectSO[] creatureEntries; //0
        [SerializeField] private ObjectSO[] materialEntries; //1
        [SerializeField] private ObjectSO[] potionEntries;   //2
        [SerializeField] private ObjectSO[] toolEntries;     //3
    #endregion
    
    #region TEST
        [Header("Test")]
        public List<ObjectSO> shownObjects;
    #endregion

    void Start()
    {
        HideEntryDetails();
        CloseBook();
    }

    #region BOOK/CHAPTER CALLS

        public void OpenBook()
        {
            CloseChapter();
            bookCanvas.SetActive(true);
        }
        public void CloseBook()
        {
            CloseChapter();
            bookCanvas.SetActive(false);
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
            HideEntryDetails();
            itemDetailsScreen.SetActive(false);
            bookIndex.SetActive(true);
        }

    #endregion
    
    
    #region ENTRY LIST
    
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
            butt.ApplyObject(obj, this);
            entry.SetActive(true);
        }
    #endregion




    #region ENTRY DETAILS
        public void ShowEntryDetails(ObjectSO obj)
        {
            entryName.text = obj.objName;
            entryDescription.text = obj.objDescription;
            entryIcon.sprite = obj.objIcon;
            
            entryDetailsPanel.SetActive(true);
        }

        public void HideEntryDetails()
        {
            ClearEntryDetails();
            entryDetailsPanel.SetActive(false);
        }
        
        private void ClearEntryDetails()
        {
            entryName.text = "";
            entryDescription.text = "";
            entryIcon.sprite = null;
        }
    #endregion
    
   
    
}

