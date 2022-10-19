using UnityEngine;

public abstract class CollectibleItem : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Icon;
    public int Level = 0;
}
