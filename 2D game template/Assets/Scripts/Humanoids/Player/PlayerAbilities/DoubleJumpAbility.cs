using UnityEngine;

public class DoubleJumpAbility : MonoBehaviour
{
    [SerializeField] private float jumpPower = 60f;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerState playerStateManager;
    void Update()
    {
        if (playerStateManager.isJumping && !playerStateManager.doubleJumped && Input.GetButtonDown("Jump"))
        {
            DoubleJump();
        }
    }

    private void DoubleJump() {
        playerStateManager.doubleJumped = true;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }
}
