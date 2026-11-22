using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BookEntryDetails : MonoBehaviour
{
    [SerializeField] private GameObject entryDetailsPanel;
    [SerializeField] private TextMeshProUGUI entryName;
    [SerializeField] private TextMeshProUGUI entryDescription;
    [SerializeField] private Image entryIcon;

    private void Start()
    { HideEntryDetails(); }
    
    
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
    
}
