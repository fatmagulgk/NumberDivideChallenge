using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScorePanelControl : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ScoreText;
    private void Awake()
    {
        ScorePrint();
    }
    public void  ScorePrint()
    {
        ScoreText.text = PlayerManager.instance.playerMotor.score.ToString();
    }


}
