using nuitrack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{

    [Header("Data")]
    public int playerId;
    public long lastPlayerTouchball; // Luu id nguoi choi cuoi cung cham vao ball
    public int playerScore;
    public int scores;

    [Header("Components")]
    public FootTracker footTracker;
    Ball ball;


    [Header("Actions")]
    public static Action onCollisionWithBallSound;




    private void Awake()
    {
        Ball.onCollisionWithball += HandleBallCollisionWithPlayer;
    }
    private void OnDestroy()
    {
        Ball.onCollisionWithball -= HandleBallCollisionWithPlayer;
    }
    public void HandleBallCollisionWithPlayer(Ball ball, Collision  OtherCollider)
    {
        if (OtherCollider.gameObject == gameObject)
        {
            //SoundManager.Instance.PlayKickShot();
            onCollisionWithBallSound?.Invoke();
            ball.lastPlayerTouchball = (int)footTracker.userId;
            Debug.Log("Player" + playerId + "hit the ball!");
            // Goi su kien ghi diem o day

        }
    }
}
