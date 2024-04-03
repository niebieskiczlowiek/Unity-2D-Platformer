using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LightLevelBarUI : MonoBehaviour
{
    [SerializeField] private TMP_Text lightLevelText;
    [SerializeField] private Orb orbStats;
    

    public void Start()
    {
        ManageText();
    }

    public void Update()
    {
        if (!PlayerInventoryManager.Instance.GetPlayerInventory().lanternSlot.IsUnityNull()
            && PlayerInventoryManager.Instance.GetPlayerInventory().lanternSlot.lightEquipped)
        {
            Show();
        }
        else
        {
            Hide();
        }
        ManageText();
    }

    private void ManageText()
    {
        lightLevelText.text =  "" + orbStats.lightLevel;
    }

    private void Hide()
    {
        lightLevelText.alpha = 0f;
    }
    
    private void Show()
    {
        lightLevelText.alpha = 1f;
    }
}