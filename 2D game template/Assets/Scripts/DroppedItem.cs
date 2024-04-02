using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    [SerializeField] private Item myItem;

    public Item GetItem()
    {
        return myItem;
    }
     
    public void Start()
    {
        gameObject.tag = "Item";
    }
    
    public void PickUp()
    {
        // Debug.Log("PICKUP " + gameObject.GetComponent<DroppedItem>().GetItem().itemName);
        // PlayerInventoryManager.Instance.AddToInventory(gameObject.GetComponent<DroppedItem>());
        PlayerInventoryManager.Instance.AddToInventory(myItem);
        Destroy(gameObject);
    }
}