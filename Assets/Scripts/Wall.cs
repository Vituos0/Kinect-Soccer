using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{


   // [Header("Settings")]

    //[Header("Components")]

    void Awake()
    {
        Ball.onCollisionWithball += HandleBallCollision;
    }

    private void OnDestroy()
    {
        Ball.onCollisionWithball -= HandleBallCollision;
    }  
    private void HandleBallCollision(Ball ball, Collider TargetCollider)
    {
        if(TargetCollider == gameObject)
        {
            Debug.Log("GOAL!");
            Destroy(ball.gameObject);     
        }
    }
}
