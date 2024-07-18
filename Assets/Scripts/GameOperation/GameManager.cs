
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    
    public  List<int>RandomValue = new List<int>();
    public  List<int> Veriablelist = new List<int>();
    
    
    public  int randomIndex;
    public  bool control=true;
    public  int value;
    public  int currentIndex = 0;
    public static GameManager instance {  get; private set; }

    public int QuestionScore;
    public int BonusScore;
    public List<int> temp = new List<int>();
    public int _temp;
    public int SquaresCount = 25;
    public bool resultControl=true;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
    }
    public void Temp()
    {
        foreach (int i in RandomValue)
        {
            _temp = i;
            temp.Add(_temp);
        }
    }
    private void Start()
    {
        ReloadQuestion();
    }
    public void ReloadQuestion()
    {
        RandomValueListAdd();
        Temp();
        Debug.Log("gamemanager"+OparetionManager.instance.operation);
        VeriableListAdd(OparetionManager.instance.operation);//null hatasi
        AskQuestion();
        UIManager.instance.InitSquareCreate();
    }
    public  void RandomValueListAdd()
    {
        if (MenuManager.instance.Level==Difficulty.Easy)
        {
            for (int i = 0; i < 25; i++)
            {
                randomIndex = Random.Range(2, 10);
                RandomValue.Add(randomIndex);
            }
        }
        else if (MenuManager.instance.Level == Difficulty.Medium)
        {
            for (int i = 0; i < 25; i++)
            {
                randomIndex = Random.Range(10, 40);
                RandomValue.Add(randomIndex);
            }
        }
        else if (MenuManager.instance.Level == Difficulty.Hard)
        {
            for (int i = 0; i < 25; i++)
            {
                randomIndex = Random.Range(50, 100);
                RandomValue.Add(randomIndex);
            }
        }
    }

    public void VeriableListAdd(string _operation)
    {
        int value = 0;
        for (int i = 0; i < temp.Count; i++) // 0 * 5
        {
            if (_operation == "+")
            {
                value = temp[i] -(- temp[Random.Range(0, temp.Count)]);
            }
            else if (_operation == "-")
            {
                value = temp[i] + temp[Random.Range(0, temp.Count)];
            }
            else if (_operation == "/")
            {
                value = temp[i] * temp[Random.Range(0, temp.Count)];
            }
            else if (_operation == "x")
            {
                value = temp[i] * temp[Random.Range(0, temp.Count)];
            }
            Veriablelist.Add(value);
        }
    }
    public (int x ,int y) CalculateAndRetrieveRange(int result,string _operator)
    {
        int resultLength = result.ToString().Length;
        (int x, int y) numbersRange = (_operator) switch
        {
            "+" => (result - 5, result + 5),
            "-" => (-result, result + 5),
            "x" => resultLength switch
            {
                1 => (1, 10), 
                2 => (10, result +20), 
                3 => (100 , 1000),  
                4 => (1000, 10000),
                _ => (0,0)
            },
            "/" => (result - 20 ,result + 20),
            _ => (0,0)
        };
        return numbersRange;
    }
    public  int RandomSqruareValue(string _operation)
    {
        int trueResult=OparetionManager.instance.trueResult;
        var numbersRange = CalculateAndRetrieveRange(trueResult, _operation);
        if ((_operation == "/" && numbersRange.x < 0)||(_operation == "-" && numbersRange.x < 0))
        {
            numbersRange.x = 0;
        }
        int result = Random.Range(numbersRange.x, numbersRange.y);  
        
        return result;
    }
    public  void AskQuestion()
    {
        OparetionManager.instance.WhichProgress();      
        UIManager.instance.QuizPrint();
    }
    public  void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//Aktif olan sahneyi caðýrmak icin kullanilir,
    }
    public void ResultCheck()
    {
        if (UIManager.instance.PressedButtonValue.transform.GetChild(0).GetComponent<Text>().text == OparetionManager.instance.trueResult.ToString())
        {
            resultControl = true;
            HowManyPointsQuestion(OparetionManager.instance.operation,MenuManager.instance.Level);
            ScoreManager.instance.PlayerScoreAdd(QuestionScore);
            Debug.Log("Doðru cevap verildi");            
            ReloadQuestion();
        }
        else
        {
            resultControl = false;
            UIManager.instance.HealtUIControl();
            PlayerManager.instance.playerHealt.ReducePlayerHealthPoints(1);
            ScoreManager.instance.PlayerScoreReduction(QuestionScore);
            UIManager.instance.PressedButtonValue.GetComponent<Image>().enabled = false;
            UIManager.instance.PressedButtonValue.transform.GetChild(0).GetComponent<Text>().enabled = false;
            AudioManager.Instance.PlayWrongAnswerClip();

        }
    }   
    public void HowManyPointsQuestion(string _operation ,Difficulty difficulty)
    {
        int bonusScore=0;
        if (difficulty==Difficulty.Easy)
        {
            bonusScore += 1;
        }
        else if (difficulty==Difficulty.Medium)
        {
            bonusScore += 2;
        }else if(difficulty==Difficulty.Hard)
        {
            bonusScore += 3;
        }
        QuestionScore = (OparetionManager.instance.number1 - OparetionManager.instance.number2) / 2 + bonusScore;

    }

}
