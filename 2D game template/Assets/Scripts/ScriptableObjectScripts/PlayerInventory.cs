using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Inventory", menuName = "Player/Inventory")]
public class PlayerInventory : ScriptableObject
{
    // public Lantern slot
    public Lantern lanternSlot;
    public List<Item> items;
}