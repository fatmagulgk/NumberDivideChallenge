using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealtControl : MonoBehaviour
{
    public int healt;
    public static HealtControl instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;

        }
        instance = this;
        PlayerHealtAdd(3);
    }
    void PlayerHealtAdd(int _healt)
    {
        healt = _healt;
    }
    public void ReducePlayerHealthPoints(int damage)
    {
        healt -= damage;
    }
}
