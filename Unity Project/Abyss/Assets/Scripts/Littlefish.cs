using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Littlefish : MonoBehaviour
{
    public float Frequency = 2;
    public float Power = 0.25f;
    public float Time = 0.25f;
    public iTween.EaseType EaseType = iTween.EaseType.easeOutExpo;

    void Start()
    {
        InvokeRepeating("Move", Frequency, Frequency);
    }

    void Move()
    {
        iTween.MoveBy(gameObject, iTween.Hash("z", Power, "time", Time, "easetype", EaseType));
    }
}
