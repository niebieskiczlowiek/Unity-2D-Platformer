using System.Collections;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour {

    [SerializeField] private UnlockedAbilities unlockedAbilitiesManager;
    [SerializeField] private PlayerState playerStateManager;
    [SerializeField] private Rigidbody2D rb;

    private bool dashOnCooldown = false;
    private float dashingPower = 30f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 0.5f;

    void Update()
    {
        if (DashChecker()) {
            return;
        }
        Dashing();
    }

    private void Dashing() {
        if (Input.GetKey(KeyCode.LeftShift) && unlockedAbilitiesManager.dashAbility && !dashOnCooldown)
        {
            Debug.Log("Dash pressed!");
            StartCoroutine(Dash());
        }   
    }

    private bool DashChecker() {
        if (playerStateManager.isDashing) {
            return true; // prevents player from other movements when dashing
        }
        return false;
    }

    private IEnumerator Dash()
    {
        dashOnCooldown = true;
        playerStateManager.isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);

        yield return new WaitForSeconds(dashingTime); // yields (stops) the rest of the script from executing for the duration of the dash

        rb.gravityScale = originalGravity;
        playerStateManager.isDashing = false;

        yield return new WaitForSeconds(dashingCooldown);
        dashOnCooldown = false;
    }
}
