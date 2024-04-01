using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    [SerializeField] private Item myItem;
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private PlayerInventoryManager inventoryManager;

    public Item GetItem()
    {
        return myItem;
    }
     
    public void Start()
    {
        inventory = PlayerInventoryManager.GetInventory();
        gameObject.tag = "Item";
    }
    
    public void PickUp()
    {
        PlayerInventoryManager.Instance.AddToInventory(gameObject.GetComponent<DroppedItem>());
        Destroy(gameObject);
    }
}