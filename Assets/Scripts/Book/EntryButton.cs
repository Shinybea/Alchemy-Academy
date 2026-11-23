using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntryButton : MonoBehaviour
{
    [SerializeField] private Image entrySprite;
    [SerializeField] private Image entryBackground;

    [SerializeField] private TextMeshProUGUI entryName;

    private BookManager _bookManager;
    // private BookEntryDetails _entryDetails;
    private ObjectSO entryObj;
   
    public void ApplyObject(ObjectSO obj, BookManager details)
    {
        entryObj = obj;
        if (entryObj == null) { Debug.LogError("Invalid obj"); }
        _bookManager  = details;
        if (_bookManager == null) { Debug.LogError("Invalid details"); }
        entryName.text = obj.objName;
        entrySprite.sprite = obj.objThumbnail;
        if (obj.objThumbnailBkg != null) { entryBackground.sprite = obj.objThumbnailBkg; }
    }

    public void ShowObjectDetails()
    { _bookManager.ShowEntryDetails(entryObj); }
    
}
