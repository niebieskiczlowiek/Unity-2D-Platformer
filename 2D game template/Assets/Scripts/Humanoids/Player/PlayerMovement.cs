using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Variables")]
    [SerializeField] private float _movementAcceleration = 50f;
    [SerializeField] private float _maxSpeed = 12f;
    [SerializeField] private float _linearDrag = 10f;
    private float _horizontalMovement;
    private float _verticalMovement;
    private bool _isFacingRight = true;
    private bool _changingDirection => (rb.velocity.x > 0f && _horizontalMovement < 0f) ||
                                       (rb.velocity.x < 0f && _horizontalMovement > 0f);
    
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck; 
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private PlayerState playerStateManager;
    
    [Header("Jump Variables")]
    [SerializeField] private float _jumpPower = 12f;
    [SerializeField] private float _airLinearDrag = 2.5f;
    [SerializeField] private float _fallMultiplier = 8f;
    [SerializeField] private float _lowJumpFallMultiplier = 5f;
    
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

    }
    private void Update()
    {
        _horizontalMovement = GetInput().x;
        _verticalMovement = GetInput().y;
        
        if (IsGrounded()) { 
            playerStateManager.isJumping = false;
            playerStateManager.doubleJumped = false;
        }
        else
        {
            playerStateManager.isJumping = true;
        }   
        DashChecker();
        Flip();
    }
    private void FixedUpdate()
    {
        if (DashChecker() || KnockBackChecker()) { return; }
        MoveHorizontal();
        if (Input.GetButtonDown("Jump") && IsGrounded()) {
            Jump();
        }
        if (IsGrounded())
        {
            ApplyGroundLinearDrag();
        }
        else
        {
            ApplyAirLinearDrag();
            // FallMultiplier();
        }
    }

    private static Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
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
        // if (Input.GetButtonDown("Jump") && IsGrounded())
        // {
        //     
        // }
        //
        // if (IsGrounded())
        // {
        //     ApplyGroundLinearDrag();
        // }
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        
        
        // if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        // {
        //     rb.AddForce(Vector2.up * (_jumpPower * 0.5f), ForceMode2D.Force);
        //     // rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        // }
    }

    private void FallMultiplier()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = _fallMultiplier;
        } 
        else if (rb.velocity.y > 0 && !Input.GetButtonDown("Jump"))
        {
            rb.gravityScale = _lowJumpFallMultiplier;
        } 
        else
        {
            rb.gravityScale = 1f;
        }
    }
    
    private void MoveHorizontal() {
        rb.AddForce(new Vector2(_horizontalMovement, 0f) * _movementAcceleration);
        if (Math.Abs(rb.velocity.x) > _maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * _maxSpeed, rb.velocity.y);
        } 
    }

    private void ApplyGroundLinearDrag() { rb.drag = Mathf.Abs(_horizontalMovement) < 0.4f || _changingDirection ? _linearDrag : 0f; }
    private void ApplyAirLinearDrag() { rb.drag = _airLinearDrag; }

    private bool IsGrounded() { return Physics2D.OverlapCircle(groundCheck.position, 0.8f, groundLayer); }

    private void Flip()
    {
        if (playerStateManager.isSlashing){
            return;
        }

        if (_isFacingRight && _horizontalMovement < 0f || !_isFacingRight && _horizontalMovement > 0f)
        {
            _isFacingRight = !_isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}