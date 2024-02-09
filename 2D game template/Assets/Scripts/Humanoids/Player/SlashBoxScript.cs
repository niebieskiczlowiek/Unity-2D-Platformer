using UnityEngine;
public class SlashBoxScript : MonoBehaviour
{
    [SerializeField] private Transform slashBoxTransform;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Enemy enemy = collider.GetComponent<Enemy>();
        if (enemy != null)
        {
            var slashDirection = GetHitDirectionFromCollider(collider);
            slashDirection.Normalize();
            var hitObject = new Hit(1, slashDirection);
            enemy.HandleHit(hitObject);
        }
    }
    
    private Vector2 GetHitDirectionFromCollider(Collider2D collider)
    {
        return new Vector2((collider.transform.position.x - slashBoxTransform.position.x) * 80f, 0f);
    }
}
