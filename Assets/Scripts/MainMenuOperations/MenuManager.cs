using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;//Sahne gecisleri icin bu kutuphaneyi eklememiz gerek.
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour,IPointerClickHandler
    
{
   
    [SerializeField] private GameObject StartBtn, ExitBtn;
    private void Start()
    {
        FadeOut();
  

    }
    void FadeOut()
    {
        StartBtn.GetComponent<CanvasGroup>().DOFade(1, 0.5f);//Alfa degerini arttiriyor.
        ExitBtn.GetComponent<CanvasGroup>().DOFade(1, 0.8f).SetDelay(0.5f);//Gecikmeli bir sekilde yapiyor.
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        Debug.Log("Oyun baslatildi");
        SceneManager.LoadScene("GameScene");//Sahne arasý gecisi saglayan kod
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Týklanan Obje => "+eventData.pointerPress);//Sahnede tiklanan obje bilgisini veren kod
    }
}
