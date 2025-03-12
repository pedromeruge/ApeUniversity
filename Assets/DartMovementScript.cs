using UnityEngine;

public class DartMovementScript : MonoBehaviour
{
    [SerializeField] private Vector2 arrowDirection;

    [SerializeField] private float arrowSpeed = 10.0f;
    [SerializeField] private int damage = 1;

    void Update()
    {
        transform.position += (Vector3) arrowDirection * arrowSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            EventObstacle.Hit(damageable, damage);
        }

        Destroy(gameObject); // destroy arrow on collision, regardless if wall or damageable entity
    }
}
