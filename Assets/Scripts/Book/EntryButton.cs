using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntryButton : MonoBehaviour
{
    [SerializeField] private Image entrySprite;
    [SerializeField] private Image entryBackground;

    [SerializeField] private TextMeshProUGUI entryName;

    private BookEntryDetails _entryDetails;
    private ObjectSO entryObj;
   
    public void ApplyObject(ObjectSO obj, BookEntryDetails details)
    {
        entryObj = obj;
        _entryDetails  = details;
        entryName.text = obj.objName;
        entrySprite.sprite = obj.objThumbnail;
        if (obj.objThumbnailBkg != null) { entryBackground.sprite = obj.objThumbnailBkg; }
    }

    public void ShowObjectDetails()
    {
        if (entryObj == null || _entryDetails == null) { return; }

        _entryDetails.ShowEntryDetails(entryObj);
    }
    
}
