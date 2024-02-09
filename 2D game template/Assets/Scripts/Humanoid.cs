using System.Collections;
using UnityEngine;

public abstract class Humanoid : MonoBehaviour {
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;
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

    protected virtual void Die()
    {
        Debug.Log("died");
    }

    public virtual void TakeDamage(int damageTaken, Vector2 damageDirection)
    {
        CurrentHealth -= damageTaken;
    }
    protected virtual IEnumerator ApplyKnockBack(Rigidbody2D rb, Vector2 knockBackDirection)
    {
        Debug.Log("KNOCK BACK APPLIED TO " + HumanoidName);
        
        // Vector2 difference = rb.transform.position - transform.position;
        // difference = difference.normalized * 100f;
        // rb.AddForce(difference, ForceMode2D.Impulse);
        // rb.AddForce(collider.transform.localScale * 100f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
    }
}