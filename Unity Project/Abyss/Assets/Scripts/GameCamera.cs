using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public GameObject ObjectToFollow;
    [Range(0, 2)]
    public float SpeedX = 1f;
    [Range(0, 2)]
    public float SpeedY = 1f;

    void LateUpdate()
    {
        float myY = gameObject.transform.position.y;
        float followY = ObjectToFollow.transform.position.y;

        float myX = gameObject.transform.position.x;
        float followX = ObjectToFollow.transform.position.x;

        if (followY > myY) //don´t follow the ball if it is above the camera
            return;

        float lerpedX = Mathf.Lerp(myX, followX, Time.deltaTime * SpeedX);
        float lerpedY = Mathf.Lerp(myY, followY, Time.deltaTime * SpeedY);

        //this is a super simple linear tween.
        transform.position = new Vector3(lerpedX, lerpedY, transform.position.z);
    }
}
