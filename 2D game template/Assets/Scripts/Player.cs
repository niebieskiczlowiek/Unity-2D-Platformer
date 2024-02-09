using System.Collections;
using UnityEngine;

public class Player : Humanoid {
    [SerializeField] private PlayerStats playerStatsManager;
    [SerializeField] private PlayerState playerStateManager;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Rigidbody2D playerRb;

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
    protected override IEnumerator ApplyKnockBack(Rigidbody2D rb, Vector2 knockBackDirection)
    {
        playerStateManager.isKnockBacked = true;
        rb.AddForce(knockBackDirection * 80f, ForceMode2D.Impulse);
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

    public override void TakeDamage(int damageTaken, Vector2 damageDirection)
    {
        if (!(Time.time - _lastTimeHit > _invincibilityFramesTime))
        {
            return;
        }
        CurrentHealth -= damageTaken;
        StartCoroutine(ApplyKnockBack(playerRb, damageDirection));
        _lastTimeHit = Time.time;
    }
}