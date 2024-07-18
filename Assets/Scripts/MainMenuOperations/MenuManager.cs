using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;//Sahne gecisleri icin bu kutuphaneyi eklememiz gerek.
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;



public class MenuManager : MonoBehaviour,IPointerClickHandler    
{ 
    [SerializeField] private GameObject StartBtn, ExitBtn;
    [SerializeField] private GameObject DifficultyLevelPanel;
    [SerializeField] private GameObject  EasyBtn;
    [SerializeField] private GameObject MediumBtn;
    [SerializeField] private GameObject HardBtn;
    [SerializeField] private GameObject PlusBtn,MinusBtn,TimesBtn,DividedBtn;
    public string Operation;
    [SerializeField]private Slider MinuteSlider;
    public float minMinute=1f;
    public float maxMinute=30f;
    [SerializeField] private TextMeshProUGUI sliderText;

    [SerializeField] private GameObject ProgressPanel;
    public static MenuManager instance { get; private set; }
    public Difficulty Level;

    
    private void Start()
    {
        

        StartBtn.SetActive(true);
        MinuteSlider.gameObject.SetActive(true);
        StartBtn.GetComponent<Button>().interactable = false;
        ExitBtn.SetActive(true);
        PlusBtn.SetActive(true);
        MinusBtn.SetActive(true);
        TimesBtn.SetActive(true);
        DividedBtn.SetActive(true);
        FadeOutFirstButtons();
        AudioManager.Instance.PlayMenuMusic();
        StartBtn.GetComponent<Button>().onClick.AddListener(() => OnButtonClickVoiceControl());
        ExitBtn.GetComponent<Button>().onClick.AddListener(() => OnButtonClickVoiceControl());
        PlusBtn.GetComponent<Button>().onClick.AddListener(() => OnButtonClickVoiceControl());
        MinusBtn.GetComponent<Button>().onClick.AddListener(() => OnButtonClickVoiceControl());
        TimesBtn.GetComponent<Button>().onClick.AddListener(() => OnButtonClickVoiceControl());
        DividedBtn.GetComponent<Button>().onClick.AddListener(() => OnButtonClickVoiceControl());

        EasyBtn.GetComponent<Button>().onClick.AddListener(() => OnButtonClickVoiceControl());
        MediumBtn.GetComponent<Button>().onClick.AddListener(() => OnButtonClickVoiceControl());
        HardBtn.GetComponent<Button>().onClick.AddListener(() => OnButtonClickVoiceControl());

        MinuteSlider.minValue = minMinute;
        MinuteSlider.maxValue = maxMinute;
    }
    public void OnButtonClickVoiceControl()
    {
        AudioManager.Instance.PlayButtonClip();
    }
    private void Awake()
    {
        if (instance!=null)
        {
            Destroy(this);
            return;
            
        }
        instance=this;

        MinuteSlider.onValueChanged.AddListener(SliderValueChanged);

        EasyBtn.GetComponent<Button>().onClick.AddListener(() => SetDiffculty(Difficulty.Easy));
        MediumBtn.GetComponent<Button>().onClick.AddListener(() => SetDiffculty(Difficulty.Medium));
        HardBtn.GetComponent<Button>().onClick.AddListener(() => SetDiffculty(Difficulty.Hard));

        EasyBtn.GetComponent<Button>().onClick.AddListener(StartGame);
        MediumBtn.GetComponent<Button>().onClick.AddListener(StartGame);
        HardBtn.GetComponent<Button>().onClick.AddListener(StartGame);

        PlusBtn.GetComponent<Button>().onClick.AddListener(() => WhichProcessButton(PlusBtn));
        MinusBtn.GetComponent<Button>().onClick.AddListener(() => WhichProcessButton(MinusBtn));
        TimesBtn.GetComponent<Button>().onClick.AddListener(() => WhichProcessButton(TimesBtn));
        DividedBtn.GetComponent<Button>().onClick.AddListener(() => WhichProcessButton(DividedBtn));

    }
    
    public void SliderValueChanged(float _value)
    {
        int value = (int)_value;
        sliderText.text = value.ToString();
    }
 
    void FadeReset(params GameObject[] _fadeObjects)//Params sayesinde metodu overload ediyormus gibi istedigimiz sayida paremetre gondermemize olanak sagliyor.
    {
        
        foreach (var fadeobje in _fadeObjects)
        {
            if (fadeobje.TryGetComponent(out CanvasGroup component))//Gondermis oldugumuz objenin canvas group companenti var mi diye kontrol ediyor varsa companent adli degiskene atiyor.Bu sekilde companenti istedigimiz gibi manipule edebiliyoruz.
            {
                component.alpha = 0;
            }
        }
        

    }
    IEnumerator FadeHelperOperation(params FadeHelper[] helpers)
    {
        foreach (var helper in helpers)
        {
            if (helper.fadeObject.TryGetComponent(out CanvasGroup component))
            {
                Tween helperTween = component.DOFade(helper.value, helper.duration);
                yield return new WaitForSeconds(0.1f);
                
            }
        }
        
    }
    void FadeOut()
    {
        FadeReset( StartBtn,ExitBtn,PlusBtn,MinusBtn,TimesBtn,DividedBtn);
        StartCoroutine(FadeHelperOperation(new FadeHelper(1f, 0.5f, DifficultyLevelPanel),new FadeHelper(1f, 1f, EasyBtn), new FadeHelper(1f, 1.5f, MediumBtn), new FadeHelper(1f, 2f, HardBtn))); 
    } 
    void FadeOutFirstButtons()
    {
       
        FadeReset(DifficultyLevelPanel,EasyBtn,MediumBtn,HardBtn);

        StartCoroutine(FadeHelperOperation(new FadeHelper(1f, 0.5f, PlusBtn), new FadeHelper(1f, 0.5f, MinusBtn), new FadeHelper(1f, 0.5f, TimesBtn), new FadeHelper(1f, 0.5f, DividedBtn),new FadeHelper(1f,0.5f,MinuteSlider.gameObject), new FadeHelper(1f, 0.5f, StartBtn), new FadeHelper(1f, 0.5f, ExitBtn))) ;
       
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void SetMinute()
    {
        int value = (int)MinuteSlider.value;
        TimerManager.instance.totalTime =value*60;
    }
    public void LevelDesigner()
    {
        Debug.Log("Oyun baslatildi"); 
        MinuteSlider.gameObject.SetActive(false);
        StartBtn.SetActive(false);
        ExitBtn.SetActive(false);
        PlusBtn.SetActive(false);
        MinusBtn.SetActive(false);
        TimesBtn.SetActive(false);
        DividedBtn.SetActive(false);
        DifficultyLevelPanel.SetActive(true);
        FadeOut();
    }
    public void SetDiffculty(Difficulty _difficulty)
    {       
        Level = _difficulty;
    } 
    public void WhichProcessButton(GameObject button)
    {

        int lenght = ProgressPanel.transform.childCount;
        if (button!=null)
        {
            StartBtn.GetComponent<Button>().interactable = true;
            Operation = button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
            Debug.Log(Operation);
            
            for (int i = 0; i < lenght; i++)
            {
                if (button == ProgressPanel.transform.GetChild(i).gameObject)
                {
                    ProgressPanel.transform.GetChild(i).gameObject.GetComponent<Button>().interactable = false;
                }
                else
                {
                    ProgressPanel.transform.GetChild(i).gameObject.GetComponent<Button>().interactable = true;
                }
            }
        }       
    }
    public void StartGame()
    {        
        SceneManager.LoadScene("GameScene");
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Týklanan Obje => "+eventData.pointerPress);//Sahnede tiklanan obje bilgisini veren kod
    }
    
}
public enum Difficulty
{
    None,
    Easy,
    Medium,
    Hard
}
public struct FadeHelper
{
    public float value;
    public float duration;
    public GameObject fadeObject;
    public FadeHelper(float _value,float _duration,GameObject _fadeObject)
    {
        value = _value;
        duration = _duration;
        fadeObject = _fadeObject;
    }
}
