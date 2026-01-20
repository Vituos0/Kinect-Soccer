using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{


   // [Header("Settings")]

    //[Header("Components")]

    void Awake()
    {
        Ball.onCollisionWithball += HandleBallTouchCollision;
    }

    private void OnDestroy()
    {
        Ball.onCollisionWithball -= HandleBallTouchCollision;
    }  
    private void HandleBallTouchCollision(Ball ball, Collision TargetCollider)
    {
        if(TargetCollider.gameObject  == gameObject)
        {
            Debug.Log("GOAL!");
            Destroy(ball.gameObject);     
        }
    }
}
