using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System.Linq;

public class PickupStaticCollectiblesScript : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision) {
        IStaticCollectible staticCollectible = collision.GetComponent<IStaticCollectible>();
        if (staticCollectible != null) {
            staticCollectible.onCollect(playerStats);
        }
    }
}

