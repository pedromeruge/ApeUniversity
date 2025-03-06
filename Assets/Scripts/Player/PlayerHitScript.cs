using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Linq;

public class PlayerHitScript : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    private HashSet<IHittable> catchables = new HashSet<IHittable>(); // set of catchable objects at each point in time
    public static event Action<IHittable> OnEntityHit; // signal to caught entities that they were caught 
    // (better separation of concerns this way, by using events. instead of calling methods in other classes directly when they are caught, which might require rework if the target classes change

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {

        IHittable catchable = collision.GetComponent<IHittable>();
        if (catchable != null) {
            catchables.Add(catchable);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        IHittable catchable = collision.GetComponent<IHittable>();
        if (catchable != null) {
            catchables.Remove(catchable);
        }
    }

    public void Catch(InputAction.CallbackContext context) {

        // context performed because we only care when button starts being pressed, not if it is held down
        if (context.performed) {

            foreach (IHittable catchable in catchables.ToHashSet()) { // call event for all objects present in catch range
                catchables.Remove(catchable);
                OnEntityHit?.Invoke(catchable); // signal to caught entities that they were caught
                playerStats.modifyMonkeys(1); // add monkey to player stats
            }
        }
    }
}

