using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LanternBarNoLight : MonoBehaviour
{
    [SerializeField] private Image lanternSprite;

    public void Update()
    {
        if (!PlayerInventoryManager.Instance.GetPlayerInventory().lanternSlot.IsUnityNull()
            && !PlayerInventoryManager.Instance.GetPlayerInventory().lanternSlot.lightEquipped)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    
    private void Hide() { lanternSprite.enabled = false; }
    private void Show() { lanternSprite.enabled = true; }
}