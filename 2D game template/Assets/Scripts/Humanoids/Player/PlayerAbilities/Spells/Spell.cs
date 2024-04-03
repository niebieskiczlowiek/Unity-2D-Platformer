using Unity.VisualScripting;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private int cost;

    private bool CanCast()
    {
        var playerInventory = PlayerInventoryManager.Instance.GetPlayerInventory();
        if (playerInventory.lanternSlot.IsUnityNull() || !playerInventory.lanternSlot.lightEquipped) return false;

        var lightLevel = playerInventory.lanternSlot.orbSlot.lightLevel;

        return cost <= lightLevel;
    }
}