using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static  List<int> QuotientList = new List<int>();
    static int _dividend, _divisor;
    static int HowMuchQuiz;
    public static  string QuizText;
    public static int RandomSqruareValue()
    {
        int randomNumber = Random.Range(1, 13);
        QuotientList.Add(randomNumber);
        return randomNumber;
    }

    public static void AskQuestion()
    {
        _divisor = Random.Range(2, 11);
        HowMuchQuiz = Random.Range(0,QuotientList.Count);
        _dividend = _divisor * 5 ;
        QuizText = _dividend.ToString()+ " : " +_divisor.ToString();
       
    }
}
