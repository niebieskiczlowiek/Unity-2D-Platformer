using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float horizontalMovement;
    public float speed = 10f;
    public float jumpPower = 26f;
    private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck; 
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private PlayerState playerStateManager;
    
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (IsGrounded()) { 
            playerStateManager.isJumping = false;
            playerStateManager.doubleJumped = false;
        } else {
            playerStateManager.isJumping = true;
        }

        Jump();
        DashChecker();
        Flip();
    }

    private void FixedUpdate()
    {
        if (DashChecker() || KnockBackChecker()) {
            return;
        }
        MoveHorizontal();
    }

    private bool DashChecker() 
    {
        return playerStateManager.isDashing; // prevents player from other movements when dashing
    }

    private bool KnockBackChecker()
    {
        return playerStateManager.isKnockBacked;
    }

    private void Jump() {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private void MoveHorizontal() {
        rb.velocity = new Vector2(horizontalMovement * speed, rb.velocity.y);
        // Vector2 xd =  new Vector2(horizontalMovement * speed, rb.velocity.y);
        // rb.AddForce(xd, ForceMode2D.Force);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.8f, groundLayer);
    }

    private void Flip()
    {
        if (playerStateManager.isSlashing){
            return;
        }

        if (isFacingRight && horizontalMovement < 0f || !isFacingRight && horizontalMovement > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;

        }
    }
}