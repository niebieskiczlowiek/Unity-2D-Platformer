using Unity.VisualScripting;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private int cost;
    [SerializeField] protected GameObject spellObject;
    [SerializeField] protected Transform transform;

    protected bool CanCast()
    {
        var playerInventory = PlayerInventoryManager.Instance.GetPlayerInventory();
        if (playerInventory.lanternSlot.IsUnityNull() || !playerInventory.lanternSlot.lightEquipped) return false;

        var lightLevel = playerInventory.lanternSlot.orbSlot.lightLevel;

        return cost <= lightLevel;
    }

    protected void UpdateEnergyAfterCast()
    {
        var playerInventory = PlayerInventoryManager.Instance.GetPlayerInventory();
        var lightLevel = playerInventory.lanternSlot.orbSlot.lightLevel;
        var newLightLevel = lightLevel - cost;
        playerInventory.lanternSlot.orbSlot.lightLevel = newLightLevel;
    }

    protected virtual void Cast()
    {
        UpdateEnergyAfterCast();
    }
}