using UnityEngine;

public interface IPickupable
{
    void OnPickup(GameObject parent);

    void OnDrop(GameObject parent);

}
