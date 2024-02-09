using System.Collections;
using UnityEngine;

public class Enemy : Humanoid
{
    //[SerializeField] private Rigidbody2D enemyRb;
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
        if (player == null) { return; }
        
        Vector2 hitDirection = new Vector2((collider.transform.position.x - enemyTransform.position.x) * 80f, 50f);
        hitDirection.Normalize();
       // player.TakeDamage(1, hitDirection);
       Hit hitObject = new Hit(1, hitDirection);
       player.HandleHit(hitObject);
       player.checkCollision();
    }
    
    protected override IEnumerator ApplyKnockBack(Vector2 knockBackDirection)
    {
        Rigidbody.AddForce(knockBackDirection * 80f, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.1f);
    }
    protected override void TakeDamage(int damageTaken)
    {
        CurrentHealth -= damageTaken;
    }
    protected override void Die()
    {
        Destroy(gameObject);
    }
}