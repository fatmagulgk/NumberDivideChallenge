using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static  ScoreManager instance {  get; private set; }
    [SerializeField]public ScorePanelControl panelControl;//Static olmayan bir class'in hangi nesneyi isaret ettgini bilemeyiz.Bunun icin atama yapmaliyiz.
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;


    }
    public void PlayerScoreAdd(int _score)
    {
        PlayerManager.instance.playerMotor.score += _score;
        panelControl.ScorePrint();
    }
    public void PlayerScoreReduction(int _score)
    {
        PlayerManager.instance.playerMotor.score -= _score;
        panelControl.ScorePrint();
    }
}
