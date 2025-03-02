using UnityEngine;

public interface IDestructible
{

    void Destroy(Vector3 position, float radius); // position where the object is destroyed
}
