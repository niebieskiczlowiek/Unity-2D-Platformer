using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
        public static PlayerInventoryManager Instance { get; set; }
        [SerializeField] private PlayerInventory playerInventory;

        private enum SpecialItems
        {
                Lantern
        }

        private void Awake()
        {
                if (Instance != null && Instance != this) Destroy(this);
                else Instance = this;
        }

        public void Start()
        {
                playerInventory = FindInventory();
        }

        private static PlayerInventory FindInventory()
        {
                string[] searchFolders = { "Assets/SO" };
                string[] foundInventoryGUIDs = AssetDatabase.FindAssets("Player Inventory", searchFolders);
        
                if (foundInventoryGUIDs.Length <= 0) return null;
        
                string inventoryPath = AssetDatabase.GUIDToAssetPath(foundInventoryGUIDs[0]);
                PlayerInventory playerInventory = AssetDatabase.LoadAssetAtPath<PlayerInventory>(inventoryPath);

                if (playerInventory != null) return playerInventory;
                return null;
        }

        public PlayerInventory GetPlayerInventory()
        {
                return playerInventory;
        }

        public void AddToInventory(Item item)
        {
                switch (item.itemName)
                {
                        case "Lantern":
                                playerInventory.lanternSlot = (Lantern) item;
                                break;
                        case "Orb":
                                if (!playerInventory.lanternSlot.IsUnityNull())
                                {       
                                        playerInventory.lanternSlot.lightEquipped = true;
                                        playerInventory.lanternSlot.orbSlot = (Orb) item;
                                }
                                else
                                {
                                        playerInventory.orbSlot = (Orb) item;
                                }

                                break;
                        default:
                                var itemList = playerInventory.items.ToList();
                                itemList.Add(item);
                                playerInventory.items = itemList;
                                Debug.Log(item.itemName);
                                break;
                }
        }
}