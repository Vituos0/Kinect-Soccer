using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{


   // [Header("Settings")]

    //[Header("Components")]
    private Ball ball;

    void Awake()
    {
        Ball.onCollisionWithball += HandleBallCollision;
    }

    private void OnDestroy()
    {
        Ball.onCollisionWithball -= HandleBallCollision;
    }  



    private void HandleBallCollision()
    {
        Debug.Log("GOAL!");
        ball.DeleteTheBall();
    }
}
