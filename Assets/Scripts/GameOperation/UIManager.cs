using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEditor.Presets;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject SquarePrefab;
    [SerializeField] private Transform SquarePanel;

    private GameObject[] squares = new GameObject[25];
    public Transform quizPanel;

    [SerializeField] private Text QuizText;

    GameManager game;
    private void Start()
    {
        SquareCreate();
        quizPanel.GetComponent<RectTransform>().localScale = Vector3.zero;//Quiz panelin ebatlarini sifira cektik.
        SquareRandomValuePrint();
        StartIe();
        Invoke("OpenQuizPanel", 2f);
        //foreach(int i in GameManager.QuotientList)
        //{
        //    Debug.Log(i);
        //}

    }
    public void SquareCreate()
    {
        int lenght = squares.Length;
        for (int i = 0; i < lenght; i++)
        {
            GameObject square = Instantiate(SquarePrefab, SquarePanel);//Bu metotla cogaltma yapýlýyor.
            square.transform.GetComponent<Button>().onClick.AddListener(() => ButtonPressed());
            squares[i] = square;
        }
    }
    void ButtonPressed()
    {

    }
    IEnumerator DofadeRoutine()
    {
       
        foreach (var square in squares)
        {           
            square.GetComponent<CanvasGroup>().DOFade(1, 0.2f);
            yield return new WaitForSeconds(0.07f);
        }
    }
    private void StartIe()
    {
        StartCoroutine(DofadeRoutine());
    }
    void SquareRandomValuePrint()
    {
        foreach (var square in squares)
        {
            //int randomSquareValue = Random.Range(1, 13);
            square.transform.GetChild(0).GetComponent<Text>().text = GameManager.RandomSqruareValue().ToString();
        }
    }
    void OpenQuizPanel()
    {
        GameManager.AskQuestion();
        QuizPrint();
        quizPanel.GetComponent<RectTransform>().DOScale(1, 0.3f).SetEase(Ease.OutBack);
    }
    void QuizPrint()
    {
        QuizText.text = GameManager.QuizText;
    }
}
