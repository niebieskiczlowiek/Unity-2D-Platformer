using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAbility : MonoBehaviour
{
    private float slashingTime = 0.1f;
    private float slashingCooldown = 0.2f;
    private bool slashOnCooldown;

    [SerializeField] private GameObject slashBox;
    [SerializeField] private PlayerState playerStateManager;

    private void Update()
    {
        if (slashOnCooldown) return;
        if (Input.GetButtonDown("Slash")) StartCoroutine(Slash());
    }

    private IEnumerator Slash() {
        
        slashOnCooldown = true;
        playerStateManager.isSlashing = true;
        // slashBoxScript.ActivateSlash();
        slashBox.SetActive(true);

        yield return new WaitForSeconds(slashingTime);

        // slashBoxScript.DeactivateSlash();
        slashBox.SetActive(false);
        playerStateManager.isSlashing = false;

        yield return new WaitForSeconds(slashingCooldown);

        slashOnCooldown = false;
    }
}
