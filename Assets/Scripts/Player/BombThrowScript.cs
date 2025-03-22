using UnityEngine;
using UnityEngine.InputSystem;

public class BombThrowScript : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject bombPrefab;
    [SerializeField] private GameObject parent; // parent object under which all bomb items will be instantiated
    [SerializeField] private float throwForce = 10.0f;
    [SerializeField] private float throwAngle = 45.0f;
    [SerializeField] private float throwRotation = 50.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void ThrowBomb(InputAction.CallbackContext context) {
        if (context.performed && playerStats.getBombs() > 0) {
            GameObject bombInstance = Instantiate(bombPrefab, transform.position, Quaternion.identity, parent.transform);

            BombInstantiatedScript bombScript = bombInstance.GetComponent<BombInstantiatedScript>();
            Rigidbody2D bombRigidbody = bombInstance.GetComponent<Rigidbody2D>();

            if (bombScript == null || bombRigidbody == null) {
                Debug.LogError("BombInstantiatedScript or Rigidbody2D not found in bomb prefab");
                return;
            }

            //start animation countdown
            bombScript.startCountdown();

            // apply intial force to bomb
            bombRigidbody.linearVelocity = new Vector2(throwForce * Mathf.Cos(throwAngle * Mathf.Deg2Rad) * playerTransform.localScale.x, throwForce * Mathf.Sin(throwAngle * Mathf.Deg2Rad));
            bombRigidbody.angularVelocity = throwRotation;
            
            //update player state
            playerStats.modifyBombs(-1);
        }
    }
}

