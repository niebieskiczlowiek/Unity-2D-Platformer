using System.Collections;
using UnityEngine;

public class Enemy : Humanoid
{
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
        if (CheckIfColliderIsPlayer(collider)) { return; }
        var player = collider.gameObject.GetComponent<Player>();

        Vector2 hitDirection = GetHitDirectionFromCollider(collider);
        hitDirection.Normalize();
        
        Hit hitObject = new Hit(1, hitDirection);
        player.HandleHit(hitObject);
    }

    private Vector2 GetHitDirectionFromCollider(Collider2D collider)
    {
        return new Vector2((collider.transform.position.x - enemyTransform.position.x) * 80f, 50f);
    }
    private static bool CheckIfColliderIsPlayer(Collider2D collider)
    {
        var player = collider.gameObject.GetComponent<Player>();
        return player == null;
    }
    protected override void TakeDamage(int damageTaken) { CurrentHealth -= damageTaken; }
    protected override void Die() { Destroy(gameObject); }
}