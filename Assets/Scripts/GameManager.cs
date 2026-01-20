using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int[] playerScores { get; private set; } = new int[2];

    [Header("Settings")]
    UIManager uimanager;


    [Header("Actions")]
    public static Action<int ,int> onScoreChanged;
    public static Action<float> onTimeChanged;
    public static Action onGameOver;



    [Header("TimeData")]
    [SerializeField] private float gameDuration; // max time for the game in seconds
    public float currentTime { get; private set; }// current time left in seconds
    private bool isGameActive;


    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else 
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(!isGameActive)
            return; 
        GameClock();
    }


    public void AddScore(int playerIndex, int score)
    {
        playerScores[playerIndex] += score;
        onScoreChanged?.Invoke(playerIndex, playerScores[playerIndex]);
    }

    public void GameClock()
    {   
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            onTimeChanged?.Invoke(currentTime);
        }
        else
        {
            currentTime = 0;
            isGameActive = false;
            GameOver();
        }
    }


    public void StartGame()
    {
        // 1. Reset điểm số về 0
        playerScores[0] = 0;
        playerScores[1] = 0;

        // 2. Thiết lập thời gian và trạng thái
        currentTime = gameDuration;
        isGameActive = true;

        // 3. Thông báo cho UI cập nhật lại số 0
        onScoreChanged?.Invoke(0, 0);
        onScoreChanged?.Invoke(1, 0);
    }


    private void GameOver()
    {
        Debug.Log("Game Over");
        onGameOver?.Invoke();
    }

}
