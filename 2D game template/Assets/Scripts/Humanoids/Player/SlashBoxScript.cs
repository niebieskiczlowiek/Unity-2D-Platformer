using UnityEngine;
public class SlashBoxScript : MonoBehaviour
{
    [SerializeField] private Transform slashBoxTransform;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Enemy enemy = collider.GetComponent<Enemy>();
        if (enemy != null)
        {
            Vector2 slashDirection = new Vector2((collider.transform.position.x - slashBoxTransform.position.x) * 80f, 0f);
            Hit hitObject = new Hit(1, slashDirection);
            enemy.HandleHit(hitObject);
        }
    }
}
