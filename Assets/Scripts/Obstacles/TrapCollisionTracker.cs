using UnityEngine;

public enum Direction {
    Left,
    Right,
    Up,
    Down
}

public class TrapCollisionTracker : MonoBehaviour
{
    [SerializeField] private int maxBlockDistance = 5; // max blocks distance to check for entities colliding
    [SerializeField] private int dartsCount = 3;
    [SerializeField] private float collisionCooldown = 1.0f; // cooldown between collisions
    [SerializeField] private Direction direction = Direction.Left; // direction of dart trap
    [SerializeField] private LayerMask obstacleLayer; // layers where raycast stops (walls,obstacles)
    [SerializeField] private LayerMask entityLayer; // layers where collision with entities is detected (player, monkeys,...)
    [SerializeField] private GameObject DartPrefab; // prefab of dart we are going to spawn
    private float blockSize = 1.0f; // grid block size
    private Vector3 raycastStart;
    private float actualCheckDistance = 0.0f; // after raycast of obstacles, we find actual raycast to use for entities
    
    private float currentCollisionCooldown = 0.0f;

    private int remainingDarts = 0;

    // when script loaded, or when we change values in editor
    private void Awake()
    {
        raycastStart = this.transform.position + this.rayCastStartDisplacement();
        remainingDarts = dartsCount;
        actualCheckDistance = checkObstacleCollision();
        this.transform.localScale = TrapCollisionTracker.initScale(this.direction);
        this.transform.rotation = Quaternion.Euler(TrapCollisionTracker.initRotation(this.direction)); 
    }
    
    private void Update() {
        CheckEntityCollision();

        if (currentCollisionCooldown > 0.0f) {
            currentCollisionCooldown -= Time.deltaTime;
        }
    }

    float checkObstacleCollision() {
        Vector2 trapDirection = TrapCollisionTracker.GetDirection(this.direction);
        float maxCheckDistance = maxBlockDistance * blockSize;
        RaycastHit2D hit = Physics2D.Raycast(raycastStart, trapDirection, maxCheckDistance, obstacleLayer);
        if (hit.collider != null) {
            return hit.distance;
        }
        return maxCheckDistance;
    }
    void CheckEntityCollision() {
        Vector2 trapDirection = TrapCollisionTracker.GetDirection(this.direction);
        RaycastHit2D entityHit = Physics2D.Raycast(raycastStart, trapDirection, actualCheckDistance, entityLayer);
        if (entityHit.collider != null && remainingDarts > 0 && currentCollisionCooldown <= 0.0f)
        {
            remainingDarts--;
            currentCollisionCooldown = collisionCooldown;
            // Debug.Log("Player detected within " + entityHit.distance + " units!");
            // Debug.Log("remaining darts: " + remainingDarts);
            // Debug.Log("At position: " + entityHit.point);
            spawnDart();
        }
    }

    void spawnDart() {
        GameObject dart = Instantiate(DartPrefab, this.transform.position + rayCastStartDisplacement() , Quaternion.identity, this.transform);
        dart.GetComponent<DartMovementScript>().SetArrowDirection(this.direction); // change dart direction based on trap direction
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 trapDirection = TrapCollisionTracker.GetDirection(this.direction);
        Vector3 start = this.transform.position + rayCastStartDisplacement();
        // Draw max block distance
        Gizmos.color = Color.green;
        Gizmos.DrawLine(start, start + new Vector3(trapDirection.x, trapDirection.y, 0f) * maxBlockDistance * blockSize);

        // Draw actual raycast range
        Gizmos.color = Color.red;
        Gizmos.DrawLine(start, start + new Vector3(trapDirection.x,trapDirection.y,0f) * actualCheckDistance);
    }

//variables to change trap rotation, scale, etc.., based on the direction trap is to be placed
    public static Vector3 initScale(Direction direction) {
        return direction switch
        {
            Direction.Left => new Vector3(1f, 1f, 1f),
            Direction.Right => new Vector3(-1f, 1f, 1f),
            Direction.Up => new Vector3(1f, 1f, 1f),
            Direction.Down => new Vector3(1f, 1f, 1f),
            _ => Vector3.zero
        };
    }

    public static Vector3 initRotation(Direction direction) {
        return direction switch
        {
            Direction.Left => new Vector3(0f, 0f, 0f),
            Direction.Right => new Vector3(0f, 0f, 0f),
            Direction.Up => new Vector3(0f, 0f, -90f),
            Direction.Down => new Vector3(0f, 0f, 90f),
            _ => Vector3.zero
        };
    }

    public static Vector2 GetDirection(Direction direction) {
        return direction switch
        {
            Direction.Left => Vector2.left,
            Direction.Right => Vector2.right,
            Direction.Up => Vector2.up,
            Direction.Down => Vector2.down,
            _ => Vector2.zero
        };
    }

    private Vector3 rayCastStartDisplacement() {
        return direction switch
        {
            Direction.Left => new Vector3(-0.51f, 0f, 0f),
            Direction.Right => new Vector3(0.51f, 0f, 0f),
            Direction.Up => new Vector3(0f, 0.51f, 0f),
            Direction.Down => new Vector3(0f, -0.51f, 0f),
            _ => Vector3.zero
        };
    }
}
