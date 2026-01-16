using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{

    [Header("Data")]
    [SerializeField] private int playerId;
    public int lastPlayerTouchball; // Luu id nguoi choi cuoi cung cham vao ball
    public int playerScore;
    public int scores; 

    private void Awake()
    {
        Ball.onCollisionWithball += HandleBallCollisionWithPlayer;
    }

    private void OnDestroy()
    {
        Ball.onCollisionWithball -= HandleBallCollisionWithPlayer;
    }

    private void HandleBallCollisionWithPlayer(Ball ball, Collider TargetCollider)
    {
        if(TargetCollider == gameObject)
        {   
            lastPlayerTouchball = playerId;
            Debug.Log("Player"+ playerId +"hit the ball!");
            // Goi su kien ghi diem o day
            playerScore += scores;
            //goi UI Update diem o day
        }
    }
}
