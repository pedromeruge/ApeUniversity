using UnityEngine;

public interface IPickupable
{
    void OnPickup(GameObject playerParent);

    void OnDrop(GameObject dropParent);

    // dropParent - parent object in scene to which the item goes when dropped
    // playerParent - player object in scene, from which the item is used. relevant to apply items in relation to the player position
    // returns: true if item was dropped when using, false otherwise
    bool OnUse(GameObject dropParent, GameObject playerParent);

}
