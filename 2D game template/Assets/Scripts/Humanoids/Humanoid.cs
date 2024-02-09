using System.Collections;
using UnityEngine;

public abstract class Humanoid : MonoBehaviour {
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;
    [SerializeField] private Rigidbody2D _humanoidRb;
    protected string HumanoidName;
    
    
    public virtual int CurrentHealth
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

    protected virtual void Die()
    {
        Debug.Log("died");
    }

    protected virtual void TakeDamage(int damageTaken)
    {
        CurrentHealth -= damageTaken;
    }
    protected virtual IEnumerator ApplyKnockBack(Vector2 knockBackDirection)
    {
        yield return new WaitForSeconds(0.1f);
    }
    public void HandleHit(Hit hitObject) // Called when something hits this Humanoid
    {
        // Take Damage
        int damageReceived = hitObject.GetDamageDealt();
        TakeDamage(damageReceived);
        
        // Apply Knockback
        Vector2 hitDirection = hitObject.GetHitDirection();
        StartCoroutine(ApplyKnockBack(hitDirection));
        
        Debug.Log(HumanoidName + " got hit");
    }
}