using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance { get; private set; }
    GameObject player;
    public HealtControl playerHealt;
    public PlayerMotor playerMotor;
    
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
        player = GameObject.FindWithTag("Player");//Tag'deki objeyi hiyerarside arayip bulur.
        playerHealt = ScanAndGetPlayerComponents(playerHealt);//Bu kodda kastedilen objenin bütün çocuklarýna bakar ve operatorlerin icerisinde yazan companeneti arar.
        //Bu sekilde Playermanager.instance.playerHealt diyerek HealtControl scriptine eriþebiliriz.
        playerMotor = ScanAndGetPlayerComponents(playerMotor);
        
    }
    public T ScanAndGetPlayerComponents<T>(T _component)
    {
        if (player.TryGetComponent(out T component))//Objenin uzerinde boyle bir componenet var mi? Kontrol eder.
        return component;
        int length = player.transform.childCount;
        for (int i = 0; i < length; i++)
            if (player.transform.GetChild(i).TryGetComponent(out T component1))
                return component1;
        Debug.Log("Istenilen component player'da bulunamadi.");
        return _component;
    }




}
