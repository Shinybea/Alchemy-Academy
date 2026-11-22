using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EntryButton : MonoBehaviour
{
    [SerializeField] private Image entrySprite;
    [SerializeField] private Image entryBackground;

    [SerializeField] private TextMeshProUGUI entryName;
   
    public void ApplyObject(ObjectSO obj)
    {
        entryName.text = obj.objName;
        entrySprite.sprite = obj.objThumbnail;
        if (obj.objThumbnailBkg != null) { entryBackground.sprite = obj.objThumbnailBkg; }
    }
}
