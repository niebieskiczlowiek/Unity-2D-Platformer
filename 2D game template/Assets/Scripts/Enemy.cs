using System.Collections;
using UnityEngine;

public class Enemy : Humanoid
{
    [SerializeField] private Rigidbody2D enemyRb;
    [SerializeField] private Transform enemyTransform;

    private void Start()
    {
        HumanoidName = "Enemy";
    }
    private void Update()
    {
        if (CurrentHealth == 0)
        {
            Die();
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        var player = collider.gameObject.GetComponent<Player>();
        if (player != null)
        {
            Vector2 hitDirection = new Vector2((collider.transform.position.x - enemyTransform.position.x) * 80f, 50f);
            hitDirection.Normalize();
            player.TakeDamage(1, hitDirection);
        }
    }
    
    protected override IEnumerator ApplyKnockBack(Rigidbody2D rb, Vector2 knockBackDirection)
    {
        rb.AddForce(knockBackDirection * 80f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
    }
    public override void TakeDamage(int damageTaken, Vector2 damageDirection)
    {
        CurrentHealth -= damageTaken;
        StartCoroutine(ApplyKnockBack(enemyRb, damageDirection));
    }
    
    protected override void Die()
    {
        Destroy(gameObject);
    }
}