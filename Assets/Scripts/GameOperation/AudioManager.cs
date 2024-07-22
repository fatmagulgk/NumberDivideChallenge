using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioSource audioSource;
    [SerializeField]public AudioClip ButonClip;
    [SerializeField]public AudioClip WrongAnswerClip;
    [SerializeField]public AudioClip StartMusicClip;
    [SerializeField]public AudioClip KillClip;
    [SerializeField]public AudioClip GameOverClip;
    [SerializeField] public GameObject SoundPrefab;

    public static AudioManager Instance { get => Getting(); private set { instance = value; } }
    private static AudioManager instance;

    private static AudioManager Getting()
    {
        if (audioSource==null)
        {
            AudioSource _audioSource = Camera.main.GetComponent<AudioSource>();
            if (_audioSource!=null)
            {
                audioSource = _audioSource;
            }
            else
            {

                Debug.LogError("Mevcut sahnede ki kamerada audiosource bulunmamtadir.Mevcut sahne : "+SceneManager.GetActiveScene().name);
                audioSource = Camera.main.AddComponent<AudioSource>();
            }
            
        }
        return instance;
        

    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void PlayMenuMusic()
    {
        AudioClip clip = StartMusicClip;
        GameObject sound = Instantiate(SoundPrefab);
        SoundDataHandler soundDataHandler = sound.GetComponent<SoundDataHandler>();
        soundDataHandler.PlayClip(clip);

    }
    public void PlayKillMusic()
    {
        AudioClip clip = KillClip;
        GameObject sound = Instantiate(SoundPrefab);
        SoundDataHandler soundDataHandler = sound.GetComponent<SoundDataHandler>();
        soundDataHandler.PlayClip(clip);
    }
    public void PlayGameOverMusic()
    {
        AudioClip clip = GameOverClip;
        GameObject sound = Instantiate(SoundPrefab);
        SoundDataHandler soundDataHandler = sound.GetComponent<SoundDataHandler>();
        soundDataHandler.PlayClip(clip);

    }
    public void PlayWrongAnswerClip()
    {
        GameObject sound = Instantiate(SoundPrefab);//Instantiate metotu gönderilen prefabýn kopyasýný sahneye oluþturur.
        DontDestroyOnLoad (sound);
        SoundDataHandler soundDataHandler = sound.GetComponent<SoundDataHandler>();
        soundDataHandler.PlayOneShotClip(WrongAnswerClip);
    }
    public void PlayButtonClip()
    {
        GameObject sound = Instantiate(SoundPrefab);//Instantiate metotu gönderilen prefabýn kopyasýný sahneye oluþturur.
        DontDestroyOnLoad(sound);
        SoundDataHandler soundDataHandler = sound.GetComponent<SoundDataHandler>();
        soundDataHandler.PlayOneShotClip(ButonClip);
    }
   
}
