using System.Collections;
using UnityEngine;

public class TitleScreenCamera : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints; // waypoints to alterante between
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float waitTimeAtPoint = 2.0f;
    [SerializeField] private bool randomMovement = false; // allow for random movement, instead of having to follow the exact waypoints path

    private int currentIndex = 0;
    private Transform targetPoint;
    private bool isMoving = false;

    void Start()
    {
        if (waypoints.Length > 0)
        {
            targetPoint = waypoints[currentIndex];
            StartCoroutine(MoveCamera());
        }
        else
        {
            Debug.LogWarning("No waypoints assigned to TitleScreenCamera.");
        }
    }

    IEnumerator MoveCamera()
    {
        while (true)
        {
            if (!isMoving)
            {
                isMoving = true;
                yield return StartCoroutine(MoveToTarget(targetPoint.position));
                isMoving = false;
                yield return new WaitForSeconds(waitTimeAtPoint);
                SelectNextPoint();
            }
            yield return null;
        }
    }

    IEnumerator MoveToTarget(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPosition;
    }

    void SelectNextPoint()
    {
        if (randomMovement)
        {
            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, waypoints.Length);
            } while (randomIndex == currentIndex); // move to a random point,but guarantee it isnt the same point

            currentIndex = randomIndex;
        }
        else
        {
            currentIndex = (currentIndex + 1) % waypoints.Length;
        }

        targetPoint = waypoints[currentIndex];
    }
}