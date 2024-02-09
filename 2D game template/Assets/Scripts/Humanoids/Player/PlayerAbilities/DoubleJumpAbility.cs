using UnityEngine;

public class DoubleJumpAbility : MonoBehaviour
{
    public float jumpPower = 26f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerState playerStateManager;
    void Update()
    {
        DoubleJump();
    }

    private void DoubleJump() {
        if ( playerStateManager.isJumping && !playerStateManager.doubleJumped && Input.GetButtonDown("Jump") ) {
            playerStateManager.doubleJumped = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
    }
}
