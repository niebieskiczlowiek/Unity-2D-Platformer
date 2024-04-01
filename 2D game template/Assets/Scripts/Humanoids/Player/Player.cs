using System;
using System.Collections;
using UnityEngine;

public class Player : Humanoid {
    [Header("Managers")]
    [SerializeField] private PlayerStats playerStatsManager;
    [SerializeField] private PlayerState playerStateManager;
    
    [Header("Components")]
    [SerializeField] private Transform playerTransform;
    
    private Vector3 _playerRespawnPoint;
    
    public override int MaxHealth
    {
        get => playerStatsManager.maxHealth;
        set => playerStatsManager.maxHealth = value;
    }
    protected override int CurrentHealth
    {
        get => playerStatsManager.currentHealth;
        set => playerStatsManager.currentHealth = value;
    }

    private void Start()
    {
        _playerRespawnPoint = playerTransform.position;
    }
    private void Update()
    {
        if (CurrentHealth == 0) Die(); 
        playerStateManager.hitStunActive = hitStun.IsCoolingDown;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Item") && Input.GetButtonDown("Interact"))
        {
            DroppedItem item = other.gameObject.GetComponent<DroppedItem>();
            item.PickUp();
        }
    }

    protected override void Die()
    {
        playerTransform.position = _playerRespawnPoint;
        RestoreHealthFull();
    }
    public void RestoreHealth(int healthAmount) { CurrentHealth += healthAmount; }
    private void RestoreHealthFull() { CurrentHealth = MaxHealth; }
    protected override void TakeDamage(int damageTaken) { CurrentHealth -= damageTaken; }
    public override void HandleHit(Hit hitObject) // Called when something hits this Humanoid
    {
        if (hitCooldown.IsCoolingDown) return;
        
        // Take Damage
        int damageReceived = hitObject.GetDamageDealt();
        TakeDamage(damageReceived);
        
        // Apply knock-back
        Vector2 hitDirection = hitObject.GetHitDirection();
        ApplyKnockBack(hitDirection);

        hitCooldown.StartCoolDown();
    }
}