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

    private bool isTriggerEf = false;

    [Header("Components")]
    public FootTracker footTracker;
    Ball ball;
    [SerializeField] private GameObject hitEffect;


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
    public void HandleBallCollisionWithPlayer(Ball ball, Collision OtherCollider)
    {
        if (OtherCollider.gameObject == gameObject)
        {
            if(isTriggerEf) 
                return;
            isTriggerEf = true;
            //SoundManager.Instance.PlayKickShot();
            onCollisionWithBallSound?.Invoke();
            ball.lastPlayerTouchball = (int)footTracker.userId;
            Debug.Log("Player" + playerId + "hit the ball!");
            // Goi su kien ghi diem o day

            StartCoroutine(StopCollisionTime());
        }
    }
    IEnumerator StopCollisionTime()
    {
        SpawnEffect();
        yield return new WaitForSeconds(0.5f);
        isTriggerEf = false;
    }

    private void SpawnEffect()
    {        
        Instantiate(hitEffect, transform.position, Quaternion.identity);
    }
}

