using UnityEngine;

[CreateAssetMenu(fileName = "Entry Button", menuName = "SO/Book/Entry Buttons")]
public class EntryButtonSO : ScriptableObject
{
    public int entryIndex;
    public string entryName;
    public Sprite entryIcon;
    public ObjectSO entryObject;
}
