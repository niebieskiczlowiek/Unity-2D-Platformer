using System.Collections;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour {

    [Header("Managers")]
    [SerializeField] private UnlockedAbilities unlockedAbilitiesManager;
    [SerializeField] private PlayerState playerStateManager;
    
    [Header("Player Components")]
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private Transform playerTransform;
    
    [Header("Dash variables")]
    [SerializeField] private float _dashingPower = 60f;
    [SerializeField] private float _dashingTime = 0.2f;
    [SerializeField] private float _dashingCooldown = 0.5f;
    private bool _dashOnCooldown;
    private bool _isDashing;
    private Vector2 _dashDirection;

    private void Update()
    {
        _isDashing = playerStateManager.isDashing;
        _dashDirection = GetDashDirection();
        
        if (CanDash()) {
            return;
        }

        if (_isDashing)
        {
            rb.velocity = _dashDirection.normalized * _dashingPower;
            return;
        }
        Dashing();
    }
    
    private Vector2 GetDashDirection()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        
        Vector2 dashDirection = new Vector2(x, y).normalized;
        
        if (dashDirection == Vector2.zero)
        {
            dashDirection = new Vector2(playerTransform.localScale.x, 0).normalized;
        }

        return dashDirection;
    }

    private void Dashing() {
        if (Input.GetKey(KeyCode.LeftShift) && unlockedAbilitiesManager.dashAbility && !_dashOnCooldown)
        {
            StartCoroutine(Dash());
        }   
    }

    private bool CanDash() {
        return playerStateManager.isDashing;
    }

    private IEnumerator Dash()
    {
        _dashOnCooldown = true;
        playerStateManager.isDashing = true;
        var originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = _dashDirection * _dashingPower;
        
        yield return new WaitForSeconds(_dashingTime); // yields (stops) the rest of the script from executing for the duration of the dash

        rb.gravityScale = originalGravity;
        playerStateManager.isDashing = false;

        yield return new WaitForSeconds(_dashingCooldown);
        _dashOnCooldown = false;
    }
}
