using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class OparetionManager : MonoBehaviour
{
    public int number1,number2;
    public string operation;
   
    public string QuizText;
    public int trueResult;
    public static  OparetionManager instance { get; private set; }
    public List<string> LastQuestion = new List<string>();
    public int randomNumber1, randomNumber2;
    public string level;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
        
        operation = MenuManager.instance.Operation;
        Debug.Log("Progressmanager" + operation);
       
    }
  
    public void  WhichProgress()
    {
        switch (operation)
        {
            case "+":
                Plus();
                break;
            case "-":
                Minus();
                break;
            case "/":             
                Divided();
                break;
            case "x":
                Times();
                break;
            default:
                break;
        }
    }
    public void Plus()
    {
        do
        {
            RandomNumbers();
            trueResult = number1 + number2;
            QuizText = number1.ToString() + " + " + number2.ToString();

        }
        while (LastQuestion.Contains(QuizText));
        
        LastQuestion.Add(QuizText);
    }
    public void Minus()
    {
        do
        {
            randomNumber1 = Random.Range(0, GameManager.instance.temp.Count);
            number1 = GameManager.instance.temp[randomNumber1];
            randomNumber2 = Random.Range(0, GameManager.instance.temp.Count);
            number2 = GameManager.instance.temp[randomNumber2];
            trueResult = number1 - number2;
            QuizText = number1.ToString() + " - " + number2.ToString();
        }
        while (number1 < number2 || LastQuestion.Contains(QuizText));
        

        LastQuestion.Add(QuizText);      
    }
    public void Times()
    {
        do
        {
            randomNumber1 = Random.Range(0, GameManager.instance.temp.Count);
            number1 = GameManager.instance.temp[randomNumber1];
            randomNumber2 = Random.Range(0, GameManager.instance.temp.Count);
            number2 = GameManager.instance.temp[randomNumber2];
            trueResult = number1 * number2;
            QuizText = number1.ToString() + " x " + number2.ToString();
        }
        while (LastQuestion.Contains(QuizText));
        LastQuestion.Add(QuizText);

    }
    public void Divided()
    {
        do
        {
            RandomNumbers();
            trueResult = number1 / number2;
            QuizText = number1.ToString() + " : " + number2.ToString();
        }
        while (number2 * trueResult != number1 || LastQuestion.Contains(QuizText));
        

        LastQuestion.Add(QuizText);
        
    }
    
    public void RandomNumbers()
    {
        randomNumber1 = Random.Range(0, GameManager.instance.Veriablelist.Count);
        number1 = GameManager.instance.Veriablelist[randomNumber1];
        randomNumber2 = Random.Range(0, GameManager.instance.temp.Count);
        number2 = GameManager.instance.temp[randomNumber2];
        
    }
   
}
