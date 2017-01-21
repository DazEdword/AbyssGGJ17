﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public GameObject ObjectToFollow;
    [Range(0, 2)]
    public float SpeedX = 1f;
    [Range(0, 2)]
    public float SpeedY = 1f;

    public float ZFactor = 0.01f;

    public Light Light;

    void LateUpdate()
    {
        if (!GameManager.Instance.GameStarted)
            return;

        float myY = gameObject.transform.position.y;
        float followY = ObjectToFollow.transform.position.y;

        float myX = gameObject.transform.position.x;
        float followX = ObjectToFollow.transform.position.x;

        float myZ = gameObject.transform.position.z;
        float targetZ = -(5 + (-ZFactor * GameManager.Instance.Ball.transform.position.y));

        float lerpedX = Mathf.Lerp(myX, followX, Time.deltaTime * SpeedX);
        float lerpedY = Mathf.Lerp(myY, followY, Time.deltaTime * SpeedY);
        float lerpedZ = Mathf.Lerp(myZ, targetZ, Time.deltaTime);


        //this is a super simple linear tween.
        transform.position = new Vector3(lerpedX, lerpedY, lerpedZ);

        transform.LookAt(ObjectToFollow.transform);
    }
}
