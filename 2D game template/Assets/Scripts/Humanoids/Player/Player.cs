using System.Collections;
using UnityEngine;

public class Player : Humanoid {
    [SerializeField] private PlayerStats playerStatsManager;
    [SerializeField] private PlayerState playerStateManager;
    [SerializeField] private Transform playerTransform;

    private float _invincibilityFramesTime = 1f;
    private float _lastTimeHit;
    private Vector3 _playerRespawnPoint;
    public override int MaxHealth
    {
        get => playerStatsManager.maxHealth;
        set => playerStatsManager.maxHealth = value;
    }
    public override int CurrentHealth
    {
        get => playerStatsManager.currentHealth;
        set => playerStatsManager.currentHealth = value;
    }

    private void Start()
    {
        HumanoidName = "Player";
        _playerRespawnPoint = playerTransform.position;
    }
    private void Update()
    {
        if (CurrentHealth == 0)
        {
            Die();
        }
    }
    protected override IEnumerator ApplyKnockBack(Vector2 knockBackDirection)
    {
        playerStateManager.isKnockBacked = true;
        Rigidbody.AddForce(knockBackDirection * 80f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
        playerStateManager.isKnockBacked = false;
    }
    protected override void Die()
    {
        playerTransform.position = _playerRespawnPoint;
        RestoreHealthFull();
    }

    public void RestoreHealth(int healthAmount)
    {
        CurrentHealth += healthAmount;
    }
    private void RestoreHealthFull()
    {
        CurrentHealth = MaxHealth;
    }

    protected override void TakeDamage(int damageTaken)
    {
        if (!(Time.time - _lastTimeHit > _invincibilityFramesTime))
        {
            return;
        }
        CurrentHealth -= damageTaken;
        _lastTimeHit = Time.time;
    }

    public void checkCollision()
    {
        Debug.Log("player collided");
    }
}