using UnityEngine;

[CreateAssetMenu(fileName = "Lantern", menuName = "Item/Lantern")]
public class Lantern : Item
{
    public bool lightEquipped;
    public int damage = 1;
    public Orb orbSlot;
}