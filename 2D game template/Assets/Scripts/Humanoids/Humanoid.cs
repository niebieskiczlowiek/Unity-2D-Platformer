using System;
using System.Collections;
using UnityEngine;

public abstract class Humanoid : MonoBehaviour { 
    [Header("Humanoid values")]
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;
    [SerializeField] private Rigidbody2D _humanoidRb;
    [SerializeField] protected Cooldown hitCooldown;
    // the hitCooldown attribute essentially functions as invicibility frames
    // when it's active the Humanoid will not handle any incoming hit objects
    // meaning it will not receive damage or knock-backed, essentially ignoring any hits
    [SerializeField] protected Cooldown hitStun;

    public void FixedUpdate()
    {
        if (hitStun.IsCoolingDown) Rigidbody.drag = 0;
    }

    protected virtual int CurrentHealth
    {
        get => _currentHealth;
        set => _currentHealth = value;
    }
    public virtual int MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }
    public virtual Rigidbody2D Rigidbody
    {
        get => _humanoidRb;
        set => _humanoidRb = value;
    }

    protected virtual void Die() { Debug.Log("died"); }

    protected virtual void TakeDamage(int damageTaken)
    {
        CurrentHealth -= damageTaken;
    }
    protected virtual void ApplyKnockBack(Vector2 knockBackDirection)
    {
        hitStun.StartCoolDown();
        Rigidbody.AddForce(knockBackDirection * 80f, ForceMode2D.Impulse);
    }
    public virtual void HandleHit(Hit hitObject) // Called when something hits this Humanoid
    {
        if (hitCooldown.IsCoolingDown) return;
        
        // Take Damage
        int damageReceived = hitObject.GetDamageDealt();
        TakeDamage(damageReceived);
        
        // Apply Knockback
        Vector2 hitDirection = hitObject.GetHitDirection();
        ApplyKnockBack(hitDirection);
        
        hitCooldown.StartCoolDown();
    }
}