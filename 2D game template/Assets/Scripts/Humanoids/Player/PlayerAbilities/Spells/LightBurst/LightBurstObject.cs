using UnityEngine;

public class LightBurstObject : MonoBehaviour
{
    [SerializeField] private Cooldown despawnCooldown;
    [SerializeField] private float speed;
    [SerializeField] private Transform myTransform;

    public void Start()
    {
        despawnCooldown.StartCoolDown();
    }
    public void Update()
    {
        if (!despawnCooldown.IsCoolingDown) Destroy(gameObject);

        var direction = myTransform.localScale.x > 0 ? myTransform.right : -myTransform.right;
        var move = direction * (speed * Time.deltaTime);
        myTransform.position += move;
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        var enemy = collider.GetComponent<Enemy>();
        if (enemy != null)
        {
            var hitDirection = GetHitDirectionFromCollider(collider);
            hitDirection.Normalize();
            var hitObject = new Hit(2, hitDirection);
            enemy.HandleHit(hitObject);
        }
    }
    
    private Vector2 GetHitDirectionFromCollider(Collider2D collider)
    {
        return new Vector2((collider.transform.position.x - gameObject.transform.position.x) * 80f, 0f);
    }
}