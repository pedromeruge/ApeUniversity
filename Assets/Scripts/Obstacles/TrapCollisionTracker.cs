using UnityEngine;

public class TrapCollisionTracker : MonoBehaviour
{
    [SerializeField] private int maxBlockDistance = 5; // max blocks distance to check for entities colliding
    [SerializeField] private int dartsCount = 3;
    [SerializeField] private float collisionCooldown = 1.0f; // cooldown between collisions
    [SerializeField] private LayerMask obstacleLayer; // layers where raycast stops (walls,obstacles)
    [SerializeField] private LayerMask entityLayer; // layers where collision with entities is detected (player, monkeys,...)
    [SerializeField] private GameObject sceneItemsParent; // parent in scene where items go, so its better organized
    [SerializeField] private GameObject DartPrefab; // prefab of dart we are going to spawn
    private float blockSize = 1.0f; // grid block size
    private Vector3 raycastStart;
    private Vector2 direction = Vector2.left;
    private float actualCheckDistance = 0.0f; // after raycast of obstacles, we find actual raycast to use for entities
    
    private float currentCollisionCooldown = 0.0f;

    private int remainingDarts = 0;
    private void Awake() {
        raycastStart = this.transform.position + new Vector3(-0.51f, 0f, 0f);
        remainingDarts = dartsCount;
    }
    private void Start() {
        actualCheckDistance = checkObstacleCollision();
    }
    private void Update() {
        CheckEntityCollision();

        if (collisionCooldown > 0.0f) {
            collisionCooldown -= Time.deltaTime;
        }
    }

    float checkObstacleCollision() {
        float maxCheckDistance = maxBlockDistance * blockSize;
        RaycastHit2D hit = Physics2D.Raycast(raycastStart, direction, maxCheckDistance, obstacleLayer);
        if (hit.collider != null) {
            return hit.distance;
        }
        return maxCheckDistance;
    }
    void CheckEntityCollision() {
        RaycastHit2D entityHit = Physics2D.Raycast(raycastStart, direction, actualCheckDistance, entityLayer);
        if (entityHit.collider != null && remainingDarts > 0 && collisionCooldown <= 0.0f)
        {
            remainingDarts--;
            collisionCooldown = 1.0f;
            Debug.Log("Player detected within " + entityHit.distance + " units!");
            Debug.Log("remaining darts: " + remainingDarts);
            spawnDart();
        }
    }

    void spawnDart() {
        GameObject dart = Instantiate(DartPrefab, transform.position + new Vector3(-0.5f,0f,0f) , Quaternion.identity, sceneItemsParent.transform);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 start = this.transform.position + new Vector3(-0.5f, 0f, 0f);
        // Draw max block distance
        Gizmos.color = Color.green;
        Gizmos.DrawLine(start, start + new Vector3(direction.x, direction.y, 0f) * maxBlockDistance * blockSize);

        // Draw actual raycast range (only in gameplay mode)
        Gizmos.color = Color.red;
        Gizmos.DrawLine(start, start + new Vector3(direction.x,direction.y,0f) * actualCheckDistance);
    }
}
