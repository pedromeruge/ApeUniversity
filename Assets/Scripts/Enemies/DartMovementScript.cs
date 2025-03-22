using UnityEngine;

public class DartMovementScript : MonoBehaviour
{
    [SerializeField] public Direction arrowDirection;

    [SerializeField] private float arrowSpeed = 10.0f;
    [SerializeField] private int damage = 1;

    private Vector3 arrowUpdateVector;
    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        setRigidbodyConstraints(rb);
        this.transform.rotation = Quaternion.Euler(TrapCollisionTracker.initRotation(this.arrowDirection)); 
        // arrow scale doesnt need to also be changed like the trap scale, since it is a child of the trap, and will inherit the scale
        arrowUpdateVector = TrapCollisionTracker.GetDirection(arrowDirection) * arrowSpeed * Time.deltaTime;
    }
    void Update()
    {
        transform.position += arrowUpdateVector;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Vector3 objPos = transform.position;
        CallInterfaces.triggerExplosive(collider, objPos);
        CallInterfaces.hitDamageable(collider, objPos, damage);
        CallInterfaces.signalDestructibles(collider, objPos, 0.01f);



        Destroy(gameObject); // destroy arrow on collision, regardless if wall or damageable entity
    }

    //based on arrow supposed direction, set constraints to prevent position changes
    private void setRigidbodyConstraints(Rigidbody2D rb)
    {
        Vector2 arrowDirectionVector = TrapCollisionTracker.GetDirection(this.arrowDirection);
        if (arrowDirectionVector.x != 0)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        else if (arrowDirectionVector.y != 0)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
    }

    public void SetArrowDirection(Direction direction)
    {
        arrowDirection = direction;
    }
}
