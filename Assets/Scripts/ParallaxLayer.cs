using UnityEngine;
 
 //based on https://www.youtube.com/watch?v=MEy-kIGE-lI
[ExecuteInEditMode]
public class ParallaxLayer : MonoBehaviour
{
    public float parallaxFactor;
 
    public void Move(float delta)
    {
        // layers will move in opposite direction of camera
        // further away layers should have lower parallax factor, moving slower, creating the parallax illusion
        Vector3 newPos = transform.localPosition;
        newPos.x -= delta * parallaxFactor; 
 
        transform.localPosition = newPos;
    }
 
}