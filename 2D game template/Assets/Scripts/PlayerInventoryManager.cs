using UnityEditor;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
        public static PlayerInventoryManager Instance { get; set; }
        [SerializeField] private PlayerInventory playerInventory;

        enum SpecialItems
        { Lantern }

        private void Awake()
        {
                if (Instance != null && Instance != this) Destroy(this);
                else Instance = this;
        }

        public void Start()
        {
                playerInventory = GetInventory();
        }

        public static PlayerInventory GetInventory()
        {
                string[] searchFolders = { "Assets/SO" };
                string[] foundInventoryGUIDs = AssetDatabase.FindAssets("Player Inventory", searchFolders);
        
                if (foundInventoryGUIDs.Length <= 0)
                {
                        Debug.LogError("Couldn't found any inventories");
                        return null;
                }
        
                string inventoryPath = AssetDatabase.GUIDToAssetPath(foundInventoryGUIDs[0]);
                PlayerInventory playerInventory = AssetDatabase.LoadAssetAtPath<PlayerInventory>(inventoryPath);

                if (playerInventory != null) return playerInventory;
            
                Debug.LogError("Failed to load Player Inventory at path: " + inventoryPath);
                return null;
        }

        public void AddToInventory(DroppedItem item)
        {
                Debug.Log(item.name);
                playerInventory.items.Add(item.GetItem());
        }
}