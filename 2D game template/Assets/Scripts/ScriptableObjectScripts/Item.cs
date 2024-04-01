using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/Item")]
public class Item : ScriptableObject
{
    public Sprite sprite;
    public int itemId;
    public string itemName;
}

// The item scriptable object only holds information about a certain item.