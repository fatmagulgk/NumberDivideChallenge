using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.SceneManagement;



public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject SquarePrefab;
    [SerializeField] public Transform SquarePanel;   
    public List<GameObject> squares = new List<GameObject>();
    public Transform quizPanel;
    [SerializeField] private TextMeshProUGUI QuizText;
    public GameObject PressedButtonValue;
    GameManager game;
    [SerializeField] public Button RestartButton;
    public static UIManager instance { get; private set; }
    bool press =false;
    [SerializeField] private List<GameObject> Hearts;
    [SerializeField] public  TextMeshProUGUI countdownText;
    public float currentTime;
    [SerializeField] public GameObject ScorePanel;
    [SerializeField] public TextMeshProUGUI scoreText;
    [SerializeField] public GameObject GameOverPanel;
    [SerializeField] public Button PauseButton;
    [SerializeField] public Button MenuButton;
    [SerializeField] public Button PauseMenuButton;
    [SerializeField] public GameObject HealthPanel;
    [SerializeField] public GameObject TopPanel;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;           
        }
        instance = this;       
    }

    public void OnButtonClickVoiceControl()
    {
        AudioManager.Instance.PlayButtonClip();
    }
    public void GamePause()
    {
        TopPanel.gameObject.SetActive(true);
        SquarePanel.gameObject.SetActive(true);
        GameOverPanel.gameObject.SetActive(false);
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void PauseMenu()
    {
        TopPanel.gameObject.SetActive(false);
        SquarePanel.gameObject.SetActive(false);       
        GameOverPanel.gameObject.SetActive(true);
        GameOverPanel.transform.GetChild(3).gameObject.SetActive(false);
        GameOverPanel.transform.GetChild(4).gameObject.SetActive(false);       
    }
    private void Start()
    {
        RestartButton.onClick.AddListener(() => OnButtonClickVoiceControl());
        PauseButton.onClick.AddListener(() => OnButtonClickVoiceControl());
        MenuButton.onClick.AddListener(() => OnButtonClickVoiceControl());
        PauseMenuButton.onClick.AddListener(() => OnButtonClickVoiceControl());

        RestartButton.onClick.AddListener(GameManager.instance.RestartGame);
        PauseButton.onClick.AddListener(() => GamePause());
        MenuButton.onClick.AddListener(() => Menu());
        PauseMenuButton.onClick.AddListener(() => PauseMenu());

        currentTime = (float)TimerManager.instance.totalTime;
    }
    private void Update()
    {
        if (currentTime > 0 && HealtControl.instance.healt != 0)
        {
            currentTime -= Time.deltaTime;
            countdownText.text = TimerManager.instance.FormatTimeOperation(currentTime);
        }
        else if (currentTime <= 0 && HealtControl.instance.healt != 0)
        {
            AudioManager.Instance.PlayGameOverMusic();
            GameOver.instance.GameOverControl();
        }
        else if (HealtControl.instance.healt <= 0)
        {

            GameOver.instance.GameOverControl();
            AudioManager.Instance.PlayKillMusic();
        }
    }
    public void InitSquareCreate()
    {
        
        SquareCreate();
        quizPanel.GetComponent<RectTransform>().localScale = Vector3.zero;//Quiz panelin ebatlarini sifira cektik.
        foreach (var _square in squares)
        {
            _square.GetComponent<Button>().interactable = true;
        }
        //SquareRandomValuePrint();
        StartIe();
        Invoke(nameof (OpenQuizPanel), 2f);
    }
  
    public void SquareCreate()
    {
        /*System.Array.Clear(squares, 0, squares.Count);*///Bu dizinin 0'inci elemanindan sonuncu elamnina kadar sil.
        squares.Clear();
        int lengtsquares = SquarePanel.childCount;
        for (int i = 0; i < lengtsquares; i++)
        {
            //Debug.Log(SquarePanel.GetChild(i).name);
            Destroy(SquarePanel.GetChild(i).gameObject);//Destroy metodu objenin game objesini silmelidir.Aksi takdirde icerisinde kompanenetleri silmeye calisabilir ve hata alabiliriz.
        }

        int lenght = GameManager.instance.SquaresCount;
        int index = Random.Range(0, lengtsquares);
        for (int i = 0; i < lenght; i++)
        {
            GameObject square = Instantiate(SquarePrefab, SquarePanel);//Bu metotla cogaltma yapýlýyor.
            square.transform.GetComponent<Button>().onClick.AddListener(() => ButtonPressed());
           
            if (index == i )
            {
                square.transform.GetChild(0).GetComponent<Text>().text = OparetionManager.instance.trueResult.ToString();
                Debug.Log(i + "indexsli kare");
             
            }
            else
            {
                square.transform.GetChild(0).GetComponent<Text>().text = GameManager.instance.RandomSqruareValue(OparetionManager.instance.operation).ToString();
                

            }
            square.transform.SetSiblingIndex(i);
            
            squares.Add(square);

        }
        foreach (var _square in squares)
        {
            _square.GetComponent<Button>().interactable = false;
        }

    }
    void ButtonPressed()
    {
       
        
        if (press)
        {
            PressedButtonValue = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            
            GameManager.instance.ResultCheck();
        }
        if (GameManager.instance.resultControl)
        {
            AudioManager.Instance.PlayButtonClip();
        }
        else
        {
            AudioManager.Instance.PlayWrongAnswerClip();
        }
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
    GameObject trueSquare = null;
    void SquareRandomValuePrint()
    {
        
        bool control=false;
        Text squareText=null;
       
        foreach (var square in squares)
        {
            if (square.transform.GetChild(0).GetComponent<Text>().text == OparetionManager.instance.trueResult.ToString())
            {
                control = true;                               
                break;
            }
            else
            {
                control = false;
                trueSquare = square;            
            }               
        }
        if (!control)
        {

            int childCount = GameManager.instance.SquaresCount;
            Debug.Log("Child Count: " + childCount);
            int siblingIndex = Random.Range(0, childCount);
            trueSquare.transform.GetChild(0).GetComponent<Text>().text = OparetionManager.instance.trueResult.ToString();
            Debug.Log("trueSquare SiblingIndex => "+trueSquare.gameObject.transform.GetSiblingIndex());
            trueSquare.gameObject.transform.SetSiblingIndex(siblingIndex);
            Debug.Log("Dogru kare " + siblingIndex + " Indexli karenin yerine geçti "+ "trueSquare SiblingIndex => " + trueSquare.gameObject.transform.GetSiblingIndex());
        }
    }
    void OpenQuizPanel()
    {
        QuizPrint();
        quizPanel.GetComponent<RectTransform>().DOScale(1, 0.3f).SetEase(Ease.OutBack);
        press = true;
    }
    public void QuizPrint()
    {
        QuizText.text = OparetionManager.instance.QuizText;        
    }
    public void HealtUIControl()
    {
        Hearts[Hearts.Count-1].gameObject.SetActive(false);
        Hearts.RemoveAt(Hearts.Count-1);
    }
}
