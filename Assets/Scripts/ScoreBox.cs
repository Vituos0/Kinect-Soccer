using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreBox : MonoBehaviour
{
   
    int value;

    Text scoreText;



    private void Awake()
    {
        scoreText = GetComponentInChildren<Text>();
    }
    public void Init(int score)
    {
        value = score;
        scoreText.text = value.ToString();
    }




}
