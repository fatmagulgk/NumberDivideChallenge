using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameOver : MonoBehaviour
{
    public static GameOver instance { get; private set; }
    
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;

        }
        instance = this;
    }
    public void GameOverControl()
    {
        
        UIManager.instance.quizPanel.gameObject.SetActive(false);
        UIManager.instance.HealthPanel.gameObject.SetActive(false);
        UIManager.instance.SquarePanel.gameObject.SetActive(false);
        UIManager.instance.countdownText.gameObject.SetActive(false);
        UIManager.instance.ScorePanel.gameObject.SetActive(false);

        UIManager.instance.GameOverPanel.gameObject.SetActive(true);
        UIManager.instance.GameOverPanel.transform.GetChild(3).gameObject.SetActive(true);
        UIManager.instance.GameOverPanel.transform.GetChild(4).gameObject.SetActive(true);
        UIManager.instance.GameOverPanel.transform.GetChild(1).gameObject.SetActive(false);



        UIManager.instance.scoreText.text = PlayerMotor.instance.score.ToString();

        
    }
}

