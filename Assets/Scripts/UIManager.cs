using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{


    [Header("Data")]
    [SerializeField] private TextMeshProUGUI[] scoreText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject gameOverPanel; 
    [SerializeField] private TextMeshProUGUI[] finalScoreTexts;
    public Button StartButton;





    private void Awake()
    {   
        GameManager.onScoreChanged += UpdateScoreUI;
        GameManager.onTimeChanged += UpdateTimeUI;
        GameManager.onGameOver += GameOverUI;
    }
    private void OnDestroy()
    {
        GameManager.onScoreChanged -= UpdateScoreUI;
        GameManager.onTimeChanged -= UpdateTimeUI;
        GameManager.onGameOver -= GameOverUI;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateScoreUI(int playerIndex, int points)
    {
        scoreText[playerIndex].text = points.ToString();
    }

    private void UpdateTimeUI(float time)
    {
        timeText.text = Mathf.CeilToInt(time).ToString();
    }

    private void GameOverUI()
    {
        gameOverPanel.SetActive(true);
        for(int i = 0; i < finalScoreTexts.Length; i++)
        {
            finalScoreTexts[i].text = scoreText[i].text;
        }
    }

}
