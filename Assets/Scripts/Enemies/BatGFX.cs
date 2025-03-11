using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aiPath;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
    //    
    //}

    // Update is called once per frame
    void FixedUpdate()
    {
        if(aiPath.desiredVelocity.x >= 0.01f ){
            transform.localScale = new Vector3(-1f,1f,1f);
        }else if(aiPath.desiredVelocity.x <= 0.01f){
            transform.localScale = new Vector3(1f,1f,1f);
        }
    }
}
